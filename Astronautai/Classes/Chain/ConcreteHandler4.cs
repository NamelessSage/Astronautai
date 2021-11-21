using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Chain
{
    class ConcreteHandler4 : Handler
    {
        public override object HandleRequest(int request, int player_speed)
        {
            if (request > 40)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}
