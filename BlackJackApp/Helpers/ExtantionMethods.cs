using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp
{
    public static class ExtantionMethods
    {
        public static void GetCardFrom<T> (this Player<T> player, Shoe shoe) where T:BaseHand
        {
            player.TakeCard(shoe.GetCard());
        }
    }
}
