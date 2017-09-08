namespace BlackJackApp
{
    public abstract class Shoe
    {
        public abstract Card GetCard();
        public abstract void LoadDeck(Deck deck);
        public abstract int GetCount();
        public abstract int GetStartCount();
    }
}