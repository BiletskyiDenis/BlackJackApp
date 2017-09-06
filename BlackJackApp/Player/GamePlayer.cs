using BlackJackApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackApp
{
    public class GamePlayer : Player<PlayerHand>
    {
        private List<PlayerHand> hands = null;
        private Person person = null;
        private int activeHand;

        public event EventHandler<HandEventArgs> OnSetInsurance;
        public event EventHandler<HandEventArgs> OnSetBet;
        public event EventHandler<HandEventArgs> OnDoubleBet;
        public event EventHandler<HandEventArgs> OnSetSplit;
        public event EventHandler<HandEventArgs> OnChangeCards;
        public event EventHandler<HandEventArgs> OnBust;
        public event EventHandler<HandEventArgs> OnPush;
        public event EventHandler<HandEventArgs> OnWin;
        public event EventHandler<HandEventArgs> OnLose;
        public event EventHandler<HandEventArgs> OnBlackJack;
        public event EventHandler OnChangeMoney;

        public override void Drop()
        {
            activeHand = 0;
            var currentHand = GetHand();

            currentHand.Bust = false;
            currentHand.DoubleAce = false;
            currentHand.DoubleBet = false;
            currentHand.DoubleAce = false;
            currentHand.Insurance = false;
            currentHand.IsSplit = false;
            currentHand.Cards.Clear();
            currentHand.Bet = 0;
            currentHand.DoubleAce = false;

            if (hands.Count > 1)
            {
                hands.Remove(hands[1]);
            }

        }

        public override int GetCount()
        {
            return hands[activeHand].Cards.Count();
        }

        public override PlayerHand GetHand()
        {
            return hands[activeHand];
        }

        public PlayerHand GetHand(int handNumber)
        {
            return hands[handNumber];
        }

        public bool NextHand()
        {
            activeHand++;
            if (activeHand > hands.Count - 1)
            {
                activeHand = 0;
                return false;
            }
            return true;
        }

        public override int GetScore()
        {
            var cardsScore = GetHand().Cards.Sum(c => c.Vaue);
            var aceCount = GetHand().Cards.Count(c => c.CardFace == Face.Ace);

            if (aceCount > 0 && cardsScore + 10 <= 21)
            {
                cardsScore += 10;
            }

            return cardsScore;
        }

        public override void Interact(Person person, PlayerHand playerHand)
        {
            this.person = person;
            this.hands = new List<PlayerHand>();
            this.hands.Add(playerHand);
        }

        public override void TakeCard(Card card)
        {
            GetHand().Cards.Add(card);
            OnChangeCards?.Invoke(this, new HandEventArgs(GetHand().IsSplit, GetHand().DoubleBet, GetHand()));
        }

        public int GetHandsCount()
        {
            return hands.Count();
        }

        public bool CheckMoney()
        {
            return person.Money - GetHand().Bet >= 0;
        }

        public bool CheckMoney(int bet)
        {
            return person.Money - bet >= 0;
        }

        public bool CanSplit()
        {
            return (GetCount() > 1 && GetHand().Cards[0].CardFace == GetHand().Cards[1].CardFace && !GetHand().IsSplit && CheckMoney());
        }

        public void SetBust()
        {
            GetHand().Bust = true;
            OnBust?.Invoke(this, new HandEventArgs(GetHand().IsSplit, GetHand().DoubleBet, GetHand()));
        }

        public void SetInsurance()
        {
            GetHand().Insurance = true;
            OnSetInsurance?.Invoke(this, new HandEventArgs(GetHand().IsSplit, GetHand().DoubleBet, GetHand()));
        }

        public void SetBet(int money)
        {
            GetHand().Bet = money;
            TakeMoney(money);
            OnSetBet?.Invoke(this, new HandEventArgs(GetHand().IsSplit, GetHand().DoubleBet, GetHand()));
        }

        public void SetDoubleBet()
        {
            GetHand().DoubleBet = true;
            TakeMoney(GetHand().Bet);
            OnDoubleBet?.Invoke(this, new HandEventArgs(GetHand().IsSplit, GetHand().DoubleBet, GetHand()));
        }

        public void TakeMoney(int money)
        {
            person.Money -= money;
            OnChangeMoney?.Invoke(this, EventArgs.Empty);
        }

        public void PutMoney(int money)
        {
            person.Money += money;
            OnChangeMoney?.Invoke(this, EventArgs.Empty);
        }

        public override int GetMaxCards()
        {
            return GetHand().DoubleAce ? 3 : 5;
        }

        public void Split(Shoe shoe)
        {
            var currentHand = GetHand();
            var newHand = new PlayerHand();
            var tempCard = currentHand.Cards[1];
            var currentBet = currentHand.Bet;

            if (currentHand.Cards[0].CardFace == Face.Ace && tempCard.CardFace == Face.Ace)
            {
                currentHand.DoubleAce = true;
                newHand.DoubleAce = true;
            }

            currentHand.Cards.Remove(tempCard);
            newHand.Cards = new List<Card>();
            newHand.HandNumber = currentHand.HandNumber + 1;

            newHand.IsSplit = true;
            currentHand.IsSplit = true;
            hands.Add(newHand);
            newHand.Cards.Add(tempCard);

            OnSetSplit?.Invoke(this, new HandEventArgs(currentHand.IsSplit, currentHand.DoubleBet, currentHand));
            OnChangeCards?.Invoke(this, new HandEventArgs(currentHand.IsSplit, currentHand.DoubleBet, currentHand));
            NextHand();
            SetBet(currentBet);
            OnChangeCards?.Invoke(this, new HandEventArgs(GetHand().IsSplit, GetHand().DoubleBet, GetHand()));
            NextHand();
            this.GetCardFrom(shoe);
            NextHand();
            this.TakeCard(tempCard);
            NextHand();
        }

        public override Person GetPerson()
        {
            return person;
        }

        public void Push()
        {
            var currentHand = GetHand();

            var pushBet = currentHand.Bet;
            if (currentHand.DoubleBet)
            {
                pushBet *= 2;
            }
            if (currentHand.Insurance)
            {
                pushBet -= pushBet / 2;
            }
            PutMoney(pushBet);


            OnPush?.Invoke(this, new HandEventArgs(currentHand.IsSplit, currentHand.DoubleBet, GetHand()));
        }

        public void Win()
        {
            var currentHand = GetHand();

            if (CheckBlackJack())
            {
                BlackJack();
                return;
            }

            var winBet = currentHand.Bet;

            winBet += currentHand.Bet;

            if (currentHand.DoubleBet)
            {
                winBet *= 2;
            }

            if (currentHand.Insurance)
            {
                winBet -= (winBet / 2) / 2;
            }

            OnWin?.Invoke(this, new HandEventArgs(currentHand.IsSplit, currentHand.DoubleBet, GetHand()));

            PutMoney(winBet);
        }

        public void BlackJack()
        {
            var currentHand = GetHand();
            var winBet = currentHand.Bet;

            winBet += (int)(currentHand.Bet * 1.5);

            OnBlackJack?.Invoke(this, new HandEventArgs(currentHand.IsSplit, currentHand.DoubleBet, GetHand()));

            PutMoney(winBet);
        }

        public void Lose()
        {
            var currentHand = GetHand();
            var winBet = currentHand.Bet;
            if (currentHand.Insurance)
            {
                if (currentHand.DoubleBet)
                {
                    winBet *= 2;
                }
                winBet /= 2;
                PutMoney(winBet);
            }

            if (!currentHand.Bust)
            {
                OnLose?.Invoke(this, new HandEventArgs(currentHand.IsSplit, currentHand.DoubleBet, GetHand()));
            }
        }

        public bool CheckBlackJack()
        {
            var currentHand = GetHand();
            if (GetCount() > 0 && GetCount() < 3)
            {
                return GetScore() == 21;
            }

            return false;
        }
    }
}
