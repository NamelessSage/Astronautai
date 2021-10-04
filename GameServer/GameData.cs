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
        public Dictionary<string, PictureBox> playerBoxes = new Dictionary<string, PictureBox>();



        public GameData()
        {

        }

        public Player CreateNewPlayer()
        {
            Console.WriteLine("1");
            Player p = new Player(players.Count, 3, 100);
            p.X = 100;
            p.Y = 100;
            players.Add(p);
            Console.WriteLine(2);
            return players[players.Count-1];
        }




    }
}
