﻿using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class EnemySpawner
    {
        int movableMinX = 0;
        int movableMinY = 0;
        int movableMaxX = 800;
        int movableMaxY = 600;
        Random rnd;
        int counter = 1;

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

        public Enemy CreateAsteroid(Map map)
        {
            EnemyCreator creator = new EnemyCreator();
            int id = map.enemyCounter;
            int type = rnd.Next(0, 2);

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
    }
}
