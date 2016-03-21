namespace Thrive.Behaviors
{
    using System;

    public class FoodBalance : Behavior
    {
        private int TotalGameMass;

        public FoodBalance(int totalGameMass)
        {
            TotalGameMass = totalGameMass;
        }

        public override void Step()
        {
            double mass = 0;
            var actors = Game.Actors;
            for (int i = 0; i < actors.Count; i++)
                mass += actors[i].Mass;

            int delta = TotalGameMass-(int)mass;

            if (delta > 0)
            {
                
            }
        }
    }
}
