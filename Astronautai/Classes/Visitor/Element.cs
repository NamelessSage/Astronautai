using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Visitor
{
    public abstract class Element 
    {
        public abstract void Accept(IVisitor visitor);
    }
}
