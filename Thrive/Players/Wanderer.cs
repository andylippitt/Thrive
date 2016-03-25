using Thrive.Actors;
using Thrive.Geometry;

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

            if (!IsDead())
                Sense();
        }

        private void Sense()
        {
            var center = this.CenterPointOfActors;
            var zero = this.Actors[0] as Cell;
            var closest = double.MaxValue;

            zero.Note = null;
            zero.Color = Actor.ActorColor.Green;

            foreach (var actor in Game.Actors)
            {
                if (actor.CanBeEatenBy(zero) && actor is Food)
                {
                    var distance = Geo.Distance(actor.Position, center);
                    if (distance < closest)
                    {
                        closest = distance;
                        if (distance < 200)
                        {
                            var direction = center.ToVector() - actor.Position.ToVector();
                            direction = BasicVector.VectorUtil.Normalize(direction);
                            var target = center.ToVector() + direction * -200;

                            MovementVector.X = (int)target.X;
                            MovementVector.Y = (int)target.Y;

                            zero.Note = ((int)closest).ToString();
                            zero.Note = "Grazing";
                        }
                    }
                }
            }
        }
    }
}
