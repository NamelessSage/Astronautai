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
using Astronautai;

namespace GameServer
{
    public class HazardSpawner
    {
        Random random;
        int id;

        public HazardSpawner()
        {
            id = 0;
            random = new Random();
        }

        public Hazard SpawnRandom()
        {
            int randomValueFactoryType = random.Next(0, 2);
            if(randomValueFactoryType == 0)
            {
                Hazard fire = new Fire(new HazardDamage(), new HazardMovement());
                fire.id = id;
                id++;
                fire.X = random.Next(100, 700);
                fire.Y = random.Next(100, 500);
                fire.MoveDirection = 'H';
                return fire;
            }
            else if (randomValueFactoryType == 1)
            {
                Hazard water = new Water(new HazardDamage(), new HazardMovement());
                water.id = id;
                id++;
                water.X = random.Next(100, 700);
                water.Y = random.Next(100, 500);
                water.MoveDirection = 'V';
                return water;
            }
            return null;
        }

    }
}
