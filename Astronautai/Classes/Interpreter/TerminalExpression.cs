using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class TerminalExpression : iExpression
    {
        Player pl;

        public TerminalExpression(Player pl, string value)
        {
            this.pl = pl;
        }

        public Player interpreter(string value)
        {
            string[] str = value.Split(' ');
            if (str[0] == "add")
            {
                if (str[1] == "ammo")
                {
                    if (str[2] != "max")
                    {
                        pl.AddAmmo(int.Parse(str[2]));
                    }
                    else
                    {
                        pl.Ammo = 10;
                    }
                }
                else if (str[1] == "health")
                {
                    if (str[2] != "max")
                    {
                        pl.AddHealth(int.Parse(str[2]));
                    }
                    else
                    {
                        pl.Health = 3;
                    }
                }
            }
            else if (str[0] == "remove")
            {
                if (str[1] == "ammo")
                {
                    if (str[2] != "max")
                    {
                        pl.Ammo -= int.Parse(str[2]);
                        if (pl.Ammo < 0)
                        {
                            pl.Ammo = 0;
                        }
                    }
                    else
                    {
                        pl.Ammo = 0;
                    }
                }
                else if (str[1] == "health")
                {
                    if (str[2] != "max")
                    {
                        pl.Damage(int.Parse(str[2]));
                        if (pl.Health < 0)
                        {
                            pl.Health = 0;
                        }
                    }
                    else
                    {
                        pl.Health = 0;
                    }
                }
            }
            return pl;
        }
    }
}
