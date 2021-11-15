using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class HazardDamage : IBridge
    {
        static Random random = new Random();
        public char valueDangerous()
        {
            return random.Next(2, 4).ToString().ToCharArray()[0];
        }
        public char valueInnocutous()
        {
            return random.Next(0, 2).ToString().ToCharArray()[0];
        }
    }
}
