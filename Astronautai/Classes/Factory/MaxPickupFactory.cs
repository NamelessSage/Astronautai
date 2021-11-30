using Astronautai.Classes.Builder;
using Astronautai.Classes.Factory.BasicPickup;
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

        public MaxPickupFactory()
        {
            random = new Random();
        }

        public override Pickup CreateAmmoPickup()
        {
            count++;
            PickupBuilder builder = new AmmoPickupBuilder();
            builder.SetType("AmmoPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(100, 700), random.Next(100, 500));
            builder.SetImage(@"..//..//Objects//ammoPickup.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(10);
            return builder.GetBuildable();
        }

        public override Pickup CreateHealthPickup()
        {
            count++;
            random = new Random();
            PickupBuilder builder = new HealthPickupBuilder();
            builder.SetType("HealthPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(100, 700), random.Next(100, 500));
            builder.SetImage(@"..//..//Objects//healthPickup.png");
            builder.SetSize(pickupSize);
            builder.SetValue(3);
            return builder.GetBuildable();
        }

        public override Pickup CreateSpeedPickup()
        {
            count++;
            random = new Random();
            PickupBuilder builder = new SpeedPickupBuilder();
            builder.SetType("SpeedPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(100, 700), random.Next(100, 500));
            builder.SetImage(@"..//..//Objects//speedPickup.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(10);
            return builder.GetBuildable();
        }

        public override Pickup CreateMultiPickup()
        {
            count++;
            PickupBuilder builder = new MultiPickupBuilder();
            builder.SetType("MultiPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(100, 700), random.Next(100, 500));
            builder.SetImage(@"..//..//Objects//multiPickup.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(1);
            List<Pickup> effects = new List<Pickup>();
            effects.Add(CreateSpeedPickup());
            effects.Add(CreateHealthPickup());
            effects.Add(CreateAmmoPickup());
            builder.SetEffects(effects);
            return builder.GetBuildable();
        }

        public override Pickup TeleportMultiPickup()
        {
            count++;
            random = new Random();
            PickupBuilder builder = new TeleportPickupBuilder();
            builder.SetType("TeleportPickup");
            builder.SetId(count);
            builder.SetCoordinates(random.Next(100, 700), random.Next(100, 500));
            builder.SetImage(@"..//..//Objects//smulAsteroid.jpg");
            builder.SetSize(pickupSize);
            builder.SetValue(1);
            return builder.GetBuildable();
        }
    }
}
