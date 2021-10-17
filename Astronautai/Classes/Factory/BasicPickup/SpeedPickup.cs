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

            return player;
        }
    }
}
