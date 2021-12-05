using Astronautai.Classes.Visitor;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory.BasicPickup
{
    class AsteroidPickup : Pickup
    {
        static Random random = new Random();
        public override Player Action(Player player, Pickup pickup)
        {
            Map map = Map.Instance;
            int i = random.Next(2);
            if (i == 0)
            {
                map.visitor.Accept(new Visitor1());
            }
            if (i == 1)
            {
                map.visitor.Accept(new Visitor2());
            }
            if (i == 1)
            {
                map.visitor.Accept(new Visitor3());
            }
            return player;
        }
    }
}
