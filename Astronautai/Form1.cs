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
        const int PlayerStartSize = 16;

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
                //useless??
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
            server.Invoke("AddPlayerOnJoin", player);
        }

        public void AddPlayerPictureBox(Player player)
        {
            var playerPictureBox = new PictureBox
            {
                Name = player.Username,
                Size = new Size(PlayerStartSize, PlayerStartSize),
                Location = new Point(player.X, player.Y),
                Image = Image.FromFile("player.jpg"),
            };
            this.Controls.Add(playerPictureBox);
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
                AddPlayersPictureBoxes(playerList);
                startGame = false;
                gameLoopStarted = true;
            }

            if (gameLoopStarted)
            {
                Console.WriteLine(this.Focused);
                var pictureBox = this.Controls.Find(player.Username, true)[0] as PictureBox;
                pictureBox.Location = new Point(player.X, player.Y);
            }
        }

        private void StartGameButton_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == Keys.W)
            {
                player.Y -= moveAmount;
                server.Invoke("PlayerMovement", player).Wait();
            }
            if (e.KeyCode == Keys.S)
            {
                player.Y += moveAmount;
                server.Invoke("PlayerMovement", player).Wait();
            }
            if (e.KeyCode == Keys.A)
            {
                player.X -= moveAmount;
                server.Invoke("PlayerMovement", player).Wait();
            }
            if (e.KeyCode == Keys.D)
            {
                player.X += moveAmount;
                server.Invoke("PlayerMovement", player);
            }
        }
    }
}
