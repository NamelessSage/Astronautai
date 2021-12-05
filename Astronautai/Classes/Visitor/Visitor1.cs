using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Visitor
{
    public class Visitor1 : IVisitor
    {
        static Random random = new Random();
        public void Visit(Element element)
        {
            Enemy enemy = element as Enemy;
            var moveDirections = new char[] { 'W', 'A', 'S', 'D', 'Q', 'E', 'Z', 'C' };
            int i = random.Next(moveDirections.Length);
            enemy.Rotation = moveDirections[i];
        }
    }
}
