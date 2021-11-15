using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Strategy
{
    public class NoneMoveStrategy : MoveStrategy
    {
        int Speed = 0;

        public override int MoveAlgorithm()
        {
            return Speed;
        }
    }
}
