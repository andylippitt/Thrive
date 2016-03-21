namespace Thrive.Players
{
    public class Wanderer : Player
    {
        int Steps = 0;

        public Wanderer(Game game) : base(game) { }

        public override void Initialize()
        {
            base.Initialize();
            Actors[0].Mass = 2000;
        }

        public override void Step()
        {
            base.Step();
            if (Steps++ % 100 == 0)
                this.MovementVector = Game.RandomPosition();
        }
    }
}
