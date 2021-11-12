using Class_diagram;
using GameServer.Classes;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Astronautai.Classes.Observer;

namespace GameServer
{
    public class EnemySpawner : Spawner, IObserver
    {
        Random rnd;

        public EnemySpawner()
        {
            rnd = new Random();
        }

        public Coordinates GetSpawnCoords()
        {
            int x = rnd.Next(-100, 900);
            int y = rnd.Next(-100, 700);

            bool spawned = false;

            while(!spawned)
            {
                x = rnd.Next(-100, 900);
                y = rnd.Next(-100, 700);
                if (x < 0 || x > 800)
                    spawned = true;

                if (y < 0 || y > 600)
                    spawned = true;
            }

            return new Coordinates(x, y);
        }

        public Enemy Spawn(Map map)
        {
            EnemyCreator creator = new EnemyCreator();
            int id = map.enemyCounter;
            int type = rnd.Next(0, 3);

            if(type == 0)
            {
                map.enemyCounter = id + 1;
                
                return creator.CreateAsteroid("Big", GetSpawnCoords(), id);
            }
            else if(type == 1)
            {
                map.enemyCounter = id + 1;

                return creator.CreateAsteroid("Average", GetSpawnCoords(), id);
            }
            else
            {
                map.enemyCounter = id + 1;

                return creator.CreateAsteroid("Small", GetSpawnCoords(), id);
            }
        }

        public override void Update()
        {
            Map map = Map.Instance;
            
            if (map.enemies.Count < 30)
            {
                map.enemies.Add(Spawn(map));
            }
        }
    }
}
