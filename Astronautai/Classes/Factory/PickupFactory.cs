using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory
{
    public class PickupFactory
    {
        const int pickupSize = 20;
        const int maxCoordinate = 500;

        static int pickupCount = 0;

        Random random;

        public PickupFactory()
        {
            random = new Random();
        }

    }
}
