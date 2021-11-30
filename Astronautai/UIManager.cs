using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    public abstract class UIManager
    {
        protected Mediator mediator;

        public UIManager(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public abstract void DisplayLabel(Label label, int startAmount);
        public abstract void UpdateLabel(Label label, int amount);
    }
}
