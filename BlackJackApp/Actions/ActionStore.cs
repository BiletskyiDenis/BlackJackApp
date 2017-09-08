using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Actions
{
    public abstract class ActionStore
    {
        public abstract void AddAction(Func<GamePlayer, Shoe, bool> action);
        public abstract Func<GamePlayer, Shoe, bool> DoAction(int actionNumber);
    }
}

