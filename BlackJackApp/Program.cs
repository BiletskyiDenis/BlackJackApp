using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Helpers;

namespace BlackJackApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Title = "BlackJack Game.";

            var gameStatus = GameStatus.NewGame;
            while (gameStatus == GameStatus.NewGame)
            {
                gameStatus = CreateGame();
            }

        }

        static GameStatus CreateGame()
        {
            var factory = new BlackJackFactory();
            var player = factory.CreatePlayer("Homer", 3000);
            var dealer = factory.CreateDealer("Bender", 100000);
            var shoe = factory.CreateShoe();
            var gameLogic = factory.CreateGameLogic(dealer, player, shoe);

            return gameLogic.StartGame();
        }
    }
}
