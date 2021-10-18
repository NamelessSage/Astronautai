using Astronautai.Classes.Factory;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Astronautai.Classes.Observer;

namespace GameServer
{
    public class PickupSpawner : IObserver
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
            Pickup pickup = new Pickup();

            int randomValuePickupType = random.Next(0, 3);
            Console.WriteLine(randomValuePickupType);
            if (randomValuePickupType == 0)
            {
                pickup = onePickupFactory.CreateAmmoPickup();
            }
            else if (randomValuePickupType == 1)
            {
                pickup = onePickupFactory.CreateHealthPickup();
            }
            else
            {
                pickup = onePickupFactory.CreateSpeedPickup();
            }
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

        public void Update()
        {
            Map map = Map.Instance;
            if (map.pickups.Count < 10)
            {
                map.pickups.Add(SpawnRandom());
            }
        }
    }
}
