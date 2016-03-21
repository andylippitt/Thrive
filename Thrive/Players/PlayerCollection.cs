namespace Thrive.Players
{
    using Common;

    public class PlayerCollection : ThriveCollection<Player>
    {
        private Game Game;
        public PlayerCollection(Game game)
        {
            Game = game;
        }

        public override void Add(Player player)
        {
            player.Game = Game;
            base.Add(player);
        }

        public void Step()
        {
            for (int i = 0; i < Set.Count; i++)
                Set[i].Step();
        }

        public void Report()
        {
            for (int i = 0; i < Set.Count; i++)
                Set[i].Report();
        }
    }
}
