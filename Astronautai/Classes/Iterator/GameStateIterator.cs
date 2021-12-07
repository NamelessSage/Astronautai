using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class GameStateIterator : iIterator
    {
        string[] GameStates;
        int pos = 0;

        public GameStateIterator(string[] gameStates)
        {
            GameStates = gameStates;
        }

        public bool hasNext()
        {
            if (pos >= GameStates.Length || GameStates[pos] == null)
                return false;
            else
                return true;
        }

        public object next()
        {
            string state = GameStates[pos];
            pos += 1;
            return state;
        }

        public object current()
        {
            return GameStates[pos];
        }

    }
}
