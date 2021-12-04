using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class AmmoExpression : iExpression
    {
        iExpression expr1;

        public AmmoExpression(iExpression expr1)
        {
            this.expr1 = expr1;
        }

        public Player interpreter( string value)
        {
            if(int.Parse(value.Split(' ')[2]) > 10)
            {
                string newValue = value.Split(' ')[0] + " " + value.Split(' ')[1] + " max";
                return expr1.interpreter(newValue);
            }
            return expr1.interpreter(value);
        }
    }
}
