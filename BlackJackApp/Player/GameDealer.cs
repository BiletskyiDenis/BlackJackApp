using BlackJackApp.Helpers;
using System;
using System.Linq;

namespace BlackJackApp
{
    public class GameDealer : Player<BaseHand>
    {
        private BaseHand hand = null;
        private Person person = null;

        public event EventHandler<HandEventArgs> OnChangeCards;
        public event EventHandler OnInsurance;

        public override void Drop()
        {
            hand.Cards.Clear();
        }

        public override int GetCount()
        {
            return hand.Cards.Count;
        }

        public override BaseHand GetHand()
        {
            return hand;
        }

        public override int GetScore()
        {
            var cardsScore = hand.Cards.Sum(c => c.Vaue);
            var aceCount = hand.Cards.Count(c => c.CardFace == Face.Ace);

            if (aceCount > 0 && cardsScore + 10 <= 21)
            {
                cardsScore += 10;
            }

            return cardsScore;
        }

        public override void Interact(Person person, BaseHand baseHand)
        {
            this.person = person;
            this.hand = baseHand;
        }

        public override void TakeCard(Card card)
        {
            if (GetCount() ==1) card.IsHidden = true;

            hand.Cards.Add(card);
            
            OnChangeCards?.Invoke(this, new HandEventArgs(false,false, GetHand()));

            if (hand.Cards[0].CardFace == Face.Ace && GetCount()== 2)
            {
                OnInsurance?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ShowHiddenCard()
        {
            var card = hand.Cards.Where(c => c.IsHidden).FirstOrDefault();

            if (card != null) card.IsHidden = false;

            OnChangeCards?.Invoke(this, new HandEventArgs(false, false, GetHand()));

        }

        public override int GetMaxCards()
        {
            return 5;
        }

        public override Person GetPerson()
        {
            return person;
        }
    }
}
