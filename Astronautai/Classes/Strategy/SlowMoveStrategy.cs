using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Strategy
{
    public class SlowMoveStrategy : MoveStrategy
    {
        int Speed = 1;

        public override int MoveAlgorithm()
        {
            Speed = 1;
            return Speed;
        }
    }
}
