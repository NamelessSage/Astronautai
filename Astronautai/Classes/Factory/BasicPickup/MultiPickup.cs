using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory.BasicPickup
{
    class MultiPickup : Pickup
    {
        public override Player Action(Player player, Pickup pickup)
        {
            foreach(var item in pickup.Effects)
            {
                item.Action(player, item);
            }

            return player;
        }
    }
}
