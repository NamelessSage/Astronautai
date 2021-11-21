using Astronautai.Classes.Chain;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory
{
    public class SpeedPickup : Pickup
    {
         
        public override Player Action(Player player, Pickup pickup)
        {
            player.AddSpeed(pickup.Value);
            player.GetState().ChangeSpeed(player.Speed);
            ConcreteHandler1 h1 = new ConcreteHandler1();
            ConcreteHandler2 h2 = new ConcreteHandler2();
            ConcreteHandler3 h3 = new ConcreteHandler3();
            ConcreteHandler4 h4 = new ConcreteHandler4();
            h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);
            h3.SetSuccessor(h4);

            var temp = h1.HandleRequest(pickup.Value,player.Speed);

            player.AddSpeed(Convert.ToInt32(temp));

            return player;
        }
    }
}
