using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Classes
{
    public class PickupSpawnerAdapter : Spawner
    {
        PickupSpawner adaptee = new PickupSpawner();

        public Pickup Spawn()
        {
            return adaptee.SpawnRandom();
        }

        public override void Update()
        {
            adaptee.Update();
        }
    }
}
