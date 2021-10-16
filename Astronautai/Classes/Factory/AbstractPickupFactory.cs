using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory
{
    public abstract class AbstractPickupFactory
    {
        public virtual void CreateAmmoPickup() { }
        public virtual void CreateHealthPickup() { }
        public virtual void CreateSpeedPickup() { }

    }
}
