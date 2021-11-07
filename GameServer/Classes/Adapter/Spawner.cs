using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Classes
{
    public class Spawner
    {
        public virtual void Update()
        {
            Console.WriteLine("Spawner.Update()");
        }
    }
}
