using Astronautai;
using Class_diagram;
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
        public static Map map = Map.getInstance();

        public GameData()
        {

        }

        public Player CreateNewPlayer()
        {
            Player p = new Player(players.Count, 3, 100);
            p.X = 100;
            p.Y = 100;
            players.Add(p);

            return players[players.Count-1];
        }




    }
}
