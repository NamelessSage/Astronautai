using Astronautai.Classes.Factory;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

using static Astronautai.Classes.Observer;
using GameServer.Classes;

namespace GameServer
{
    public class PickupSpawner : IObserver
    {
        const int SpawnMax = 4;
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
            int randomValuePickupType = random.Next(0, 5);
            if (randomValuePickupType == 0)
            {
                return onePickupFactory.CreateAmmoPickup();
            }
            else if (randomValuePickupType == 1)
            {
                return onePickupFactory.CreateHealthPickup();
            }
            else if (randomValuePickupType == 2)
            {
                return onePickupFactory.CreateSpeedPickup();
            }
            else if (randomValuePickupType == 3)
            {
                return onePickupFactory.TeleportMultiPickup();
            }
            else if (randomValuePickupType == 4)
            {
                return onePickupFactory.AsteroidPickup();
            }
            else
            {
                return onePickupFactory.CreateMultiPickup();
            }

        }

        public Pickup SpawnMaxValuePickup()
        {
            int randomValuePickupType = random.Next(0, 5);
            if (randomValuePickupType == 0)
            {
                return maxPickupFactory.CreateAmmoPickup();
            }
            else if (randomValuePickupType == 1)
            {
                return maxPickupFactory.CreateHealthPickup();
            }
            else if (randomValuePickupType == 2)
            {
                return maxPickupFactory.CreateSpeedPickup();
            }
            else if (randomValuePickupType == 3)
            {
                return maxPickupFactory.TeleportMultiPickup();
            }
            else
            {
                return maxPickupFactory.CreateMultiPickup();
            }

        }

        public void Update()
        {
            Map map = Map.Instance;
            if (map.pickups.Count < SpawnMax+20)
            {
                Pickup pickup = SpawnRandom();
                map.pickups.Add(pickup);
                var context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
                context.Clients.All.addPickup(pickup);
            }
        }
    }
}
