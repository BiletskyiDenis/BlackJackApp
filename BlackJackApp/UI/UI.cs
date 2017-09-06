using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackJackApp.UI
{
    public static class UI
    {
        public static void DrawMain()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;

            DrawImage(Images.MainLogo(), new Point(10, 1), ConsoleColor.Yellow);
            DrawImage(Images.MainFooter(), new Point(30, 13), ConsoleColor.DarkGreen);

            backgroundWorker.DoWork += new DoWorkEventHandler((o, w) =>
            {
                var colorChange = false;
                while (!backgroundWorker.CancellationPending)
                {
                    DrawImage(Images.PressEnter(), new Point(16, 9), colorChange ? ConsoleColor.DarkYellow : ConsoleColor.Yellow);
                    colorChange = !colorChange;

                    Thread.Sleep(250);
                }
            });

            backgroundWorker.RunWorkerAsync();

            var enterKey = new ConsoleKeyInfo();
            while (enterKey.Key != ConsoleKey.Enter)
            {
                enterKey = Console.ReadKey(true);
            }

            backgroundWorker.CancelAsync();

            Thread.Sleep(500);
            Console.Clear();

        }

        public static void DrawCards(object sender, HandEventArgs e)
        {
            var fieldWidth = 80;
            var y = sender is GameDealer ? 1 : 15;
            var distance = e.IsSplit ? 5 : 10;
            var offset = e.IsSplit ? e.hand.HandNumber > 1 ? 15 : -15 : 0;

            var startPos = (fieldWidth / 2 - (e.hand.Cards.Count * distance) / 2) + offset;

            ClearRegion(new Point(startPos, y), new Size(distance * e.hand.Cards.Count, 7));

            var x = 0;
            foreach (var card in e.hand.Cards)
            {
                var cardImg = card.IsHidden ? Images.HiddenCard() : Images.Card(card);

                DrawImage(cardImg, new Point(distance * x + startPos, y));

                x++;
            }
            Thread.Sleep(GameConfig.DistributionSpeed);
        }

        public static void DrawDeck(int deckCount, int startCount)
        {
            var count = (int)Math.Ceiling(((deckCount / (double)startCount) * 5));

            ClearRegion(new Point(65, 8), new Size(13, 7));
            DrawImage(Images.Deck(count), new Point(65, 8), ConsoleColor.DarkYellow);
        }

        public static void DrawCorn(object sender, EventArgs e)
        {
            var player = (GamePlayer)sender;
            var hand = player.GetHand().HandNumber;
            var count = player.GetHand().DoubleBet ? 2 : 1;

            if (player.GetHand().Bet == 0)
            {
                return;
            }

            if (hand == 1)
            {
                DrawImage(Images.Corn(player.GetHand().Bet, count), new Point(22, 11), ConsoleColor.DarkGreen);
                if (player.GetHand().Insurance)
                {
                    DrawImage(Images.Corn(player.GetHand().Bet / 2, count), new Point(22, 8), ConsoleColor.DarkMagenta);
                }
                return;

            }
            DrawImage(Images.Corn(player.GetHand().Bet, count), new Point(56, 11), ConsoleColor.DarkGreen);
        }

        public static int ShowDialog(string message, IEnumerable<DialogItem> dialogItems, Size size, ConsoleColor dialogColor = ConsoleColor.Gray, ConsoleColor textColor = ConsoleColor.White, ConsoleColor SelectTextColor = ConsoleColor.Gray)
        {
            ClearKeyBuffer();
            var EnadleItems = dialogItems.Where(item => item.Enabled).ToList();

            size.Height -= dialogItems.Count() - EnadleItems.Count;

            var tmpY = ShowMessage(message, size, dialogColor, textColor, child: true);

            var x = 40 - (size.Width / 2);
            var y = 11 - (size.Height / 2);

            var indentItems = 3;

            var tmpI = 0;
            var key = new ConsoleKeyInfo();
            var selectedItem = 0;
            while (key.Key != ConsoleKey.Enter)
            {
                foreach (var item in EnadleItems)
                {
                    DrawText(item.Text, new Point(x + indentItems, y + tmpY + tmpI), textColor, selectedItem == tmpI ? SelectTextColor : ConsoleColor.Black);
                    tmpI++;
                }
                tmpI = 0;

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow) selectedItem -= 1;
                if (key.Key == ConsoleKey.DownArrow) selectedItem += 1;

                if (selectedItem < 0) selectedItem = EnadleItems.Count - 1;
                if (selectedItem > EnadleItems.Count - 1) selectedItem = 0;
            }
            ClearRegion(new Point(28, 8), new Size(28, 7));
            return EnadleItems[selectedItem].Value;
        }

        public static int ShowMessage(string message, Size size, ConsoleColor dialogColor = ConsoleColor.Gray, ConsoleColor textColor = ConsoleColor.White, bool child = false)
        {
            var width = size.Width;
            var height = size.Height;

            var x = 40 - (width / 2);
            var y = 11 - (height / 2);

            var indentText = 2;

            string[] dialogFrame = new string[height];

            dialogFrame[0] = $"╭{new string('─', width)}╮";
            dialogFrame[height - 1] = $"╰{new string('─', width)}╯";

            for (int i = 1; i < height - 1; i++)
            {
                dialogFrame[i] = $"│{new string(' ', width)}│";
            }

            for (int i = 0; i < height; i++)
            {
                DrawText(dialogFrame[i], new Point(x, y + i), dialogColor);
            }

            var tmpY = 1;
            if (message.Contains("\n"))
            {
                foreach (var text in message.Split('\n'))
                {
                    DrawText(text, new Point(x + indentText, y + tmpY), textColor);
                    tmpY++;
                }
            }
            else
            {
                DrawText(message, new Point(x + indentText, y + tmpY), textColor);
                tmpY++;
            }

            if (!child)
            {
                Thread.Sleep(GameConfig.MessageTimeout);
                ClearRegion(new Point(x, y), new Size(width + 2, height));
            }

            return tmpY;
        }

        private static void DrawText(string text, Point point, ConsoleColor color = ConsoleColor.Gray, ConsoleColor BackColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = BackColor;
            Console.SetCursorPosition(point.X, point.Y);
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void DrawImage(string[] date, Point point, ConsoleColor color = ConsoleColor.Gray)
        {
            for (int i = 0; i < date.Length; i++)
            {
                DrawText(date[i], new Point(point.X, point.Y + i), color);
            }
        }

        public static void DrawAvatars()
        {
            DrawImage(Images.AvatarDealer(), new Point(1, 1), ConsoleColor.DarkYellow);
            DrawImage(Images.AvatarPlayer(), new Point(1, 15), ConsoleColor.DarkGray);
        }

        public static void DrawDealerStatus(Person status)
        {
            DrawText("Dealer", new Point(1, 0), ConsoleColor.DarkYellow);
            DrawText(status.Name, new Point(1, 8), ConsoleColor.DarkYellow);
        }

        public static void DrawPlayerStatus(Person status)
        {
            ClearRegion(new Point(1, 13), new Size(10, 1));
            DrawText("Player", new Point(1, 14));
            DrawText(status.Name, new Point(1, 21));
            DrawText($"${status.Money.ToString()}", new Point(1, 13), ConsoleColor.DarkGreen);
        }

        public static void ClearRegion(Point point, Size size)
        {
            for (int i = 0; i < size.Height; i++)
            {
                DrawText(new string(' ', size.Width), new Point(point.X, point.Y + i));
            }
        }

        public static void ClearTable()
        {
            ClearRegion(new Point(12, 1), new Size(52, 24));
            ClearRegion(new Point(60, 15), new Size(12, 8));
        }

        public static GameStatus GameOver()
        {
            Console.Clear();
            DrawImage(Images.GameOver(), new Point(8, 4), ConsoleColor.DarkYellow);
            Thread.Sleep(GameConfig.MessageTimeout);

            List<DialogItem> dialogItems = new List<DialogItem> {
               new DialogItem{Text="New Game", Value=0 },
               new DialogItem{Text="EXIT    ", Value=1 }
            };
            var choice = ShowDialog("Actions:", dialogItems, new Size(12, 5), ConsoleColor.Cyan);
            if (choice != 0)
            {
                return GameStatus.Exit;
            }
            Console.Clear();
            return GameStatus.NewGame;

        }

        /// <summary>
        /// Free Key Buffer after Thread.Sleep
        /// </summary>
        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
    }
}
