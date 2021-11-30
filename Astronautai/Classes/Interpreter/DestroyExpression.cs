using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class DestroyExpression : iExpression
    {
        iExpression expr1;
        iExpression expr2;

        public DestroyExpression(iExpression expr1, iExpression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }

        public string interpreter(string element)
        {
            if(expr1.interpreter(element) == "Destroy" || expr2.interpreter(element) == "Destroy")
            {
                return "Destroy";
            }
            return null;
        }
    }
}
