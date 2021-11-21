using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Chain
{
    class ConcreteHandler1 : Handler
    {
        public override object HandleRequest(int request,int player_speed)
        {
            if(request == 5)
            {
                return 5;
            }
            else if(player_speed < 20)
            {
                return 10;
            }
            else
            {
                return successor.HandleRequest(request, player_speed);
            }
        }
    }
}
