using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    class AmmoManager
    {
        int PlayerStartAmmo;

        public void DisplayLabel(Label label, int playerStartAmmo)
        {
            label.Visible = true;
            label.Text = string.Format("Ammo: {0}/{0}", playerStartAmmo);

            PlayerStartAmmo = playerStartAmmo;
        }

        public void UpdateLabel(Label label, int playerAmmo)
        {
            label.Text = "Ammo: " + playerAmmo + "/" + PlayerStartAmmo;
        }
    }
}
