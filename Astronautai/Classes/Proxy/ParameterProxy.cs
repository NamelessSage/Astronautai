using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Proxy
{
    public class ParameterProxy : IParameter
    {
        private Player Player;

        public ParameterProxy(Player player)
        {
            Player = player;
        }

        public void Heal(int amount)
        {
            Console.WriteLine("Healing " + Player.Username + " by " + amount);
            Player.Heal(amount);
        }
    }
}
