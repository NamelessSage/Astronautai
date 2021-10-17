using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory
{
    public abstract class AbstractPickupFactory
    {
        public virtual Pickup CreateAmmoPickup(int pickupCount) 
        {
            
            return new Pickup();
        }
        public virtual Pickup CreateHealthPickup(int pickupCount) { return new Pickup(); }
        public virtual Pickup CreateSpeedPickup(int pickupCount) { return new Pickup(); }

    }
}
