using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public sealed class Fire : Hazard
    {
        public int Speed = 5;

        public Fire(IBridge bridge1, IBridge bridge2) : base(bridge1, bridge2) { }

        public override string Effect()
        {
            return "Damage," + bridge1.valueDangerous() +','+ bridge2.valueDangerous();
        }

        public override void Move1()
        {
            this.X += 10;
        }

        public override void Move2()
        {
            this.X -= 10;
        }
    }
}
