namespace Thrive.Behaviors
{
    using Thrive.Common;

    public class BehaviorCollection : ThriveCollection<Behavior>
    {
        private Game Game;
        public BehaviorCollection(Game game)
        {
            Game = game;
        }
        public override void Add(Behavior behavior)
        {
            behavior.Game = Game;
            base.Add(behavior);
        }

        public void Step()
        {
            for (int i = 0; i < Set.Count; i++)
                Set[i].Step();
        }
    }
}
