using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Mediator
{
    public class HazardMediator : Mediator
    {
        public Fire fireHazard { private get; set; }
        public Water waterHazard { private get; set; }

        public override void Change(string message, Hazard hazard)
        {
            if(hazard.GetType().Name == "Fire")
            {
                Console.WriteLine("TRUE");
                waterHazard.Notify(message);
            }
            else
            {
                Console.WriteLine("FALSE");
                fireHazard.Notify(message);
            }
        }
    }
}
