using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    class UITextManager
    {
        HealthManager HealthManager;
        AmmoManager AmmoManager;
        ButtonManager ButtonManager;

        string GameOverHealthString;
        string GameOverAmmoString;
        string PlayerDeadString;

        public UITextManager() 
        {
            HealthManager = new HealthManager();
            AmmoManager = new AmmoManager();
            GameOverHealthString = "GG WP";
            GameOverAmmoString = "";
            PlayerDeadString = "Dead";
        }

        public UITextManager(string gameOverString)
        {
            HealthManager = new HealthManager();
            AmmoManager = new AmmoManager();
            GameOverHealthString = gameOverString;
            GameOverAmmoString = "";
            PlayerDeadString = "Dead";
        }

        public void StartGame(Button startGame, Button joinGame, TextBox playerUsernameInput, Label healthLabel, Label ammoLabel, int playerStartHealth, int playerStartAmmo)
        {
            ButtonManager.StartGame(startGame, joinGame, playerUsernameInput);
            DisplayLabels(healthLabel, ammoLabel, playerStartHealth, playerStartAmmo);
        }

        public void UpdateLabels(Label healthLabel, Label ammoLabel, int playerHealth, int playerAmmo)
        {
            HealthManager.UpdateLabel(healthLabel, playerHealth);
            AmmoManager.UpdateLabel(ammoLabel, playerAmmo);
        }

        public void DisplayLabels(Label healthLabel, Label ammoLabel, int playerStartHealth, int playerStartAmmo)
        {
            HealthManager.DisplayLabel(healthLabel, playerStartHealth);
            AmmoManager.DisplayLabel(ammoLabel, playerStartAmmo);
        }

        public void GameOver(Label healthLabel, Label ammoLabel)
        {
            healthLabel.Text = GameOverHealthString;
            ammoLabel.Text = GameOverAmmoString;
        }

        public void PlayerDead(Label healthLabel, Label ammoLabel)
        {
            healthLabel.Text = PlayerDeadString;
            ammoLabel.Text = "";
        }

        public void UpdateAmmo(Label ammoLabel, int playerAmmo)
        {
            AmmoManager.UpdateLabel(ammoLabel, playerAmmo);
        }

        public void UpdateHealth(Label healthLabel, int playerHealth)
        {
            HealthManager.UpdateLabel(healthLabel, playerHealth);
        }

        public void UpdateButtonsAfterClientJoin(Button joinGameButton, Button startGameButton)
        {
            ButtonManager.ClientJoin(startGameButton, joinGameButton);
            joinGameButton.Visible = false;
            startGameButton.Visible = true;
        }
    }
}
