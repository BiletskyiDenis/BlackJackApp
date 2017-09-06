namespace BlackJackApp.Actions
{
    public class DoubleDownPlayerAction : PlayerAction
    {
        private readonly GamePlayer player;
        private readonly Shoe shoe;
        public override bool DoAction()
        {
            player.SetDoubleBet();
            player.GetCardFrom(shoe);

            if (player.GetScore() > 21)
            {
                player.SetBust();
                return true;
            }

            if (player.GetScore() == 21 || player.GetCount() == player.GetMaxCards())
            {
                return true;
            }
            return false;
        }
        public DoubleDownPlayerAction(GamePlayer player, Shoe shoe)
        {
            this.player = player;
            this.shoe = shoe;
        }
    }




}
