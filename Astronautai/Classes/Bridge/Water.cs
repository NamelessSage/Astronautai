using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{ 
    public class Water : Hazard
    {
        public Water(IBridge bridge1, IBridge bridge2) : base(bridge1, bridge2) { }

        public override string Effect()
        {
            return "Slowdown," + bridge1.valueInnocutous() + ',' + bridge2.valueInnocutous();
        }

        public override void Move1()
        {
            this.Y += 10;
        }

        public override void Move2()
        {
             this.Y -= 10;
        }
    }
}
