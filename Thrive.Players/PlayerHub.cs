namespace Thrive.Players
{
    using Geometry;
    using Microsoft.AspNet.SignalR;

    public class PlayerHub : Hub
    {
        private static IHubContext _context;
        static PlayerHub()
        {
            _context = GlobalHost.ConnectionManager.GetHubContext("PlayerHub");
        }

        public static void Update(Game game)
        {
            _context.Clients.All.gameReport(game);
        }

        public void Move(string id, Point moveTo)
        {
            PlayerRemote.FromID(id).MovementVector = moveTo;
        }

        public void Split(string id)
        {
            PlayerRemote.FromID(id).Split();
        }
    }
}