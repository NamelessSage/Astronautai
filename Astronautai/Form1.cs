using Class_diagram;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Astronautai.Classes.Factory;

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
        public string CurrentPlayerUsername;
        bool startGame = false;
        bool gameLoopStarted = false;
        TempFactory tempFactory = new TempFactory();

        List<Player> playerList;
        List<Projectile> projectileList;
        List<Enemy> creatorList = new List<Enemy>();

        EnemyCreator creator = new EnemyCreator();

        int moveAmount = 2;

        public Form1()
        {
            InitializeComponent();
            InitTimer();
            HubConnection hubConnection = new HubConnection("http://localhost:8080");
            server = hubConnection.CreateHubProxy("serveris");
            playerList = new List<Player>();
            Bitmap btm;
            
            server.On<string, int, int, char>("movePlayer", (name, x, y, rotation) =>
            {
                var pictureBox = this.Controls.Find(name, true)[0] as PictureBox;
                pictureBox.Location = new Point(x, y);

                if (CurrentPlayerUsername == name)
                {
                    player.X = x;
                    player.Y = y;
                    btm = (Bitmap)Bitmap.FromFile(@"..//..//Objects//currentPlayer.png");
                    switch (rotation)
                    {
                        case 'S':
                            btm.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 'A':
                            btm.RotateFlip(RotateFlipType.Rotate270FlipY);
                            break;
                        case 'D':
                            btm.RotateFlip(RotateFlipType.Rotate90FlipY);
                            break;
                    }
                }
                else
                {
                    btm = (Bitmap)Bitmap.FromFile(@"..//..//Objects//player.png");
                    switch (rotation)
                    {
                        case 'S':
                            btm.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 'A':
                            btm.RotateFlip(RotateFlipType.Rotate270FlipY);
                            break;
                        case 'D':
                            btm.RotateFlip(RotateFlipType.Rotate90FlipY);
                            break;
                    }
                }
                pictureBox.Image = btm;
            });

            server.On<List<Player>>("getPlayers", (players) =>
            {
                for (int i = 0; i < players.Count; i++)
                {
                    playerList.Add(players[i]);
                }
            });

            server.On<Pickup>("showPickup", (pic) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    addpickup(pic);
                }));
            });



            server.On<List<Projectile>>("getProjectilesCountCaller", (projectiles) =>
            {
                projectileList = projectiles;
                //AddProjectile(count);
            });

            server.On<List<Projectile>>("getProjectiles", (projectiles) =>
            {
                projectileList.Add(projectiles[projectiles.Count-1]);
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
                            var b = this.Controls.Find("Projectile" + projectile.Id, true);
                            if (b.Length != 0)
                            {
                                var pictureBox = b[0] as PictureBox;
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
                    foreach (Enemy en in asteroids)
                    {
                        if (asteroids.Count != 0)
                        {
                            var b = this.Controls.Find("Asteroid" + en.Id, true);
                            if (b.Length != 0)
                            {
                                var pictureBox = b[0] as PictureBox;
                                pictureBox.Location = new Point(en.X, en.Y);
                            }
                            else
                            {
                                CreateAsteroidPictureBox(en);
                            }
                        }
                    }
                }));
            });


            hubConnection.Start().Wait();
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("JOIN GAME");
            bool existing = false;
            
            server.On<List<Player>>("getPlayersCaller", (players) =>
            {
                foreach(var player in players)
                {
                    if (player.Username == PlayerUsernameTextBox.Text)
                        existing = true;
                }
            });
            server.Invoke("GetPlayersCaller").Wait();
            if (!existing)
            {
                player = new Player(PlayerUsernameTextBox.Text, PlayerStartHealth, PlayerStartSize);
                player.SetCoordinates(random.Next(100, 200), random.Next(100, 200));
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
            timer.Interval = 1000; // in miliseconds
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
                healthLabel.Text = "Health: " + player.Health + "/3";

                server.Invoke("AddAsteroid", "Small");
                server.Invoke("AddAsteroid", "Big");
                server.Invoke("AddAsteroid", "Average");

                
            }

            if (gameLoopStarted)
            {
            }
        }



        private void playerFocus_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.W)
            {
                player.Rotation = 'W';
                server.Invoke("PlayerMovement", player);
                //if (!MovableSpace(player.Y -= moveAmount, 'y'))
                //{
                //    player.Y = 25;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else if (PlayerCollision(player.X, player.Y -= moveAmount))
                //{
                //    player.Y += moveAmount;
                //    player.Y += moveAmount;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else
                //{
                //    player.Y -= moveAmount;
                //    server.Invoke("PlayerMovement", player);
                //}
            }
            if (e.KeyCode == Keys.S)
            {
                player.Rotation = 'S';
                server.Invoke("PlayerMovement", player);
                //if (!MovableSpace(player.Y += moveAmount, 'y'))
                //{
                //    player.Y = 550;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else if (PlayerCollision(player.X, player.Y += moveAmount))
                //{
                //    player.Y -= moveAmount;
                //    player.Y -= moveAmount;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else
                //{
                //    player.Y += moveAmount;
                //    server.Invoke("PlayerMovement", player);
                //}
            }
            if (e.KeyCode == Keys.A)
            {
                player.Rotation = 'A';
                server.Invoke("PlayerMovement", player);
                //if (!MovableSpace(player.X -= moveAmount, 'x'))
                //{
                //    player.X = 25;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else if (PlayerCollision(player.X -= moveAmount, player.Y))
                //{
                //    player.X += moveAmount;
                //    player.X += moveAmount;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else
                //{
                //    player.X -= moveAmount;
                //    server.Invoke("PlayerMovement", player);
                    
                //}
            }
            if (e.KeyCode == Keys.D)
            {
                player.Rotation = 'D';
                server.Invoke("PlayerMovement", player);
                //if (!MovableSpace(player.X+=moveAmount, 'x'))
                //{
                //    player.X = 750;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else if (PlayerCollision(player.X += moveAmount, player.Y))
                //{
                //    player.X -= moveAmount;
                //    player.X -= moveAmount;
                //    server.Invoke("PlayerMovement", player);
                //}
                //else
                //{
                //    player.X += moveAmount;
                //    server.Invoke("PlayerMovement", player);
                //}
            }
            if(e.KeyCode == Keys.M)
            {
                //Pickup pic = (Pickup)tempFactory.GetPickups("Ammo", 100, 100, 1);
                //addpickup(pic);
                server.Invoke("AddPickup");
            }
        }

        //private bool MovableSpace(int coord, char type)
        //{
        //    switch (type)
        //    {
        //        case 'x':
        //            if (player.X < 25 || player.X > 750)
        //                return false;
        //            else
        //            return true;

        //        case 'y':
        //            if (player.Y < 25 || player.Y > 550)
        //                return false;
        //            return true;
        //        default:
        //            return false;
        //    }
        //}

        //private bool PlayerCollision(int x, int y)
        //{
        //    bool collides = false;
        //    server.On<List<Player>>("getPlayersCaller", (players) =>
        //    {
        //        foreach (var pl in players)
        //        {
        //            if (CurrentPlayerUsername != pl.Username)
        //            {
        //                if ((x>pl.X && x < pl.X + PlayerStartSize) && (y>pl.Y && y < pl.Y + PlayerStartSize)||
        //                (x + PlayerStartSize > pl.X && x + PlayerStartSize < pl.X + PlayerStartSize) && (y > pl.Y && y < pl.Y + PlayerStartSize)||
        //                (x > pl.X && x < pl.X + PlayerStartSize) && (y + PlayerStartSize > pl.Y && y + PlayerStartSize < pl.Y + PlayerStartSize)||
        //                (x + PlayerStartSize > pl.X && x + PlayerStartSize < pl.X + PlayerStartSize) && (y + PlayerStartSize > pl.Y && y + PlayerStartSize < pl.Y + PlayerStartSize))
        //                {
        //                    collides = true;
        //                    break;
        //                }
        //            }
        //        }
        //    });
        //    server.Invoke("GetPlayersCaller").Wait();

        //    //ADD Object Collsion!!!!!!!!!!!!!!!!

        //    return collides;
        //}
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (panel1.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 20;//it's up to you
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
            Projectile p = new Projectile();
            p.Player = player;
            p.Direction = player.Rotation;
            switch (player.Rotation)
            {
                case 'W':
                    p.X = player.X;
                    p.Y = player.Y-10;
                    break;
                case 'A':
                    p.X = player.X-10;
                    p.Y = player.Y;
                    break;
                case 'S':
                    p.X = player.X;
                    p.Y = player.Y+10;
                    break;
                case 'D':
                    p.X = player.X+10;
                    p.Y = player.Y;
                    break;
                default:
                    break;
            }
            p.Id = count;
            CreateProjectilePictureBox(p);
            server.Invoke("AddProjectile", p);
        }

        private void CreateProjectilePictureBox(Projectile p)
        {
            var projectilePictureBox = new PictureBox
            {
                Name = "Projectile" + p.Id,
                Size = new Size(5, 5),
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
                server.Invoke("GetProjectilesCountCaller").Wait();
                AddProjectile(projectileList.Count);
            }
        }

        private void CreateAsteroidPictureBox(Enemy p)
        {
            if (p.Size == 20)
            {
                var asteroidPictureBox = new PictureBox
                {
                    Name = "Asteroid" + p.Id,
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
                    Name = "Asteroid" + p.Id,
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
                    Name = "Asteroid" + p.Id,
                    Size = new Size(p.Size, p.Size),
                    Location = new Point(p.X, p.Y),
                    Image = (Bitmap)Bitmap.FromFile(@"..//..//Objects//bigAsteroid.jpg"),
                };
                this.Controls.Add(asteroidPictureBox);
                asteroidPictureBox.BringToFront();
            }
            
        private void addpickup (Pickup pic)
        {
            Console.WriteLine("ok");
            var pickupPictureBox = new PictureBox
            {
                Name = "Ammo",
                Size = new Size(pic.Size, pic.Size),
                Location = new Point(pic.X, pic.Y),
                Image = (Bitmap)Bitmap.FromFile(pic.ImageDirectoryPath)
            };
            this.Controls.Add(pickupPictureBox);
            pickupPictureBox.BringToFront();
        }
    }
}
