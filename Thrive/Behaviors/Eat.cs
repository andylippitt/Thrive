namespace Thrive.Behaviors
{
    using Geometry;
    using System;

    public class Eat : Behavior
    {
        public override void Step()
        {
            for (int i=0; i<Game.Actors.Count; i++)
            {
                var actor = Game.Actors[i];

                for (int x = 0; x < Game.Actors.Count; x++)
                    if (i != x)
                    {
                        var other = Game.Actors[x];
                        if (other.CanBeEatenBy(actor))
                        {
                            if (Geo.Distance(actor.Position, other.Position) < ((other.Radius * Game.Configuration.EatDistanceThreshold) + actor.Radius))
                            {
                                actor.Mass += other.Mass - (other.Mass * Game.Configuration.EatCost);
                                actor.Mass = Math.Min(actor.Mass, Game.Configuration.CellMaxMass);
                                other.Die();
                            }
                        }
                    }
            }
        }
    }
}
