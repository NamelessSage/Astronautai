using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_diagram
{
    class NoAmmoDecorator : Decorator
    {
        public NoAmmoDecorator(PlayerImage player) : base(player) {
            image = @"..//..//Objects//noAmmo.jpg";
        }
    }
}
