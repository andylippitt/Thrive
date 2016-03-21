namespace Thrive.Actors
{
    using Common;
    using Events;

    public class ActorCollection : ThriveCollection<Actor>
    {
        private Game Game;

        public ActorCollection(Game game)
        {
            Game = game;
        }

        public event ActorAddedHandler OnActorAdded;
        public event ActorRemovedHandler OnActorRemoved;

        public delegate void ActorAddedHandler(ActorEventArgs e);
        public delegate void ActorRemovedHandler(ActorEventArgs e);

        public override void Add(Actor actor)
        {
            base.Add(actor);
            actor.Game = Game;
            if (OnActorAdded != null)
                OnActorAdded.Invoke(new ActorEventArgs { Actor = actor });
        }

        public override void Remove(Actor actor)
        {
            base.Remove(actor);
            if (OnActorRemoved != null)
                OnActorRemoved.Invoke(new ActorEventArgs { Actor = actor });
        }
    }
}
