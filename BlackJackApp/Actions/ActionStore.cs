using System;
using System.Collections.Generic;
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

    public class PlayerActonsStore : ActionStore
    {
        private Dictionary<int, Func<GamePlayer, Shoe, bool>> palyerActions = new Dictionary<int, Func<GamePlayer, Shoe, bool>>();

        public override void AddAction(Func<GamePlayer, Shoe, bool> action)
        {
            palyerActions.Add(palyerActions.Count, action);
        }

        public override Func<GamePlayer, Shoe, bool> DoAction(int actionNumber)
        {
            return palyerActions[actionNumber];
        }

    }
}

