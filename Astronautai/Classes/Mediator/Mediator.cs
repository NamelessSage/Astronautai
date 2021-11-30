using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Mediator
{
    public abstract class Mediator
    {
        public abstract void Change(string message, Hazard hazard);
    }
}
