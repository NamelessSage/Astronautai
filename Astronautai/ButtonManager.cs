using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    class ButtonManager
    {
        public void StartGame(Button startGame, Button joinGame, TextBox playerUsernameInput)
        {
            startGame.Visible = false;
            joinGame.Visible = false;
            playerUsernameInput.Visible = false;
        }

        public void ClientJoin(Button startGame, Button joinGame)
        {
            joinGame.Visible = false;
            startGame.Visible = true;
        }
    }
}
