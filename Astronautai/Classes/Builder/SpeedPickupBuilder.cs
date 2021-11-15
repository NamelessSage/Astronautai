﻿using Astronautai.Classes.Factory;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Builder
{
    class SpeedPickupBuilder : PickupBuilder
    {
        public SpeedPickupBuilder()
        {
            pickup = new HealthPickup();
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
            pickup = new SpeedPickup();
        }
    }
}
