using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;

namespace BlackJackApp.UI.Dialogs
{
    public class ActionsDialog : Dialog
    {
        private readonly GamePlayer player;
        public override int Show()
        {
            var side = player.GetHand().IsSplit ? SplitSide(player.GetHand().HandNumber) : string.Empty;

            List<DialogItem> dialogItems = new List<DialogItem> {
               new DialogItem{Text="Stand      ", Value=0 },
               new DialogItem{Text="Hit        ", Value=1 },
               new DialogItem{Text="Double Down", Value=2, Enabled=player.CheckMoney() && !player.GetHand().DoubleBet }
            };

            return MainUI.ShowDialog("Actions:" + side, dialogItems, new Size(17, 6), dialogColor: ConsoleColor.DarkGreen);
        }
        public ActionsDialog(GamePlayer player)
        {
            this.player = player;
        }
    }

}
