using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Chain
{
    class ConcreteHandler2 : Handler
    {
        public override object HandleRequest(int request, int player_speed)
        {
            if (player_speed < 30)
            {
                return 8;
            }
            else
            {
                return successor.HandleRequest(request, player_speed);
            }
        }
    }
}
