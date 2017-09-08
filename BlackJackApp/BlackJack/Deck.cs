using System.Collections.Generic;

namespace BlackJackApp
{
    public abstract class Deck
    {
        public abstract IEnumerable<Card> NewDeck();
    }

}