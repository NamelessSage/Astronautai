using Astronautai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class GameData
    {
        public static List<Player> players = new List<Player>();

        public GameData()
        {

        }

        public Player CreateNewPlayer()
        {
            players.Add(new Player(players.Count, 100, 100));
            return players[players.Count-1];
        }




    }
}
