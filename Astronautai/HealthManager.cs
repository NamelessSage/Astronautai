using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    class HealthManager : UIManager
    {
        int PlayerStartHealth;

        public HealthManager(Mediator mediator) : base(mediator) { }

        public override void DisplayLabel(Label label, int playerStartHealth)
        {
            label.Visible = true;
            label.Text = string.Format("Health: {0}/{0}", playerStartHealth);

            PlayerStartHealth = playerStartHealth;
        }

        public override void UpdateLabel(Label label, int playerHealth)
        {
            label.Text = "Health: " + playerHealth + "/" + PlayerStartHealth;
        }

        public void Send(string message, Label label, string labelValue)
        {
            mediator.Send(message, this, label, labelValue);
        }

        public void Notify(string message, Label label, string labelValue)
        {
            label.Text = labelValue;
            Console.WriteLine("HealthManager gets message: " + message);
        }
    }
}
