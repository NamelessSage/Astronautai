using Astronautai.Classes.Memento;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory.BasicPickup
{
    class TeleportPickup : Pickup
    {
        public override Player Action(Player player, Pickup pickup)
        {
            if (player.GetMemory().Memento == null)
            {
                var mem = player.SaveMemento();
                player.SetMemento(mem);
            }
            else
            {
                player.RestoreMemento(player.GetMemory().Memento);

            }
            return player;
        }
    }
}
