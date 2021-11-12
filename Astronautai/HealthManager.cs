using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    class HealthManager
    {
        int PlayerStartHealth;

        public void DisplayLabel(Label label, int playerStartHealth)
        {
            label.Visible = true;
            label.Text = string.Format("Health: {0}/{0}", playerStartHealth);

            PlayerStartHealth = playerStartHealth;
        }

        public void UpdateLabel(Label label, int playerHealth)
        {
            label.Text = "Health: " + playerHealth + "/" + PlayerStartHealth;
        }
    }
}
