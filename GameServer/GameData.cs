using Class_diagram;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Astronautai;

namespace GameServer
{
    public class GameData
    {
        public static List<Player> players = new List<Player>();
        public static Map map = Map.getInstance();

        public GameData()
        {

        }

        public List<Player> GetPlayers()
        {
            return players;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public Map GetMap()
        {
            return map;
        }


    }
}
