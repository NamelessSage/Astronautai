using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Visitor
{
    public class ObjectStructure
    {
        List<Enemy> all = new List<Enemy>();
        public void Attach(Enemy enemy)
        {
            all.Add(enemy);
        }
        public void Detach(Enemy enemy)
        {
            all.Remove(enemy);
        }
        public void Accept(IVisitor visitor)
        {
            foreach (Enemy one in all)
            {
                //element.Accept(visitor);
                one.Accept(visitor);
            }
        }
    }
}
