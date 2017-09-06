namespace BlackJackApp
{
    public class Person
    {
        public string Name { get; set; }
        public int Money { get; set; }

        public Person(string name, int money)
        {
            this.Name = name;
            this.Money = money;
        }
    }
}