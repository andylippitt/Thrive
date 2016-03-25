using Thrive.Players;

namespace Thrive.Behaviors
{
    public class PlayerBalance : Behavior
    {
        public override void Step()
        {
            while (Game.Players.Count < Game.Configuration.HunterMin)
            {
                var hunter = new Hunter(Game);
                hunter.Initialize();
                Game.Players.Add(hunter);
            }
        }
    }
}
