using BlackJackApp.Helpers;
using System;

namespace BlackJackApp.UI.Messages
{
    public class Messages : Message
    {
        public override void BlackJack()
        {
            var side = player.GetHand().IsSplit ? SplitSide(player.GetHand().HandNumber) : string.Empty;
            UI.ShowMessage("BLACKJACK!" + side, new Size(12 + side.Length, 3), textColor: ConsoleColor.Green);
        }

        public override void Bust()
        {
            var side = player.GetHand().IsSplit ? SplitSide(player.GetHand().HandNumber) : string.Empty;
            UI.ShowMessage("BUST!" + side, new Size(7 + side.Length, 3), ConsoleColor.DarkRed, ConsoleColor.Red);
        }

        public override void Lose()
        {
            var side = player.GetHand().IsSplit ? SplitSide(player.GetHand().HandNumber) : string.Empty;
            UI.ShowMessage("Dealer WIN!" + side, new Size(13 + side.Length, 3));
        }

        public override void Push()
        {
            var side = player.GetHand().IsSplit ? SplitSide(player.GetHand().HandNumber) : string.Empty;
            UI.ShowMessage("PUSH!" + side, new Size(7 + side.Length, 3), ConsoleColor.DarkYellow, ConsoleColor.Yellow);
        }

        public override void Win()
        {
            var side = player.GetHand().IsSplit ? SplitSide(player.GetHand().HandNumber) : string.Empty;
            UI.ShowMessage("You WIN!" + side, new Size(10 + side.Length, 3), textColor: ConsoleColor.Yellow);
        }

        public Messages(GamePlayer player):base(player)
        {

        }
    }
}
