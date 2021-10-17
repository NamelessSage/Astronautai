using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory
{
    public class MaxPickupFactory : AbstractPickupFactory
    {
        const int maxCoordinate = 500;
        const int pickupSize = 20;
        Random random;
        public override Pickup CreateAmmoPickup(int pickupCount)
        {
            random = new Random();
            PickupBuilder builder = new PickupBuilder();
            builder.SetId(pickupCount);
            builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
            builder.SetImage(@"..//..//Objects//ammo.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(10);
            return builder.GetBuildable();
        }
        public override Pickup CreateHealthPickup(int pickupCount)
        {
            random = new Random();
            PickupBuilder builder = new PickupBuilder();
            builder.SetId(pickupCount);
            builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
            builder.SetImage(@"..//..//Objects//healthPickup.png");
            builder.SetSize(pickupSize);
            builder.SetValue(3);
            return builder.GetBuildable();
        }
        public override Pickup CreateSpeedPickup(int pickupCount)
        {
            random = new Random();
            PickupBuilder builder = new PickupBuilder();
            builder.SetId(pickupCount);
            builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
            builder.SetImage(@"..//..//Objects//ammo.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(10);
            return builder.GetBuildable();
        }
    }
}
