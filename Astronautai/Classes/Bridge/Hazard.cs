using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public abstract class Hazard
    {
        protected IBridge bridge1;
        protected IBridge bridge2;

        public int X { get; set; }
        public int Y { get; set; }

        protected Hazard(IBridge bridge1, IBridge bridge2)
        {
            this.bridge1 = bridge1;
            this.bridge2 = bridge2;
        }

        public abstract string Effect();

    }
}
