using Astronautai.Classes;
using Astronautai.Classes.Command;
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
        bool gameOver = false;

        List<Player> playerList;
        public List<Obstacle> obstacles;
        public List<Hazard> hazards = new List<Hazard>();
        int projectileCounter;

        MoveList moveList = new MoveList();
        MoveCommand moveCommand;
        Move move;

        UITextManager uITextManager = new UITextManager();

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
                if (pl.Health <= 0)
                {
                    pictureBox.Visible = false;
                }
                else
                {
                    pictureBox.Location = new Point(pl.X, pl.Y);
                    PlayerImage plImage = pl;
                    if (CurrentPlayerUsername == pl.Username)
                    {
                        Graphics g = Graphics.FromImage(pictureBox.Image);
                        plImage = new CurrentPlayerDecorator(new CurrentPoweredUpDecorator(new NoAmmoDecorator(pl)));
                        string[] playrDc = plImage.GetImage().Split(',');
                       
                        player.X = pl.X;
                        player.Y = pl.Y;
                        move.Player = player;
                        playerBitmap = new Bitmap(player.Size, player.Size);
                        playerBitmap = (Bitmap)Bitmap.FromFile(playrDc[3]);
                        using (var gs = Graphics.FromImage(playerBitmap))
                        {
                            if (pl.Speed > 16)
                            {
                                gs.DrawImage((Bitmap)Bitmap.FromFile(playrDc[2]), player.Size / 4, player.Size / 4);
                            }
                            if (pl.Ammo == 0)
                            {
                                gs.DrawImage((Bitmap)Bitmap.FromFile(playrDc[1]), 15, 15);
                            }
                        }
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
                        plImage = new PoweredUpDecorator(new NoAmmoDecorator(pl));
                        string[] playrDc = plImage.GetImage().Split(',');
                        playerBitmap = new Bitmap(player.Size, player.Size);
                        playerBitmap = (Bitmap)Bitmap.FromFile(playrDc[0]);
                        using (var gs = Graphics.FromImage(playerBitmap))
                        {
                            if (pl.Speed > 16)
                            {
                                gs.DrawImage((Bitmap)Bitmap.FromFile(playrDc[2]), player.Size / 4, player.Size / 4);
                            }
                            if (pl.Ammo == 0)
                            {
                                gs.DrawImage((Bitmap)Bitmap.FromFile(playrDc[1]), 15, 15);
                            }
                        }
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
                }
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
                        //int x = player.Ammo;
                        player = players[i];
                        //player.Ammo = x;
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

            server.On<bool>("gameOver", (over) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    gameOver = true;
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

            server.On<List<Obstacle>>("getObstacles", (obstacle) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    Console.WriteLine("Im getting Obstacles");
                    obstacles = obstacle;
                }));

            });
            server.On<List<Fire>, List<Water>>("getHazards", (fire, water) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    Console.WriteLine("Im getting Hazards");
                    foreach(Fire fr in fire)
                    {
                        hazards.Add(fr);
                    }
                    foreach (Water wt in water)
                    {
                        hazards.Add(wt);
                    }
                    panel1.Paint += new PaintEventHandler(panel1_Draw);
                    panel1.Refresh();
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
            Console.WriteLine("JOINED GAME");
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

            if (!existing && PlayerUsernameTextBox.Text != "")
            {
                player = new Player(PlayerUsernameTextBox.Text, PlayerStartHealth, PlayerStartAmmo, PlayerStartSize, 16);
                player.SetCoordinates(random.Next(100, 700), random.Next(100, 500));
                PlayerNameInputLabel.Text += PlayerUsernameTextBox.Text;
                CurrentPlayerUsername = PlayerUsernameTextBox.Text;
                uITextManager.UpdateButtonsAfterClientJoin(JoinGameButton, StartGameButton);
                server.Invoke("AddPlayerOnJoin", player);
                move = new Move(player, 'W');
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
            server.Invoke("GenerateObstacles");
            server.Invoke("GetObstacles");
            server.Invoke("GetHazards");
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
            timer.Interval = 50;
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

                uITextManager.StartGame(StartGameButton, JoinGameButton, PlayerUsernameTextBox, healthLabel, ammoLabel, PlayerStartHealth, PlayerStartAmmo);
            }

            if (gameOver) {
                var pictureBox = this.Controls.Find(player.Username, true)[0] as PictureBox;
                pictureBox.Visible = false;
                moveCommand = new MoveCommand(move, 'W');
                Execute(move, moveList, moveCommand);
                player.Ammo = 0;
                player.Speed = 0;
                player.Size = 0;
                player.X = -10000;
                player.Y = -10000;
                player.Health = -100;

                uITextManager.GameOver(healthLabel, ammoLabel);
            }
            else if (gameLoopStarted)
            {
                
                if (player.Health <= 0)
                {
                    moveCommand = new MoveCommand(move, 'W');
                    Execute(move, moveList, moveCommand);
                    player.Ammo = 0;
                    player.Speed = 0;
                    player.Size = 0;
                    player.X = -10000;
                    player.Y = -10000;
                    player.Health = -100;

                    uITextManager.PlayerDead(healthLabel, ammoLabel);
                }
                else
                {
                    uITextManager.UpdateLabels(healthLabel, ammoLabel, player.Health, player.Ammo);
                }
            }
        }

        private void playerFocus_KeyDown(object sender, KeyEventArgs e)
        {
            move.UpdatePlayer(player);
            if (e.KeyCode == Keys.W)
            {
                moveCommand = new MoveCommand(move, 'W');
                Execute(move, moveList, moveCommand);
                //player.Rotation = 'W';
                //server.Invoke("MovePlayer", player);
            }
            if (e.KeyCode == Keys.S)
            {
                moveCommand = new MoveCommand(move, 'S');
                Execute(move, moveList, moveCommand);
                //player.Rotation = 'S';
                //server.Invoke("MovePlayer", player);
            }
            if (e.KeyCode == Keys.A)
            {
                moveCommand = new MoveCommand(move, 'A');
                Execute(move, moveList, moveCommand);
                //player.Rotation = 'A';
                //server.Invoke("MovePlayer", player);
            }
            if (e.KeyCode == Keys.D)
            {
                moveCommand = new MoveCommand(move, 'D');
                Execute(move, moveList, moveCommand);
                //player.Rotation = 'D';
                //server.Invoke("MovePlayer", player);
            }
            if (e.KeyCode == Keys.Z)
            {
                moveList.Undo();
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

        private void panel1_Draw(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            foreach (Obstacle obs in obstacles)
            {
                Console.WriteLine("Obstacle " + obs.Id);
                Point[] points = new Point[4];

                points[0] = new Point(obs.coordinates.X, obs.coordinates.Y);
                points[1] = new Point(obs.coordinates.X, obs.coordinates.Y + obs.Size);
                points[2] = new Point(obs.coordinates.X + obs.Size, obs.coordinates.Y + obs.Size);
                points[3] = new Point(obs.coordinates.X + obs.Size, obs.coordinates.Y);

                Brush brush = new SolidBrush(Color.Black);
                g.FillPolygon(brush, points);
            }
            foreach (Hazard hz in hazards)
            {
                Point[] points = new Point[4];

                points[0] = new Point(hz.X, hz.Y);
                points[1] = new Point(hz.X, hz.Y + 20);
                points[2] = new Point(hz.X + 20, hz.Y + 20);
                points[3] = new Point(hz.X + 20, hz.Y);
                Brush brush = new SolidBrush(Color.Blue);
                if (hz.GetType() == typeof(Fire))
                {
                    brush = new SolidBrush(Color.Red);
                }

                g.FillPolygon(brush, points);
            }
        }

        private void AddProjectile(int count)
        {
            Projectile projectile = new Projectile(player);
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
            uITextManager.UpdateAmmo(ammoLabel, player.Ammo);
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
                if (player.Ammo > 0)
                {
                    player.Ammo -= 1;
                    server.Invoke("GetProjectiles").Wait();
                    AddProjectile(projectileCounter);
                }
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
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, asteroidPictureBox.Width - 1, asteroidPictureBox.Height - 1);
                Region rg = new Region(gp);
                asteroidPictureBox.Region = rg;
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
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, asteroidPictureBox.Width - 1, asteroidPictureBox.Height - 1);
                Region rg = new Region(gp);
                asteroidPictureBox.Region = rg;
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
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, asteroidPictureBox.Width - 1, asteroidPictureBox.Height - 1);
                Region rg = new Region(gp);
                asteroidPictureBox.Region = rg;
                this.Controls.Add(asteroidPictureBox);
                asteroidPictureBox.BringToFront();
            }
        }
        private void AddPickup(Pickup pickup)
        {
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

        private void Execute(Move m, MoveList lst, ICommand moveCommand)
        {
            lst.SetCommand(moveCommand);
            lst.Invoke();
        }
    }
}