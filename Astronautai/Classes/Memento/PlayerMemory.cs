using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Memento
{
    public class PlayerMemory
    {
        Memento memento;

        public PlayerMemory()
        {
        }

        public Memento Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}
