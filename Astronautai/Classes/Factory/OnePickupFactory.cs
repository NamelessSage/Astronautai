using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory
{
    public class OnePickupFactory : AbstractPickupFactory
    {
        const int maxCoordinate = 500;
        const int pickupSize = 20;
        Random random;


        public OnePickupFactory()
        {
            random = new Random();
        }

        public override Pickup CreateAmmoPickup()
        {
            count++;
            PickupBuilder builder = new PickupBuilder();
            builder.SetType("AmmoPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
            builder.SetImage(@"..//..//Objects//ammoPickup.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(1);
            return builder.GetBuildable();
        }
        
        public override Pickup CreateHealthPickup()
        {
            count++;
            PickupBuilder builder = new PickupBuilder();
            builder.SetType("HealthPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
            builder.SetImage(@"..//..//Objects//healthPickup.png");
            builder.SetSize(pickupSize);
            builder.SetValue(1);
            return builder.GetBuildable();
        }

        public override Pickup CreateSpeedPickup() {
            count++;
            PickupBuilder builder = new PickupBuilder();
            builder.SetType("SpeedPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
            builder.SetImage(@"..//..//Objects//speedPickup.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(1);
            return builder.GetBuildable();
        }
    }
}
