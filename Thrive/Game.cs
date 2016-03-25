namespace Thrive
{
    using Behaviors;
    using Geometry;
    using Players;
    using System;
    using Thrive.Actors;

    public class Game
    {
        public Random Random = new Random();

        public ActorCollection Actors { get; }
        public BehaviorCollection Behaviors { get; }
        public PlayerCollection Players { get; }
        public Configuration Configuration { get; }

        private DateTime LastReport;

        public Game(Configuration configuration)
        {
            Configuration = configuration;

            Actors = new ActorCollection(this);
            Behaviors = new BehaviorCollection(this);
            Players = new PlayerCollection(this);
        }

        public void Initialize()
        {
            LastReport = DateTime.Now;

            foreach (var player in Players)
                player.Initialize();
        }

        public void Step()
        {
            Players.Step();
            Behaviors.Step();

            if (DateTime.Now.Subtract(LastReport).TotalMilliseconds > 40)
            {
                LastReport = DateTime.Now;
                Players.Report();
            }
        }

        public Point RandomPosition()
        {
            return new Point
            {
                X = Random.Next(0, Configuration.Dimensions.Width),
                Y = Random.Next(0, Configuration.Dimensions.Height)
            };
        }
    }
}
