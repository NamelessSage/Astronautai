using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Visitor
{
    public class Visitor2 : IVisitor
    {
        public void Visit(Element element)
        {
            Enemy enemy = element as Enemy;
            enemy.Health += 10;
        }
    }
}
