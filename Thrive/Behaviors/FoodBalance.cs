namespace Thrive.Behaviors
{
    using Actors;
    using System;

    public class FoodBalance : Behavior
    {
        public FoodBalance()
        {
        }

        public override void Step()
        {
            double mass = 0;
            var actors = Game.Actors;
            for (int i = 0; i < actors.Count; i++)
                mass += actors[i].Mass;

            double delta = Game.Configuration.TotalGameMass-mass;
            if (Math.Abs(delta) > Game.Configuration.TotalGameMassTolerance)
            {
                if (delta > 0)
                {
                    while (delta > 0)
                    {
                        var food = new Food(Game);
                        delta -= food.Mass;
                    }
                }
                else
                {
                    int i = 0;
                    Actor actor = null;
                    while (delta < 0 && i < Game.Actors.Count)
                    {
                        while (++i < Game.Actors.Count && !(Game.Actors[i] is Food)) ;
                        if (i < Game.Actors.Count)
                        {
                            actor = Game.Actors[i];

                            if (actor is Food)
                            {
                                actor.Die();
                                delta += actor.Mass;
                            }
                        }
                    }
                }
            }
        }
    }
}
