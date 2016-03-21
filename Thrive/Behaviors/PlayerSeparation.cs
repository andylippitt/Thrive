namespace Thrive.Behaviors
{
    using BasicVector;
    using Geometry;
    using System;

    public class PlayerSeparation : Behavior
    {
        public override void Step()
        {
            for (int i=0; i<Game.Players.Count; i++)
            {
                var player = Game.Players[i];

                if (player.Actors.Count > 1)
                    foreach (var actor in player.Actors)
                        foreach (var other in player.Actors)
                            if (actor != other)
                            {
                                var minimumDistance = actor.Radius + other.Radius;
                                if (actor.Position.X == other.Position.X 
                                    && actor.Position.Y == other.Position.Y)
                                {
                                    actor.Position.X += Game.Random.Next(1,3);
                                    actor.Position.Y += Game.Random.Next(1,3);
                                }

                                var distance = Geo.Distance(actor.Position, other.Position);
                                if (distance < minimumDistance)
                                {
                                    var actorVector = actor.Position.ToVector();
                                    var motion = actorVector - other.Position.ToVector();
                                    motion = VectorUtil.Normalize(motion);
                                    actorVector += motion * (minimumDistance - distance) * Game.Configuration.SeparationSpeed;
                                    actor.Position.X = (int)actorVector.X;
                                    actor.Position.Y = (int)actorVector.Y;
                                }
                            }
            }
        }
    }
}
