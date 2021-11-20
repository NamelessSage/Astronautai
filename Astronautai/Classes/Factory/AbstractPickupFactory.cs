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
        public static int count = 0;

        public virtual Pickup CreateAmmoPickup()  { return new Pickup(); }
        public virtual Pickup CreateHealthPickup() { return new Pickup(); }
        public virtual Pickup CreateSpeedPickup() { return new Pickup(); }
        public virtual Pickup CreateMultiPickup() { return new Pickup(); }
    }
}
