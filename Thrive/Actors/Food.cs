namespace Thrive.Actors
{
    public class Food : Actor
    {
        public Food(Game game) : base(game)
        {
            Mass = Game.Random.Next(Game.Configuration.FoodMinSize, Game.Configuration.FoodMaxSize);
            Position = Game.RandomPosition();
        }
    }
}
