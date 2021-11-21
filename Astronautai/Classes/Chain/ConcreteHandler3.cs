using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Chain
{
    class ConcreteHandler3 : Handler
    {
        public override object HandleRequest(int request, int player_speed)
        {
            if (request < 40)
            {
                return 6;
            }
            else
            {
                return successor.HandleRequest(request, player_speed);
            }
        }
    }
}
