using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.UI.Messages
{
    public abstract class Message
    {
        protected readonly GamePlayer player;

        public abstract void BlackJack();
        public abstract void Bust();
        public abstract void Lose();
        public abstract void Push();
        public abstract void Win();

        public virtual string SplitSide(int side)
        {
            return side == 1 ? " <LEFT" : " RIGHT>";
        }

        public Message(GamePlayer player)
        {
            this.player = player;
        }
    }
}
