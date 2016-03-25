namespace Thrive.Actors
{
    using System;
    using Thrive.Geometry;

    public abstract class Actor
    {
        public Point Position { get; set; }

        public ActorColor Color { get; set; }
        public enum ActorColor
        {
            Green,
            Red,
            Yellow
        }

        public Game Game { get; set; }
        public string ActorType { get; set; }
        public string ID { get; }

        public string Note { get; set; }

        private double _Mass;
        private double _Radius;

        public Actor()
        {
            Color = ActorColor.Green;
        }

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
            game.Actors.Add(this);
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
