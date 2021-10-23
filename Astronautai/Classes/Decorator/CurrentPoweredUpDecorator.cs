using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_diagram
{
    class CurrentPoweredUpDecorator : Decorator
    {
        public CurrentPoweredUpDecorator(PlayerImage player) : base(player) { }
        public override string GetImage()
        {
            return @"..//..//Objects//currentPlayerFast.png";
        }
    }
}
