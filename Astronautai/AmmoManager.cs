using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    class AmmoManager : UIManager
    {
        int PlayerStartAmmo;

        public AmmoManager(Mediator mediator) : base(mediator) { }

        public override void DisplayLabel(Label label, int playerStartAmmo)
        {
            label.Visible = true;
            label.Text = string.Format("Ammo: {0}/{0}", playerStartAmmo);

            PlayerStartAmmo = playerStartAmmo;
        }

        public override void UpdateLabel(Label label, int playerAmmo)
        {
            label.Text = "Ammo: " + playerAmmo + "/" + PlayerStartAmmo;
        }

        public void Send(string message, Label label, string labelValue)
        {
            mediator.Send(message, this, label, labelValue);
        }

        public void Notify(string message, Label label, string labelValue)
        {
            label.Text = labelValue;
            Console.WriteLine("AmmoManager gets message: " + message);
        }
    }
}
