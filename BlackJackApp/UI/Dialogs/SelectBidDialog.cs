using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;

namespace BlackJackApp.UI.Dialogs
{
    public class SelectBidDialog : Dialog
    {
        private readonly GamePlayer player;
        public override int Show()
        {
            List<DialogItem> dialogItems = new List<DialogItem> {
               new DialogItem{Text="$100", Value=100, Enabled= player.CheckMoney(100)},
               new DialogItem{Text="$200", Value=200, Enabled= player.CheckMoney(200) },
               new DialogItem{Text="$300", Value=300, Enabled= player.CheckMoney(300) },
               new DialogItem{Text="$500", Value=500, Enabled= player.CheckMoney(500) },
            };

            var bet = UI.ShowDialog("Select a bid:", dialogItems, new Size(15, 7), dialogColor: ConsoleColor.Yellow);
            player.SetBet(bet);
            return 0;
        }

        public SelectBidDialog(GamePlayer player)
        {
            this.player = player;
        }
    }

}
