using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    public abstract class Mediator
    {
        public abstract void Send(string message, UIManager manager, Label label, string labelValue);
    }
}
