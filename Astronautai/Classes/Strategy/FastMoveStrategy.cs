using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Strategy
{
    public class FastMoveStrategy : MoveStrategy
    {
        int Speed = 3;

        public override int MoveAlgorithm()
        {
            return Speed;
        }
    }
}
