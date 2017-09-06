using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.UI
{
    public static class Images
    {
        public static string[] MainLogo()
        {
            var mainLogo = new[] {
                 "╭━━━━━╮╱╱╭━╮╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╭━╮╱╱╱╱╱╱╱╱╭━╮╱╱╱╱╱╱╱╱╱╱╱╱╱╭━╮    ",
                 "┃ ╭━╮ ┃╱╱┃ ┃╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱┃ ┃╱╱╱╱╱╱╱╱┃ ┃╱╱╱╱╱╱╱╱╱╱╱╱╱┃ ┃    ",
                 "┃ ╰━╯ ╰━╮┃ ┃╱ ╭━━━━━╮╭━━━━╮┃ ┃ ╭━╮╱╱╱╱┃ ┃╭━━━━━╮╭━━━━╮┃ ┃ ╭━╮",
                 "┃ ╭━━╮  ┃┃ ┃╱ ┃ ╭━╮ ┃┃ ╭━━╯┃ ╰━╯ ╯╭━╮ ┃ ┃┃ ╭━╮ ┃┃ ╭━━╯┃ ╰━╯ ╯",
                 "┃ ╰━━╯  ┃┃ ╰━╮┃ ╭━╮ ┃┃ ╰━━╮┃ ╭━╮ ╮┃ ╰━╯ ┃┃ ╭━╮ ┃┃ ╰━━╮┃ ╭━╮ ╮",
                 "╰━━━━━━━╯╰━━━╯╰━╯ ╰━╯╰━━━━╯╰━╯ ╰━╯╰━━━━━╯╰━╯ ╰━╯╰━━━━╯╰━╯ ╰━╯", };

            return mainLogo;
        }

        public static string[] PressEnter()
        {
            var pressEnter = new[] {
                       "█▀▀█ █▀▀█ █▀▀ █▀▀ █▀▀    █▀▀ █▀▀▄ ▀▀█▀▀ █▀▀ █▀▀█",
                       "█  █ █▄▄▀ █▀▀ ▀▀█ ▀▀█    █▀▀ █  █   █   █▀▀ █▄▄▀",
                       "█▀▀▀ ▀ ▀▀ ▀▀▀ ▀▀▀ ▀▀▀    ▀▀▀ ▀  ▀   ▀   ▀▀▀ ▀ ▀▀" };

            return pressEnter;
        }

        public static string[] MainFooter()
        {
            var footer = new[] {
                         "╭───────╮  ╭───────╮",
                         "│ K     │  │ A     │",
                         "│       │  │       │",
                         "│   ♥   │  │   ♦   │",
                         "│       │  │       │",
                         "│     K │  │     A │",
                         "╰───────╯  ╰───────╯" };

            return footer;
        }

        public static string[] HiddenCard()
        {
            var hiddenCard = new[] {
                       "╭───────╮",
                       "│░░░░░░░│",
                       "│░░░░░░░│",
                       "│░░░░░░░│",
                       "│░░░░░░░│",
                       "│░░░░░░░│",
                       "╰───────╯"};

            return hiddenCard;
        }

        public static string[] Card(Card card)
        {
            var value = GetCardValue(card.CardFace);
            var space = (int)card.CardFace + 1 != 10 ? " " : string.Empty;

            var cardSuites = new[] { "♥", "♦", "♠", "♣" };
            string cardTextImage = $"╭───────╮,│ {value + space}    │,│       │,│   {cardSuites[(int)card.CardSuit]}   │,│       │,│    {space + value} │,╰───────╯";

            return cardTextImage.Split(',');
        }

        public static string[] Deck(int count)
        {
            var deck = new[] {
                         $"╭───────{new string('╮', count)}",
                         $"│░░░░░░░{new string('│', count)}",
                         $"│░░░░░░░{new string('│', count)}",
                         $"│░░░░░░░{new string('│', count)}",
                         $"│░░░░░░░{new string('│', count)}",
                         $"│░░░░░░░{new string('│', count)}",
                         $"╰───────{new string('╯', count)}" };

            return deck;
        }

        public static string[] Corn(int value, int count)
        {
            var textValue = value.ToString();
            if (value < 100)
            {
                textValue += " ";
            }
            var corn = new[] {
                    $"{new string('╭', count)}━━━╮",
                    $"{new string('┃', count)}{textValue}┃",
                    $"{new string('╰', count)}━━━╯" };
            return corn;
        }

        public static string[] AvatarDealer()
        {
            var dealer = new[] {
                 " ╭━━━┻━━━╮ ",
                 "╭┻━━━━━━━┻╮",
                 "┃┃ ▄ ┃ ▄ ┃┃",
                 "╰┳━━━━━━━┳╯",
                 " ┃╭━━━━━╮┃ ",
                 " ┃╰━━━━━╯┃ ",
                 "━┻━━━━━━━┻━"};
            return dealer;
        }

        public static string[] AvatarPlayer()
        {
            var Player = new[] {
                 " ╭━━━━━╮ ",
                 " ┃┊┊╭━╮┻╮",
                 " ╱╲┊┃▋┃▋┃",
                 "╭┻┊┊╰━┻━╮",
                 "╰┳┊╭━━━┳╯",
                 " ┃┊┃╰━━┫ "};
            return Player;
        }

        public static string[] GameOver()
        {
            var gameOver = new[] {
                      "                           ▄▀▄     ▄▀▄                             ",
                      "                          ▄█░░▀▀▀▀▀░░█▄                            ",
                      "                      ▄▄  █░░░░░░░░░░░█  ▄▄                        ",
                      "                     █▄▄█ █░░▀░░┬░░▀░░█ █▄▄█                       ",
                      "╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱",
                      " ╭━━━━━╮╭━━━━━╮╭━━╮╱╱╱╭━━╮╭━━━━╮╱╱╭━━━━━╮╭━╮╱╱╱╱╱╭━╮╭━━━━╮╭━━━━━━╮ ",
                      " ┃ ╭━╮ ┃┃ ╭━╮ ┃┃  ╰━━━╯  ┃┃ ╭━━╯╱╱┃ ╭━╮ ┃┃ ╰━╮ ╭━╯ ┃┃ ╭━━╯┃ ╭━━╮ ┃ ",
                      " ┃ ┃╱╰━╯┃ ┃╱┃ ┃┃ ╭━╮ ╭━╮ ┃┃ ╰━━╮╱╱┃ ┃╱┃ ┃╰━╮ ┃ ┃ ╭━╯┃ ╰━━╮┃ ╰━━╯ ┃ ",
                      " ┃ ┃╭━━╮┃ ╰━╯ ┃┃ ┃ ┃ ┃ ┃ ┃┃ ╭━━╯╱╱┃ ┃╱┃ ┃ ╱┃ ╰━╯ ┃╱ ┃ ╭━━╯┃ ╭━╮ ╭╯ ",
                      " ┃╰┻━┃ ┃┃ ╭━╮ ┃┃ ┃ ┃ ┃ ┃ ┃┃ ╰━━╮╱╱┃ ╰━╯ ┃╱ ╰━╮ ╭━╯╱ ┃ ╰━━╮┃ ┃ ┃ ╰╮ ",
                      " ╰━━━━━╯╰━╯╱╰━╯╰━╯ ╰━╯ ╰━╯╰━━━━╯╱╱╰━━━━━╯╱╱╱╱╰━╯╱╱╱╱╰━━━━╯╰━╯ ╰━━╯ ",
                      "╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱╱"};
            return gameOver;
        }

        private static string GetCardValue(Face face)
        {
            var cardFace = (int)face + 1;

            if (cardFace > 10 || cardFace == 1) return (face.ToString())[0].ToString();
            return cardFace.ToString();
        }
    }
}
