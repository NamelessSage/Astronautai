using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai { 
    public class PlayerIterator : iIterator
    {
        Player[] players;
        int pos = 0;

        public PlayerIterator(Player[] player)
        {
            players = player;
        }

        public bool hasNext()
        {
            if (pos >= players.Length || players[pos] == null)
                return false;
            else
                return true;
        }

        public object next()
        {
            Player plr = players[pos];
            pos += 1;
            return plr;
        }

        public object current()
        {
            return players[pos];
        }
    }
}
