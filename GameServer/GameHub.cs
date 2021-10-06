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

        public void AddPlayerOnJoin(Player player)
        {
            data.AddPlayer(player);
        }

        public void GetPlayers()
        {
            List<Player> players = data.GetPlayers();
            Clients.All.getPlayers(players); //SEND TO EVERYONE ??
        }

        public void StartGame()
        {
            Clients.All.startGame(true);
        }

        public void PlayerMovement(Player player)
        {
            Console.WriteLine($"Moving player: {player.Username} {player.X} {player.Y}");
            Clients.All.movePlayer(player.Username, player.X, player.Y);

        }



        //PADARYTI ZAIDEJU SARASO ATNAUJINIMA
    }
}
