using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp
{
    public class PlayerHand : BaseHand
    {
        public bool IsSplit { get; set; }

        public bool Insurance { get; set; }

        public bool DoubleBet { get; set; }

        public bool DoubleAce { get; set; }

        public bool Bust { get; set; }

        public int Bet { get; set; }
    }

}
