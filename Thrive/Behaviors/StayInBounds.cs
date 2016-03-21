namespace Thrive.Behaviors
{
    using System;

    public class StayInBounds: Behavior
    {
        public override void Step()
        {
            for (int i=0; i<Game.Actors.Count; i++)
            {
                var actor = Game.Actors[i];

                if (actor.Position.X < actor.Radius)
                    actor.Position.X = (int)actor.Radius;

                if ((actor.Position.X + actor.Radius) > Game.Configuration.Dimensions.Width)
                    actor.Position.X = (int)(Game.Configuration.Dimensions.Width - actor.Radius);

                if (actor.Position.Y < actor.Radius)
                    actor.Position.Y = (int)actor.Radius;

                if ((actor.Position.Y + actor.Radius) > Game.Configuration.Dimensions.Height)
                    actor.Position.Y = (int)(Game.Configuration.Dimensions.Height - actor.Radius);

            }
        }
    }
}
