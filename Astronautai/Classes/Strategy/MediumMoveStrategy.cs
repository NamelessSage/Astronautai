using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Strategy
{
    public class MediumMoveStrategy : MoveStrategy
    {
        int Speed = 2;

        public override int MoveAlgorithm()
        {
            return Speed;
        }
    }
}
