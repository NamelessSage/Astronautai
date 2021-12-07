using Astronautai.Classes.Visitor;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes
{
    public interface IVisitor
    {
        void Visit(Element element);
    }
}
