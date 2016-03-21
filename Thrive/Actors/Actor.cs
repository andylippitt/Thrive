namespace Thrive.Actors
{
    using System;
    using Thrive.Geometry;

    public abstract class Actor
    {
        public Point Position { get; set; }

        public Game Game { get; set; }
        public string ActorType { get; set; }
        public string ID { get; }

        private double _Mass;
        private double _Radius;

        public double Mass
        {
            get
            {
                return _Mass;
            }
            set
            {
                _Mass = value;
                _Radius = Math.Sqrt(_Mass / Math.PI);
            }
        }

        public double Radius
        {
            get
            {
                return _Radius;
            }
        }

        public Actor(Game game)
        {
            ID = Guid.NewGuid().ToString();
            Game = game;
            ActorType = this.GetType().Name;
        }

        public virtual void Die()
        {
            Game.Actors.Remove(this);
        }

        public virtual bool CanBeEatenBy(Actor actor)
        {
            return true;
        }
    }
}
