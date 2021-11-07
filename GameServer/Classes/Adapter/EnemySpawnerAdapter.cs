using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Classes
{
    public class EnemySpawnerAdapter : Spawner
    {
        EnemySpawner adaptee = new EnemySpawner();

        public Enemy Spawn()
        {
            return adaptee.CreateAsteroid(Map.Instance);
        }

        public override void Update()
        {
            adaptee.Update();
        }
    }
}
