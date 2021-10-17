using Astronautai.Classes.Factory;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class PickupSpawner
    {
        Random random;

        OnePickupFactory onePickupFactory;
        MaxPickupFactory maxPickupFactory;

        public PickupSpawner()
        {
            random = new Random();
            onePickupFactory = new OnePickupFactory();
            maxPickupFactory = new MaxPickupFactory();
        }

        public Pickup SpawnRandom()
        {
            Console.WriteLine("SPAWN RANDOM");
            int randomValueFactoryType = random.Next(0, 4);
            if(randomValueFactoryType != 0)
            {
                return SpawnOneValuePickup();
            }
            else
            {
                return SpawnMaxValuePickup();
            }
        }

        public Pickup SpawnOneValuePickup()
        {
            Console.WriteLine("SPAWN ONE");
            Pickup pickup = new Pickup();
            int randomValuePickupType = random.Next(0, 3);
            Console.WriteLine(randomValuePickupType);
            if (randomValuePickupType == 0)
            {
                pickup = onePickupFactory.CreateAmmoPickup();
                Console.WriteLine("0");
            }
            else if (randomValuePickupType == 1)
            {
                pickup = onePickupFactory.CreateHealthPickup();
            }
            else
            {
                pickup = onePickupFactory.CreateSpeedPickup();
            }
            Console.WriteLine("ONE PICKUP " + pickup.Id);
            return pickup;
        }

        public Pickup SpawnMaxValuePickup()
        {
            int randomValuePickupType = random.Next(0, 3);
            if (randomValuePickupType == 0)
            {
                return maxPickupFactory.CreateAmmoPickup();
            }
            else if (randomValuePickupType == 1)
            {
                return maxPickupFactory.CreateHealthPickup();
            }
            else
            {
                return maxPickupFactory.CreateSpeedPickup();
            }
        }
    }
}
