using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Astronautai;
using Class_diagram;

namespace GameServer
{
    [HubName("serveris")]
    public class GameHub : Hub
    {
        GameData data = new GameData();


        public GameHub()
        {

        }


        public void CreatePlayer()
        {
            Player p = data.CreateNewPlayer();
            Console.WriteLine($"Creating player: {p.Id}, {p.X}, {p.Y}");
            Clients.Caller.createPlayer(p.Id, p.X, p.Y);
        }


        public void PlayerMovement(int playerId, int x, int y)
        {

            Console.WriteLine($"Moving player: {playerId}, {x}, {y}");
            Clients.All.movePlayer(playerId, x, y);
        }
    }
}
