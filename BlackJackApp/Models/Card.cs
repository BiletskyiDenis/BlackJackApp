using BlackJackApp.Helpers;

namespace BlackJackApp
{
    public class Card
    {
        public Face CardFace { get; private set; }
        public Suit CardSuit { get; private set; }
        public int Vaue { get; set; }
        public bool IsHidden { get; set; }
        public Card(Face cardFace, int vaue, Suit cardSuit)
        {
            this.CardFace = cardFace;
            this.Vaue = vaue;
            this.CardSuit = cardSuit;
        }
    }
}