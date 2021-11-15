﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_diagram
{
    class PoweredUpDecorator : Decorator
    {
        public PoweredUpDecorator(PlayerImage player) : base(player) {
            image = @"..//..//Objects//playerFast.png";
        }
    }
}
