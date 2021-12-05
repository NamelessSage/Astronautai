using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class PlayerCollection : iCollection
    {
        Player[] players;
        public PlayerCollection()
        {
            players = new Player[0];
        }

        public void addItem(Player player)
        {
            Player[] plrs = new Player[players.Length + 1];
            int i = 0;
            foreach (Player pl in players)
            {
                plrs[i] = pl;
                i++;
            }
            plrs[i] = player;
            players = new Player[plrs.Length];
            players = plrs;
        }

        public iIterator createIterator()
        {
            return new PlayerIterator(players);
        }
    }
}
