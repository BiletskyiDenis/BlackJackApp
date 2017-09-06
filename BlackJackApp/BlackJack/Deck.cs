using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackApp
{
    public abstract class Deck
    {
        public abstract IEnumerable<Card> NewDeck();
    }

    public class GameDeck : Deck
    {
        public override IEnumerable<Card> NewDeck()
        {
            return CreateDeck();
        }

        private IEnumerable<Card> CreateDeck()
        {
            var deck = new List<Card>();
            for (int k = 0; k < 8; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        var cardValue = j + 1 < 10 ? j + 1 : 10;

                        deck.Add(new Card((Face)j, cardValue, (Suit)i));
                    }
                }
            }
            return RandomizeDeck(deck);
        }

        private IEnumerable<Card> RandomizeDeck(IEnumerable<Card> deck)
        {
            var rn = new Random();
            var randDeck = deck.OrderBy(x => rn.Next());
            return randDeck;
        }

    }

}