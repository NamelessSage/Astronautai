using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class ObstacleIterator : iIterator
    {
        Obstacle[] obstacles;
        int pos = 0;

        public ObstacleIterator(Obstacle[] obstacle)
        {
            obstacles = obstacle;
        }

        public bool hasNext()
        {
            if (pos >= obstacles.Length || obstacles[pos] == null)
                return false;
            else
                return true;
        }

        public object next()
        {
            Obstacle obs = obstacles[pos];
            pos += 1;
            return obs;
        }

        public object current()
        {
            return obstacles[pos];
        }
    }
}
