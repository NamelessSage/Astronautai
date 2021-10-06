using Class_diagram;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace Astronautai
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Timer timer;

        private readonly IHubProxy server;
        Player player;

        const int PlayerStartHealth = 3;
        const int PlayerStartSize = 25;

        bool startGame = false;
        bool gameLoopStarted = false;

        List<Player> playerList;

        int moveAmount = 10;

        public Form1()
        {
            InitializeComponent();
            InitTimer();
            HubConnection hubConnection = new HubConnection("http://localhost:8080");
            server = hubConnection.CreateHubProxy("serveris");
            playerList = new List<Player>();

            server.On<string, int, int>("movePlayer", (name, x, y) =>
            {
                var pictureBox = this.Controls.Find(name, true)[0] as PictureBox;
                pictureBox.Location = new Point(x, y);

                var label = this.Controls.Find("playerLabel" + name, true)[0] as Label;
                label.Location = new Point(x, y+25);
            });

            server.On<List<Player>>("getPlayers", (players) =>
            {
                for (int i = 0; i < players.Count; i++)
                {
                    playerList.Add(players[i]);
                }
            });
           
            server.On<bool>("startGame", (start) =>
            {
                startGame = start;
            });

            hubConnection.Start().Wait();
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("JOIN GAME");
            player = new Player(PlayerUsernameTextBox.Text, PlayerStartHealth, PlayerStartSize);
            player.SetCoordinates(random.Next(100, 200), random.Next(100, 200));
            PlayerNameInputLabel.Text += PlayerUsernameTextBox.Text;
            JoinGameButton.Visible = false;
            StartGameButton.Visible = true;
            server.Invoke("AddPlayerOnJoin", player);
        }

        public void AddPlayerPictureBox(Player player)
        {
            var playerPictureBox = new PictureBox
            {
                Name = player.Username,
                Size = new Size(PlayerStartSize, PlayerStartSize),
                Location = new Point(player.X, player.Y),
                Image = Image.FromFile(@"..//..//Objects//player.png"),
            };
            var playerLabel = new Label
            {
                Name = "playerLabel" + player.Username,
                Text = player.Username,
                Location = new Point(player.X, player.Y+25),
               
            };
            this.Controls.Add(playerPictureBox);
            this.Controls.Add(playerLabel);
            //playerLabel.BringToFront();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //CANT FOCUS
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            server.Invoke("GetPlayers").Wait();
            //MAKE START BUTTON/USERNAMETEXTBOX/JOINBUTTON INVISIBLE FOR EVERYONE 
            server.Invoke("StartGame");
        }

        private void AddPlayersPictureBoxes(List<Player> playerList)
        {
            foreach (var player in playerList)
            {
                AddPlayerPictureBox(player);
            }
        }

        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = 100; // in miliseconds
            timer.Start();
        }

        //GAME UPDATE AFTER START
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (startGame)
            {
                this.ActiveControl = playerFocus;
                AddPlayersPictureBoxes(playerList);
                startGame = false;
                gameLoopStarted = true;
                StartGameButton.Visible = false;
                JoinGameButton.Visible = false;
                PlayerUsernameTextBox.Visible = false;
            }

            if (gameLoopStarted)
            {
            }
        }


        private void playerFocus_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.W)
            {
                if (MovableSpace(player.Y -= moveAmount, 'y'))
                {
                    player.Y -= moveAmount;
                    server.Invoke("PlayerMovement", player);
                }
                else
                {
                    player.Y = 25;
                    server.Invoke("PlayerMovement", player);
                }
            }
            if (e.KeyCode == Keys.S)
            {
                if (MovableSpace(player.Y += moveAmount, 'y'))
                {
                    player.Y+= moveAmount;
                    server.Invoke("PlayerMovement", player);
                }
                else
                {
                    player.Y = 550;
                    server.Invoke("PlayerMovement", player);
                }
            }
            if (e.KeyCode == Keys.A)
            {
                if (MovableSpace(player.X -= moveAmount, 'x'))
                {
                    player.X -= moveAmount;
                    server.Invoke("PlayerMovement", player);
                }
                else
                {
                    player.X = 25;
                    server.Invoke("PlayerMovement", player);
                }
            }
            if (e.KeyCode == Keys.D)
            {
                if (MovableSpace(player.X+=moveAmount, 'x'))
                {
                    player.X += moveAmount;
                    server.Invoke("PlayerMovement", player);
                }
                else
                {
                    player.X = 750;
                    server.Invoke("PlayerMovement", player);
                }

            }
        }

        private bool MovableSpace(int coord, char type)
        {
            switch (type)
            {
                case 'x':
                    if (player.X < 25 || player.X > 750)
                    {
                        return false;
                    }
                    return true;

                case 'y':
                    if (player.Y < 25 || player.Y > 550)
                    {
                        return false;
                    }
                    return true;

                default:
                    return false;
            }
        }
    }
}
