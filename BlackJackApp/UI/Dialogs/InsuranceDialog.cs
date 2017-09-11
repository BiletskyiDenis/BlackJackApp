using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;

namespace BlackJackApp.UI.Dialogs
{
    public class InsuranceDialog : Dialog
    {
        private readonly GamePlayer player;
        public override int Show()
        {
            List<DialogItem> dialogItems = new List<DialogItem> {
               new DialogItem{Text="NO ", Value=0 },
               new DialogItem{Text="YES", Value=1 }
            };

            var answ = MainUI.ShowDialog("Insurance?", dialogItems, new Size(12, 5), dialogColor: ConsoleColor.DarkMagenta);
            if (answ == 1)
            {
                player.SetInsurance();
            }
            return answ;
        }

        public InsuranceDialog(GamePlayer player)
        {
            this.player = player;
        }
    }

}
