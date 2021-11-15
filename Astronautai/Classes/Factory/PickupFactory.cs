//using Class_diagram;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Astronautai.Classes.Factory
//{
//    public class PickupFactory
//    {
//        const int pickupSize = 20;
//        const int maxCoordinate = 500;

//        static int pickupCount = 0;

//        Random random;

//        public PickupFactory()
//        {
//            random = new Random();
//        }

//        public Pickup BuildPickup(string type, int value)
//        {
//            pickupCount++;
//            PickupBuilder builder = new PickupBuilder();
//            switch (type)
//            {
//                case "Ammo":
//                    builder.SetId(pickupCount);
//                    builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
//                    builder.SetImage(@"..//..//Objects//ammo.jpg");
//                    builder.SetSize(pickupSize);
//                    builder.SetValue(value);

//                    return builder.GetBuildable();
//                case "Health":
//                    builder.SetId(pickupCount);
//                    builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
//                    builder.SetImage(@"..//..//Objects//healthPickup.png");
//                    builder.SetSize(pickupSize);
//                    builder.SetValue(value);

//                    return builder.GetBuildable();
//                case "Speed":
//                    builder.SetId(pickupCount);
//                    builder.SetCoordinates(random.Next(0, maxCoordinate), random.Next(0, maxCoordinate));
//                    builder.SetImage(@"..//..//Objects//healthPickup.png");
//                    builder.SetSize(pickupSize);
//                    builder.SetValue(value);

//                    return builder.GetBuildable();
//                default:
//                    throw new NotSupportedException("Pickup type not found!");
//            }
//        }
//    }
//}
