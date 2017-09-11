using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;

namespace BlackJackApp.UI.Dialogs
{
    public class SplitDialog : Dialog
    {
        private readonly GamePlayer player;
        private readonly Shoe shoe;
        public override int Show()
        {
            List<DialogItem> dialogItems = new List<DialogItem> {
               new DialogItem{Text="NO Thanks", Value=0},
               new DialogItem{Text="YES - Split", Value=1}
            };
            if (MainUI.ShowDialog("Split Cards?", dialogItems, new Size(14, 5), dialogColor: ConsoleColor.Yellow) != 1)
            { return 0; }

            player.Split(shoe);

            return 1;
        }
        public SplitDialog(GamePlayer player, Shoe shoe)
        {
            this.player = player;
            this.shoe = shoe;
        }
    }

}
