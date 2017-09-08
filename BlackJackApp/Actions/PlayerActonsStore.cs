using System;
using System.Collections.Generic;

namespace BlackJackApp.Actions
{
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

