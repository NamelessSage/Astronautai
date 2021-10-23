using Astronautai.Classes;
using Astronautai.Classes.Factory;
using Class_diagram;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Astronautai
{
    public partial class Form1 : Form
    {
        private readonly IHubProxy server;

        const int PlayerStartHealth = 3;
        const int PlayerStartAmmo = 10;
        const int PlayerStartSize = 25;

        const int projectileSize = 5;

        Random random;
        Timer timer;

        Player player;
        public string CurrentPlayerUsername;

        bool startGame = false;
        bool gameLoopStarted = false;

        PickupFactory pickupFactory = new PickupFactory();

        List<Player> playerList;
        int projectileCounter;


        public Form1()
        {
            ServerInput inp = new ServerInput(this);
            InitializeComponent();
            InitTimer();
            random = new Random();

            HubConnection hubConnection = new HubConnection("http://localhost:8080");
            
            server = hubConnection.CreateHubProxy("serveris");

            playerList = new List<Player>();

            Bitmap playerBitmap;

            //Move player
            server.On<Player>("movePlayer", (pl) =>
            {
                var pictureBox = this.Controls.Find(pl.Username, true)[0] as PictureBox;
                pictureBox.Location = new Point(pl.X, pl.Y);
                PlayerImage plImage = pl;
                if (CurrentPlayerUsername == pl.Username)
                {
                    plImage = new CurrentPlayerDecorator(pl);
                    if(pl.Speed != 16)
                    {
                        plImage = new CurrentPoweredUpDecorator(pl);
                    }

                    player.X = pl.X;
                    player.Y = pl.Y;
                    playerBitmap = (Bitmap)Bitmap.FromFile(plImage.GetImage());
                    switch (pl.Rotation)
                    {
                        case 'S':
                            playerBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 'A':
                            playerBitmap.RotateFlip(RotateFlipType.Rotate270FlipY);
                            break;
                        case 'D':
                            playerBitmap.RotateFlip(RotateFlipType.Rotate90FlipY);
                            break;
                    }
                }
                else
                {
                    if (pl.Speed != 16)
                    {
                        plImage = new PoweredUpDecorator(pl);
                    }
                    playerBitmap = (Bitmap)Bitmap.FromFile(plImage.GetImage());
                    switch (pl.Rotation)
                    {
                        case 'S':
                            playerBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 'A':
                            playerBitmap.RotateFlip(RotateFlipType.Rotate270FlipY);
                            break;
                        case 'D':
                            playerBitmap.RotateFlip(RotateFlipType.Rotate90FlipY);
                            break;
                    }
                }
                pictureBox.Image = playerBitmap;
            });

            //Get all player list on local client
            server.On<List<Player>>("getPlayers", (players) =>
            {
                for (int i = 0; i < players.Count; i++)
                {
                    playerList.Add(players[i]);
                }
            });

            //Refresh player data with server tick
            server.On<List<Player>>("updatePlayerData", (players) =>
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (CurrentPlayerUsername == players[i].Username)
                    {
                        player = players[i];
                    }
                }
            });

            //Add pickup on button 'M' press with pickupBuilder
            server.On<Pickup>("addPickup", (pickup) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    AddPickup(pickup);
                }));
            });


            server.On<int>("getProjectiles", (counter) =>
            {
                projectileCounter = counter;
                //AddProjectile(count);
            });


            server.On<bool>("startGame", (start) =>
            {
                startGame = start;
            });

            server.On<List<Projectile>>("updateTicks", (projectiles) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    foreach (Projectile projectile in projectiles)
                    {
                        if (projectiles.Count != 0)
                        {
                            var projectileControl = this.Controls.Find("Projectile" + projectile.Id, true);
                            if (projectileControl.Length != 0)
                            {
                                var pictureBox = projectileControl[0] as PictureBox;
                                pictureBox.Location = new Point(projectile.X, projectile.Y);
                            }
                            else
                            {
                                CreateProjectilePictureBox(projectile);
                            }
                        }
                    }
                }));
            });

            server.On<List<Enemy>>("updateTicksAsteroids", (asteroids) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    foreach (Enemy enemy in asteroids)
                    {
                        if (asteroids.Count != 0)
                        {
                            var asteroidControls = this.Controls.Find("Enemy" + enemy.Id, true);
                            if (asteroidControls.Length != 0)
                            {
                                var pictureBox = asteroidControls[0] as PictureBox;
                                pictureBox.Location = new Point(enemy.X, enemy.Y);
                            }
                            else
                            {
                                CreateAsteroidPictureBox(enemy);
                            }
                        }
                    }
                    
                }));
            });

            server.On<List<Pickup>, int>("updateTicksPickups", (pickups, deleteId) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    foreach (Pickup pickup in pickups)
                    {
                        if (pickups.Count != 0)
                        {
                            var pickupControls = this.Controls.Find("Pickup" + pickup.Id, true);
                            if (pickupControls.Length != 0)
                            {
                                var pictureBox = pickupControls[0] as PictureBox;
                                pictureBox.Location = new Point(pickup.X, pickup.Y);
                            }
                            else
                            {
                                CreatePickupPictureBox(pickup);
                            }
                        }
                    }
                    if (deleteId >= 0)
                    {
                        var pickupControls = this.Controls.Find("Pickup" + deleteId, true);
                        if (pickupControls.Length != 0)
                        {
                            var pictureBox = pickupControls[0] as PictureBox;
                            this.Controls.Remove(pictureBox);
                        }
                    }
                }));
            });

            hubConnection.Start().Wait();
        }


        public void CreatePickupPictureBox(Pickup pickup)
        {
            var pickupPictureBox = new PictureBox
            {
                Name = "Pickup" + pickup.Id,
                Size = new Size(pickup.Size, pickup.Size),
                Location = new Point(pickup.X, pickup.Y),
                Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//ammoPickup.jpg"),
            };
            this.Controls.Add(pickupPictureBox);
            pickupPictureBox.BringToFront();
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("JOIN GAME");
            bool existing = false;

            server.On<List<Player>>("getPlayersCaller", (players) =>
            {
                foreach (var player in players)
                {
                    if (player.Username == PlayerUsernameTextBox.Text)
                        existing = true;
                }
            });
            server.Invoke("GetPlayersCaller").Wait();

            if (!existing)
            {
                player = new Player(PlayerUsernameTextBox.Text, PlayerStartHealth, PlayerStartAmmo, PlayerStartSize, 16);
                player.SetCoordinates(random.Next(100, 700), random.Next(100, 500));
                PlayerNameInputLabel.Text += PlayerUsernameTextBox.Text;
                CurrentPlayerUsername = PlayerUsernameTextBox.Text;
                JoinGameButton.Visible = false;
                StartGameButton.Visible = true;
                server.Invoke("AddPlayerOnJoin", player);
            }
        }

        public void AddPlayerPictureBox(Player player)
        {
            if (player.Username == CurrentPlayerUsername)
            {
                var playerPictureBox = new PictureBox
                {
                    Name = player.Username,
                    Size = new Size(PlayerStartSize, PlayerStartSize),
                    Location = new Point(player.X, player.Y),
                    Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//currentPlayer.png"),
                };
                this.Controls.Add(playerPictureBox);
                playerPictureBox.BringToFront();
            }
            else
            {
                var playerPictureBox = new PictureBox
                {
                    Name = player.Username,
                    Size = new Size(PlayerStartSize, PlayerStartSize),
                    Location = new Point(player.X, player.Y),
                    Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//player.png"),
                };
                this.Controls.Add(playerPictureBox);
                playerPictureBox.BringToFront();
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //CANT FOCUS
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            server.Invoke("GetPlayers").Wait();
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
            timer.Interval = 1000;
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

                healthLabel.Visible = true;
                healthLabel.Text = "Health: " + player.Health + "/" + PlayerStartHealth;
                ammoLabel.Visible = true;
                ammoLabel.Text = "Ammo: " + player.Ammo + "/" + PlayerStartAmmo;

                server.Invoke("AddAsteroid", "Small");
                server.Invoke("AddAsteroid", "Big");
                server.Invoke("AddAsteroid", "Average");
            }

            if (gameLoopStarted)
            {
                healthLabel.Text = "Health: " + player.Health + "/" + PlayerStartHealth;
                ammoLabel.Text = "Ammo: " + player.Ammo + "/" + PlayerStartAmmo;
            }
        }

        private void playerFocus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                player.Rotation = 'W';
                server.Invoke("MovePlayer", player);
            }
            if (e.KeyCode == Keys.S)
            {
                player.Rotation = 'S';
                server.Invoke("MovePlayer", player);
            }
            if (e.KeyCode == Keys.A)
            {
                player.Rotation = 'A';
                server.Invoke("MovePlayer", player);
            }
            if (e.KeyCode == Keys.D)
            {
                player.Rotation = 'D';
                server.Invoke("MovePlayer", player);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (panel1.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 20;
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              panel1.ClientSize.Width - thickness,
                                                              panel1.ClientSize.Height - thickness));
                }
            }
        }

        private void AddProjectile(int count)
        {
            Projectile projectile = new Projectile();
            projectile.Player = player;
            projectile.Direction = player.Rotation;
            switch (player.Rotation)
            {
                case 'W':
                    projectile.X = player.X + (PlayerStartSize / 2);
                    projectile.Y = player.Y;
                    break;
                case 'A':
                    projectile.X = player.X;
                    projectile.Y = player.Y + (PlayerStartSize / 2);
                    break;
                case 'S':
                    projectile.X = player.X + (PlayerStartSize / 2);
                    projectile.Y = player.Y + PlayerStartSize;
                    break;
                case 'D':
                    projectile.X = player.X + PlayerStartSize;
                    projectile.Y = player.Y + (PlayerStartSize / 2);
                    break;
                default:
                    break;
            }
            ammoLabel.Text = "Ammo: " + player.Ammo + "/" + PlayerStartAmmo;

            projectile.Id = count;
            CreateProjectilePictureBox(projectile);
            server.Invoke("AddProjectile", projectile, player);
        }

        private void CreateProjectilePictureBox(Projectile p)
        {
            var projectilePictureBox = new PictureBox
            {
                Name = "Projectile" + p.Id,
                Size = new Size(projectileSize, projectileSize),
                Location = new Point(p.X, p.Y),
                Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//projectile.png"),
            };

            this.Controls.Add(projectilePictureBox);
            projectilePictureBox.BringToFront();
        }

        private void playerFocus_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                player.Ammo -= 1;
                server.Invoke("GetProjectiles").Wait();
                AddProjectile(projectileCounter);
            }
        }

        private void CreateAsteroidPictureBox(Enemy p)
        {
            if (p.Size == 20)
            {
                var asteroidPictureBox = new PictureBox
                {
                    Name = "Enemy" + p.Id,
                    Size = new Size(p.Size, p.Size),
                    Location = new Point(p.X, p.Y),
                    Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//smulAsteroid.jpg"),
                };
                this.Controls.Add(asteroidPictureBox);
                asteroidPictureBox.BringToFront();
            }
            else if (p.Size == 35)
            {
                var asteroidPictureBox = new PictureBox
                {
                    Name = "Enemy" + p.Id,
                    Size = new Size(p.Size, p.Size),
                    Location = new Point(p.X, p.Y),
                    Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//averageAsteroid.jpg"),
                };
                this.Controls.Add(asteroidPictureBox);
                asteroidPictureBox.BringToFront();
            }
            else if (p.Size == 50)
            {
                var asteroidPictureBox = new PictureBox
                {
                    Name = "Enemy" + p.Id,
                    Size = new Size(p.Size, p.Size),
                    Location = new Point(p.X, p.Y),
                    Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//bigAsteroid.jpg"),
                };
                this.Controls.Add(asteroidPictureBox);
                asteroidPictureBox.BringToFront();
            }
        }
        private void AddPickup(Pickup pickup)
        {
            Console.WriteLine("Pickup added!");
            var pickupPictureBox = new PictureBox
            {
                Name = "Pickup" + pickup.Id,
                Size = new Size(pickup.Size, pickup.Size),
                Location = new Point(pickup.X, pickup.Y),
                Image = (Bitmap)Bitmap.FromFile(pickup.ImagePath)
            };
            this.Controls.Add(pickupPictureBox);
            pickupPictureBox.BringToFront();
        }
    }
}