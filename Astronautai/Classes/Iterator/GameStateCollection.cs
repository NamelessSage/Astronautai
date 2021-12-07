using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class GameStateCollection : iCollection
    {
        string[] GameStates;
        
        public GameStateCollection()
        {
            GameStates = new string[4];
            GameStates[0] = "none";
            GameStates[1] = "startGame";
            GameStates[2] = "gameLoopStarted";
            GameStates[3] = "gameOver";
        }

        public iIterator createIterator()
        {
            return new GameStateIterator(GameStates);
        }
    }
}
