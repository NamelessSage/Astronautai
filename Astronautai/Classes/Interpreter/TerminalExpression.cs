using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class TerminalExpression : iExpression
    {
        string Effect;

        public TerminalExpression(string eff)
        {
            Effect = eff;
        }

        public string interpreter(string element)
        {
            if (Effect == element)
                return "Combine";
            if (Effect == "Damage")
            {
                switch (element)
                {
                    case "Slowdown":
                        return "Destroy";
                    default:
                        return null;
                }
            }
            else if (Effect == "Slowdown")
            {
                switch (element)
                {
                    default:
                        return null;
                }
            }
            return null;
        }
    }
}
