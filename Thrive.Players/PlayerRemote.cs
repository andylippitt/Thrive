namespace Thrive.Players
{
    using System;
    using System.Collections.Generic;

    public class PlayerRemote : Player
    {
        private static Dictionary<string, PlayerRemote> Players = new Dictionary<string, PlayerRemote>();
        public static PlayerRemote FromID(string id)
        {
            return Players[id];
        }

        public string ID { get; }

        public PlayerRemote(Game game) : base(game)
        {
            ID = Guid.NewGuid().ToString();
            Players.Add(ID, this);
        }

        public override void Report()
        {
            base.Report();
            PlayerHub.Update(Game);
        }
    }
}
