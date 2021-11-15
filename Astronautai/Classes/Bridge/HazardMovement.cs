using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class HazardMovement : IBridge
    {
        static Random random = new Random();
        public char valueDangerous()
        {
            var moveDirections = new char[] { 'A', 'D' };
            return moveDirections[random.Next(moveDirections.Length)];
        }

        public char valueInnocutous()
        {
            var moveDirections = new char[] { 'W', 'S' };
            return moveDirections[random.Next(moveDirections.Length)];
        }
    }
}
