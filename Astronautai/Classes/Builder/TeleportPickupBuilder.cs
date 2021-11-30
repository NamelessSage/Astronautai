using Astronautai.Classes.Factory.BasicPickup;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Builder
{
    class TeleportPickupBuilder : PickupBuilder
    {
        public TeleportPickupBuilder()
        {
            pickup = new TeleportPickup();
        }

        public override Pickup GetBuildable()
        {
            try
            {
                return pickup;
            }
            finally
            {
                Reset();
            }
        }

        public override void Reset()
        {
            pickup = new TeleportPickup();
        }
    }
}
