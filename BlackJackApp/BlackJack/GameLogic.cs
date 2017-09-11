using System;
using BlackJackApp.Helpers;
using BlackJackApp.UI.Dialogs;
using BlackJackApp.Actions;
using BlackJackApp.UI.Messages;
using BlackJackApp.UI;

namespace BlackJackApp
{
    public class GameLogic
    {
        private PlayerActonsStore playerActons = new PlayerActonsStore();
        private Messages message = null;
        private GameDealer dealer = null;
        private GamePlayer player = null;
        private Shoe shoe = null;
        private GameStatus gameStatus = GameStatus.Continue;

        public GameStatus StartGame()
        {
            OnStartGame();

            while (GetGameStatus() == GameStatus.Continue)
            {
                SelectBid();
                StartMatch();
                CheckSplit();
                PlayerActions();
                CheckCards();
                EndSMatch();
            }
            return gameStatus;
        }

        private void OnStartGame()
        {
            MainUI.DrawMain();
            MainUI.DrawAvatars();
            MainUI.DrawDealerStatus(dealer.GetPerson());
            MainUI.DrawPlayerStatus(player.GetPerson());
            MainUI.DrawDeck(shoe.GetCount(), shoe.GetStartCount());
            MainUI.ShowMessage("Welcome to Blackjack Game", new Size(27, 3));
        }

        private void StartMatch()
        {
            dealer.GetCardFrom(shoe);

            for (int i = 0; i < 2; i++)
            {
                player.GetCardFrom(shoe);
            }

            dealer.GetCardFrom(shoe);
        }

        private void CheckCards()
        {
            dealer.ShowHiddenCard();

            while (!player.GetHand().Bust && (dealer.GetScore() < 17 && dealer.GetCount() < dealer.GetMaxCards()))
            {
                dealer.GetCardFrom(shoe);
            }

            var dealerScore = dealer.GetScore();

            do
            {
                var playerScore = player.GetScore();
                var playerBust= player.GetHand().Bust;

                if (playerScore == dealerScore && !playerBust) { player.Push(); continue; }
                if((dealerScore > 21 && playerScore <= 21) || (playerScore <= 21 && playerScore > dealerScore) && !playerBust) { player.Win(); continue; }
                if ((playerScore > 21 && dealerScore <= 21) || (dealerScore <= 21 && dealerScore > playerScore) || playerBust) { player.Lose(); continue; }

            } while (player.NextHand());

        }

        private void EndSMatch()
        {
            dealer.Drop();
            player.Drop();
            MainUI.ClearTable();
        }

        private void PlayerActions()
        {
            var stand = false;

            while (!stand && player.GetScore() != 21 && player.GetCount()<=player.GetMaxCards())
            {
                var action = new ActionsDialog(player).Show();
                stand = playerActons.DoAction(action).Invoke(player, shoe);
            }

            stand = false;

            if (player.NextHand()) PlayerActions();
        }

        private void CheckSplit()
        {
            if (player.CanSplit())
            {
                new SplitDialog(player, shoe).Show();
            }
        }

        private void SelectBid()
        {
            new SelectBidDialog(player).Show();
        }

        private GameStatus GetGameStatus()
        {

            if (!player.CheckMoney(100))
            {
                gameStatus = MainUI.GameOver();
                return gameStatus;
            }

            return GameStatus.Continue;
        }

        public void Init(GameDealer dealer, GamePlayer player, GameShoe shoe)
        {
            this.dealer = dealer;
            this.player = player;
            this.shoe = shoe;

            message = new Messages(player);

            playerActons.AddAction((p, s) => new StayPlayerAction().DoAction());
            playerActons.AddAction((p, s) => new HitPlayerAction(p, s).DoAction());
            playerActons.AddAction((p, s) => new DoubleDownPlayerAction(p, s).DoAction());

            player.OnSetInsurance += MainUI.DrawCorn;
            player.OnSetBet += MainUI.DrawCorn;
            player.OnDoubleBet += MainUI.DrawCorn;
            player.OnChangeCards += MainUI.DrawCards;
            player.OnChangeMoney += (obj, e) => { MainUI.DrawPlayerStatus(((GamePlayer)obj).GetPerson()); };
            player.OnSetSplit += (obj, e) => { MainUI.ClearRegion(new Point(15, 15), new Size(50, 7)); };

            dealer.OnChangeCards += UI.MainUI.DrawCards;
            shoe.OnChangeDeckSize += (obj, e) => { UI.MainUI.DrawDeck(shoe.GetCount(), shoe.GetStartCount()); };


            player.OnWin += (obj, e) => { message.Win(); }; 
            player.OnBust += (obj, e) => { message.Bust(); };
            player.OnLose += (obj, e) => { message.Lose(); };
            player.OnBlackJack += (obj, e) => { message.BlackJack(); };
            player.OnPush += (obj, e) => { message.Push(); };

            dealer.OnInsurance += (obj, e) => { new InsuranceDialog(player).Show(); };
        }
    }
}
