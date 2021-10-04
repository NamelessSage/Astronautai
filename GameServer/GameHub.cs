using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Astronautai;
using Class_diagram;
using System.Windows.Forms;

namespace GameServer
{
    [HubName("serveris")]
    public class GameHub : Hub
    {
        GameData data = new GameData();


        public GameHub()
        {

        }


        public Dictionary<string, PictureBox> getPlayers()
        {
            return data.playerBoxes;
        }

        public void GetPlayersServer()
        {
            Clients.Caller.createPlayerBox(getPlayers());
        }

        public void CreatePlayer()
        {
            Console.WriteLine(3);
            Player p = data.CreateNewPlayer();
            Console.WriteLine(4);
            Console.WriteLine($"Creating player: {p.Id}");
            Clients.Caller.createPlayer(p.Id, getPlayers());
        }


        public void PlayerMovement(int playerId, int x, int y)
        {
            Console.WriteLine($"Moving player: {playerId}, {x}, {y}");
            Clients.All.movePlayer(playerId, x, y, getPlayers());
        }

        public void AddPlayerBox(Dictionary<string, PictureBox> playerBox)
        {
            foreach (var dict in playerBox)
            {
                data.playerBoxes.Add(dict.Key, dict.Value);
            }
        }
    }
}
