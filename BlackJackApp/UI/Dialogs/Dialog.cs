using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.UI.Dialogs
{
    public abstract class Dialog
    {
        public abstract int Show();
        public virtual string SplitSide(int side)
        {
            return side == 1 ? " <LEFT" : " RIGHT>";
        }
    }

}
