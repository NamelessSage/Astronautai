using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class ObstacleCollection : iCollection
    {
        Obstacle[] obstacles;
        public ObstacleCollection()
        {
            obstacles = new Obstacle[0];
        }

        public void addItem(Obstacle obstacle)
        {
            Obstacle[] obs = new Obstacle[obstacles.Length + 1];
            int i = 0;
            foreach (Obstacle ob in obstacles)
            {
                obs[i] = ob;
                i++;
            }
            obs[i] = obstacle;
            obstacles = new Obstacle[obs.Length];
            obstacles = obs;
        }

        public iIterator createIterator()
        {
            return new ObstacleIterator(obstacles);
        }
    }
}
