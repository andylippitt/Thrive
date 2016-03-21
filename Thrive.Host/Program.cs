[assembly: Microsoft.Owin.OwinStartup(typeof(Thrive.Host.Program))]
namespace Thrive.Host
{
    using Behaviors;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Hosting;
    using Newtonsoft.Json;
    using Owin;
    using System;
    using Thrive;
    using Thrive.Geometry;
    using Thrive.Players;

    class Program
    {
        static void Main(string[] args)
        {
            var options = new StartOptions();
            options.Urls.Add("http://localhost:8080");
            options.Urls.Add("http://thrive.violetdata.com:8080");

            using (WebApp.Start(options))
            {
                Console.WriteLine("Server running");
                
                var game = new Game(new Configuration
                {
                    Dimensions = new Dimensions
                    {
                        Height = 600,
                        Width = 800
                    }
                });

                game.Players.Add(new PlayerRemote(game));
                game.Players.Add(new Wanderer(game));
                game.Players.Add(new Wanderer(game));
                game.Players.Add(new Wanderer(game));
                game.Players.Add(new Wanderer(game));
                game.Players.Add(new Wanderer(game));
                game.Players.Add(new Wanderer(game));

                game.Behaviors.Add(new PlayerSeparation());
                game.Behaviors.Add(new StayInBounds());
                game.Behaviors.Add(new Eat());

                game.Initialize();

                while (true)
                {
                    //Console.WriteLine("Step");
                    System.Threading.Thread.Sleep(50);

                    game.Step();
                }
            }
        }

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            serializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            var serializer = JsonSerializer.Create(serializerSettings);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);
        }
    }
}