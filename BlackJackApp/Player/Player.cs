namespace BlackJackApp
{
    public abstract class Player<T> where T : BaseHand
    {
        public abstract void Interact(Person person, T baseHand);
        public abstract int GetScore();
        public abstract int GetCount();
        public abstract int GetMaxCards();
        public abstract T GetHand();
        public abstract Person GetPerson();
        public abstract void TakeCard(Card card);
        public abstract void Drop();
    }


}
