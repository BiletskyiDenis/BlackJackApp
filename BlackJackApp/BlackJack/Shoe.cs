using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackApp
{
    public abstract class Shoe
    {
        public abstract Card GetCard();
        public abstract void LoadDeck(Deck deck);
        public abstract int GetCount();
        public abstract int GetStartCount();
    }

    public class GameShoe : Shoe
    {
        private Queue<Card> queueCards = null;
        private Deck deck = null;
        private int startCount;

        public event EventHandler OnChangeDeckSize;

        public override Card GetCard()
        {
            if (queueCards.Count < startCount / 2)
            {
                LoadDeck(deck);
            }

            OnChangeDeckSize?.Invoke(this, EventArgs.Empty);
            return queueCards.Dequeue();
        }

        public override int GetCount()
        {
            return queueCards.Count;
        }

        public override int GetStartCount()
        {
            return startCount;
        }

        public override void LoadDeck(Deck deck)
        {
            this.deck = deck;
            queueCards = new Queue<Card>();

            var newDeck = deck.NewDeck();
            startCount = newDeck.Count();

            foreach (var item in newDeck)
            {
                queueCards.Enqueue(item);
            }

            OnChangeDeckSize?.Invoke(this, EventArgs.Empty);
        }

    }
}