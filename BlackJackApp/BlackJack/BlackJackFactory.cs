using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp
{
    public class BlackJackFactory
    {
        public virtual GamePlayer CreatePlayer(string name, int money)
        {
            var person = new Person(name, money);
            var hand = new PlayerHand();
            var player = new GamePlayer();

            hand.Cards = new List<Card>();
            hand.HandNumber = 1;
            player.Interact(person, hand);

            return player;
        }

        public virtual GameDealer CreateDealer(string name, int money)
        {
            var person = new Person(name, money);
            var hand = new BaseHand();
            var player = new GameDealer();

            hand.Cards = new List<Card>();
            player.Interact(person, hand);

            return player;
        }

        public virtual Shoe CreateShoe()
        {
            var shoe = new GameShoe();
            var deck = new GameDeck();
            shoe.LoadDeck(deck);

            return shoe;
        }

        public virtual GameLogic CreateGameLogic(GameDealer dealer, GamePlayer player, Shoe shoe)
        {
            var gameLogic = new GameLogic();
            gameLogic.Init(dealer, player, (GameShoe)shoe);
            return gameLogic;
        }
    }
}
