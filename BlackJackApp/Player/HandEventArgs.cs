using System;

namespace BlackJackApp
{
    public class HandEventArgs : EventArgs
    {
        public BaseHand hand { get; private set; }
        public bool IsSplit { get; private set; }
        public bool DoubleBet { get; private set; }

        public HandEventArgs(bool isSplit, bool DoubleBet, BaseHand hand)
        {
            this.IsSplit = isSplit;
            this.DoubleBet = DoubleBet;
            this.hand = hand;
        }
    }
}