namespace Thrive.Players
{
    using Actors;
    using BasicVector;
    using Geometry;
    using System;
    using System.Collections.Generic;

    public abstract class Player
    {
        public Game Game { get; set; }
        public Point MovementVector { get; set; }
        public ActorCollection Actors { get; set; }
        

        public Player(Game game)
        {
            Game = game;
            Actors = new ActorCollection(game);
        }

        public virtual void Report() { }
        public virtual void Initialize()
        {
            var actor = new Cell(Game)
            {
                Mass = 3000,
                Position = Game.RandomPosition(),
                Player = this
            };
            Actors.Add(actor);
        }

        public virtual void Step()
        {
            if (Actors.Count == 0)
                Die();
            else
            {
                Hunger();
                
                if (MovementVector != null)
                {
                    for (int i = 0; i < Actors.Count; i++)
                    {
                        var actor = Actors[i];

                        double speed = Math.Min(Geo.Distance(actor.Position, MovementVector), MaxCellSpeed(actor));

                        var current = actor.Position.ToVector();
                        var target = MovementVector.ToVector();

                        var direction = target - current;
                        direction = VectorUtil.Normalize(direction);

                        current += direction * speed;

                        actor.Position.X = (int)current.X;
                        actor.Position.Y = (int)current.Y;
                    }
                }
            }
        }

        protected virtual void Hunger()
        {
            foreach (var actor in Actors)
                actor.Mass *= 1 - Game.Configuration.CellHunger;
        }

        protected virtual void Die()
        {
            Game.Players.Remove(this);
        }

        private double MaxCellSpeed(Actor cell)
        {
            return Game.Configuration.CellMaxSpeed
                - (cell.Mass * Game.Configuration.CellMassSpeedPenalty);
        }

        public virtual void Split()
        {
            var newActors = new List<Cell>();

            foreach (var actor in Actors)
            {
                if (actor.Mass > Game.Configuration.MinimumSplitMass
                    && (Actors.Count + newActors.Count < Game.Configuration.MaxCellsPerPlayer))
                {
                    actor.Mass = actor.Mass / 2 - Game.Configuration.SplitCost * actor.Mass;
                    var buddy = new Cell(Game);
                    buddy.Mass = actor.Mass;
                    buddy.Position = new Point
                    {
                        X = actor.Position.X,
                        Y = actor.Position.Y
                    };
                    buddy.Player = this;

                    newActors.Add(buddy);
                }
            }

            foreach (var actor in newActors)
            {
                Actors.Add(actor);
                Game.Actors.Add(actor);
            }
        }

        protected Point CenterPointOfActors
        {
            get
            {
                if (Actors.Count > 0)
                {
                    var centerOfMyActors = new Point();
                    foreach (var actor in Actors)
                    {
                        centerOfMyActors.X += actor.Position.X;
                        centerOfMyActors.Y += actor.Position.Y;
                    }
                    centerOfMyActors.X /= Actors.Count;
                    centerOfMyActors.Y /= Actors.Count;

                    return centerOfMyActors;
                }
                else return null;
            }
        }

        public bool IsDead()
        {
            return Actors.Count == 0;
        }
    }
}
