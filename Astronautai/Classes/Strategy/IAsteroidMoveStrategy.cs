using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Strategy
{
    interface IAsteroidMoveStrategy
    {
        void SetMoveSlow();
        void SetMoveAverage();
        void SetMoveFast();
        void SetMoveNone();
    }
}
