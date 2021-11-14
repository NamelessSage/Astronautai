using Class_diagram;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GameServer;
using System.Collections.Generic;
using Astronautai.Classes.Factory;
using Astronautai.Classes;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        GameData gameData = new GameData();
        [DataTestMethod]
        [DataRow(100, 100, 'W')]
        [DataRow(100, 100, 'A')]
        [DataRow(100, 100, 'S')]
        [DataRow(100, 100, 'D')]
        [DataRow(100, 100, 'Q')]
        [DataRow(100, 100, 'E')]
        [DataRow(100, 100, 'Z')]
        [DataRow(100, 100, 'C')]

        public void TestEnemyMove(int x,int y, char rot)
        {
            Enemy enemy = new Enemy();
            enemy.Speed = 10;
            enemy.X = x;
            enemy.Y = y;
            enemy.Rotation = rot;
            enemy.Move();
            if (rot == 'W' || rot== 'A' || rot == 'S' || rot == 'D')
            {
                Assert.AreEqual(enemy.Speed, Math.Abs(enemy.X+enemy.Y-x-y));
            }
            else
            {
                Assert.AreEqual(enemy.Speed, Math.Abs(enemy.X - x));
                Assert.AreEqual(enemy.Speed, Math.Abs(enemy.Y - y));
            }
        }
        [TestMethod]
        public void TestEnemyMoveBadRotation()
        {
            Enemy enemy = new Enemy();
            enemy.Speed = 10;
            enemy.X = 100;
            enemy.Y = 100;
            enemy.Rotation = 'G';
            enemy.Move();
            Assert.AreEqual(100, enemy.X);
            Assert.AreEqual(100, enemy.Y);
        }
        [TestMethod]
        public void TestEnemySetMoveNone()
        {
            Enemy enemy = new Enemy();
            enemy.Speed = 10;
            enemy.SetMoveNone();
            Assert.AreEqual(0, enemy.Speed);
        }
        [TestMethod]
        public void TestEnemySetMoveSlow()
        {
            Enemy enemy = new Enemy();
            enemy.Speed = 10;
            enemy.SetMoveSlow();
            Assert.AreEqual(1, enemy.Speed);
        }
        [TestMethod]
        public void TestEnemySetMoveAverage()
        {
            Enemy enemy = new Enemy();
            enemy.Speed = 10;
            enemy.SetMoveAverage();
            Assert.AreEqual(2, enemy.Speed);
        }
        [TestMethod]
        public void TestEnemySetMoveFast()
        {
            Enemy enemy = new Enemy();
            enemy.Speed = 10;
            enemy.SetMoveFast();
            Assert.AreEqual(3, enemy.Speed);
        }
        [TestMethod]
        public void TestProjectileConstructor()
        {
            Projectile projectile = new Projectile();
            Player player = createTestPlayer();
            projectile.Id = 0;
            projectile.Player = player;
            projectile.Direction = 'W';
            Assert.AreEqual(0, projectile.Id);
            Assert.AreEqual("test", projectile.Player.Username);
        }
        [TestMethod]
        public void TestPlayerConstructorUserName()
        {
            Player player = new Player("test");
            Assert.AreEqual("test", player.Username);
        }
        [TestMethod]
        public void TestPlayerAddSpeed()
        {
            Player player = createTestPlayer(10, 10, 'W', 3);
            player.Speed = 1;
            Assert.AreEqual(1, player.Speed);
            player.AddSpeed(1);
            Assert.AreEqual(2, player.Speed);
        }
        [TestMethod]
        public void TestPlayerAddHealth()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(100,100,'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            player.Health = 1;
            Assert.AreEqual(1, player.Health);
            player.AddHealth(1);
            Assert.AreEqual(2, player.Health);
        }
        [TestMethod]
        public void TestPlayerAddAmmo()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(100, 100, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            player.Ammo = 1;
            Assert.AreEqual(1, player.Ammo);
            player.AddAmmo(1);
            Assert.AreEqual(2, player.Ammo);
        }
        [TestMethod]
        public void TestPlayerRemoveAmmo()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(100, 100, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            Assert.AreEqual(10, player.Ammo);
            player.RemoveAmmo();
            Assert.AreEqual(9, player.Ammo);
        }
        [TestMethod]
        public void TestPlayerGetImage()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(100, 100, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            Assert.AreEqual(@"..//..//Objects//player.png", player.GetImage());
        }

        [TestMethod]
        public void TestSmallAsteroidConstructor()
        {
            Coordinates coordinates = new Coordinates(100,100);
            SmallAsteroid small = new SmallAsteroid(0, 'W', coordinates);

            Assert.AreEqual(small.Id, 0);
            Assert.AreEqual(small.Health, 1);
            Assert.AreEqual(small.Damage, 1);
            Assert.AreEqual(small.Size, 20);
            Assert.AreEqual(small.X, 100);
            Assert.AreEqual(small.Y, 100);
        }

        [TestMethod]
        public void TestSBigAsteroidConstructor()
        {
            Coordinates coordinates = new Coordinates(100, 100);
            BigAsteroid big = new BigAsteroid(0, 'W', coordinates);

            Assert.AreEqual(big.Id, 0);
            Assert.AreEqual(big.Health, 3);
            Assert.AreEqual(big.Damage, 2);
            Assert.AreEqual(big.Size, 50);
            Assert.AreEqual(big.X, 100);
            Assert.AreEqual(big.Y, 100);
        }

        [TestMethod]
        public void TestSAverageAsteroidConstructor()
        {
            Coordinates coordinates = new Coordinates(100, 100);
            AverageAsteroid average = new AverageAsteroid(0, 'W', coordinates);

            Assert.AreEqual(average.Id, 0);
            Assert.AreEqual(average.Health, 2);
            Assert.AreEqual(average.Damage, 1);
            Assert.AreEqual(average.Size, 35);
            Assert.AreEqual(average.X, 100);
            Assert.AreEqual(average.Y, 100);
        }

        [TestMethod]
        public void TestSpeedPickup()
        {
            Player player = createTestPlayer(10, 10, 'W', 3);
            int oldSpeed = player.Speed;
            OnePickupFactory factory = new OnePickupFactory();
            SpeedPickup pickup = (SpeedPickup)factory.CreateSpeedPickup();

            player = pickup.Action(player, pickup);

            Assert.AreEqual(oldSpeed + 1, player.Speed);
        }
        [TestMethod]
        public void TestAmmoPickup()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(10, 10, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            player.RemoveAmmo();
            int oldAmmo = player.Ammo;
            Assert.AreEqual(9, oldAmmo);
            OnePickupFactory factory = new OnePickupFactory();
            AmmoPickup pickup = (AmmoPickup)factory.CreateAmmoPickup();

            player = pickup.Action(player, pickup);

            Assert.AreEqual(oldAmmo + 1, player.Ammo);
        }
        [TestMethod]
        public void TestHealthPickup()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(10, 10, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            player.Health = 1;
            int oldHealth = player.Health;
            Assert.AreEqual(1, oldHealth);
            OnePickupFactory factory = new OnePickupFactory();
            HealthPickup pickup = (HealthPickup)factory.CreateHealthPickup();

            player = pickup.Action(player, pickup);

            Assert.AreEqual(oldHealth + 1, player.Health);
        }

        [TestMethod]
        public void TestSpeedMaxPickup()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(10, 10, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            int oldSpeed = player.Speed;
            MaxPickupFactory factory = new MaxPickupFactory();
            SpeedPickup pickup = (SpeedPickup)factory.CreateSpeedPickup();

            player = pickup.Action(player, pickup);

            Assert.AreEqual(oldSpeed + 10, player.Speed);
        }
        [TestMethod]
        public void TestAmmoMaxPickup()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(10, 10, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            player.Ammo=5;
            Assert.AreEqual(5, player.Ammo);
            MaxPickupFactory factory = new MaxPickupFactory();
            AmmoPickup pickup = (AmmoPickup)factory.CreateAmmoPickup();

            player = pickup.Action(player, pickup);

            Assert.AreEqual(10, player.Ammo);
        }
        [TestMethod]
        public void TestHealthMaxPickup()
        {
<<<<<<< HEAD
            Player player = createTestPlayer(10, 10, 'W', 3);
=======
            Player player = createTestPlayer(10, 10, 'W',3);
>>>>>>> cbd568cd672e6f74b2629b825a658e48fb12abff
            player.Health = 1;
            int oldHealth = player.Health;
            Assert.AreEqual(1, oldHealth);
            MaxPickupFactory factory = new MaxPickupFactory();
            HealthPickup pickup = (HealthPickup)factory.CreateHealthPickup();

            player = pickup.Action(player, pickup);

            Assert.AreEqual(oldHealth + 2, player.Health);
        }

        [TestMethod]
        public void TestPlayerCanMoveTrue()
        {
            Player player = createTestPlayer(100, 100, 'W', 3);
            Player playerMove = gameData.PlayerCanMove(createTestPlayer(100, 100, 'W', 3));

            Assert.AreEqual(player.Y - player.Speed, playerMove.Y);
            playerMove = gameData.PlayerCanMove(createTestPlayer(100, 100, 'A', 3));
            Assert.AreEqual(player.X - player.Speed, playerMove.X);
            playerMove = gameData.PlayerCanMove(createTestPlayer(100, 100, 'S', 3));
            Assert.AreEqual(player.Y + player.Speed, playerMove.Y);
            playerMove = gameData.PlayerCanMove(createTestPlayer(100, 100, 'D', 3));
            Assert.AreEqual(player.X + player.Speed, playerMove.X);
        }
        [TestMethod]
        public void TestCheckCollisionPlayersTrue()
        {
            Player player = createTestPlayer(10, 10, 'W',3);
            Player player1 = createTestPlayer(15, 15, 'A', 3);
            gameData.GenerateObstacles();
            gameData.AddPlayer(player);
            gameData.AddPlayer(player1);
            Assert.AreEqual(false, gameData.CheckCollisionPlayers(player));
        }
        [TestMethod]
        public void TestCheckCollisionEnemyTrue()
        {
            Enemy enemy = createTestEnemy();
            Assert.AreEqual(true, gameData.CheckCollisionEnemy(enemy));
        }

        [TestMethod]
        public void TestCheckNotCollisionPickup()
        {
            Pickup pickup = createTestPickup(100,100);
            Assert.AreEqual(true, gameData.CheckNotCollisionPickup(pickup));
            pickup = createTestPickup(10, 10);
            Assert.AreEqual(false, gameData.CheckNotCollisionPickup(pickup));
        }
        [TestMethod]
        public void TestAddPlayerAndGetPlayer()
        {
            Player player = createTestPlayer(10, 10, 'W',3);
            gameData.AddPlayer(player);
            Assert.AreEqual(player, gameData.GetPlayers()[1]);
        }

        [TestMethod]
        public void TestAddProjectileAndGetProjectile()
        {
            Projectile projectile = createTestProjectile(10,10, 'W');
            gameData.AddProjectile(projectile);
            Assert.AreEqual(projectile, gameData.GetProjectiles()[0]);
        }

        [TestMethod]
        public void TestUpdateProjectilesCoords()
        {
            Projectile projectile = createTestProjectile(10, 10, 'W');
            Projectile projectile1 = createTestProjectile(10, 10, 'A');
            Projectile projectile2 = createTestProjectile(10, 10, 'S');
            Projectile projectile3 = createTestProjectile(10, 10, 'D');
            gameData.AddProjectile(projectile1);
            gameData.AddProjectile(projectile2);
            gameData.AddProjectile(projectile3);
            gameData.UpdateProjectileCoords();
            projectile1 = createTestProjectile(10, 10, 'A');
            projectile2 = createTestProjectile(10, 10, 'S');
            projectile3 = createTestProjectile(10, 10, 'D');
            Assert.AreEqual(projectile.Y - 10, gameData.GetProjectiles()[0].Y);
            Assert.AreEqual(projectile1.X - 10, gameData.GetProjectiles()[1].X);
            Assert.AreEqual(projectile2.Y + 10, gameData.GetProjectiles()[2].Y);
            Assert.AreEqual(projectile3.X + 10, gameData.GetProjectiles()[3].X);
        }
        [TestMethod]
        public void TestCheckMapEdge()
        {
            Assert.AreEqual(true, gameData.CheckMapEdge(new Coordinates(110,100)));
            Assert.AreEqual(false, gameData.CheckMapEdge(new Coordinates(1100, 1000)));
        }
        [TestMethod]
        public void TestAveragePlayerHealth()
        {
            Assert.AreEqual(3, gameData.GetAveragePlayerHealth());
        }
        [TestMethod]
        public void TestGenerateObstaclesAndGetObstacles()
        {
            gameData.GenerateObstacles();
            List<Obstacle> obs = new List<Obstacle>();
            Assert.AreNotEqual(obs, gameData.GetObstacles());
        }
        [TestMethod]
        public void TestUpdatePlayer()
        {
            Player player = createTestPlayer(10, 10, 'W',3);
            gameData.UpdatePlayer(player);
            Assert.AreEqual(player, gameData.GetPlayers()[0]);
        }
        [TestMethod]
        public void TestGetMap()
        {
            Assert.IsNotNull(gameData.GetMap());
        }
        [TestMethod]
        public void TestAddAsteroidAndUpdateAsteroidCoords()
        {
            gameData.AddPlayer(createTestPlayer(10, 10, 'W',3));
            gameData.AddAsteroid();
            gameData.UpdateAsteroidCoords();
            Assert.IsNotNull(gameData.GetEnemies());
        }

        public Player createTestPlayer(int x, int y, char rot, int hp)
        {
            Player player = new Player("test"+x.ToString(), hp, 10, 25, 16);
            player.SetCoordinates(x, y);
            player.Rotation = rot;
            return player;
        }

        [TestMethod]
        public void TestBuilder()
        {
            PickupBuilder builder = new PickupBuilder();
            builder.SetType("AmmoPickup");
            builder.SetId(10);
            builder.SetCoordinates(10, 10);
            builder.SetImage("10");
            builder.SetSize(10);
            builder.SetValue(10);
            AmmoPickup pic = (AmmoPickup)builder.GetBuildable();
            AmmoPickup pic2 = new AmmoPickup() { Id = 10, X = 10, Y = 10, ImagePath = "10", Size = 10, Value = 10 };
            Assert.AreEqual(pic.X, pic2.X);
            Assert.AreEqual(pic.Y, pic2.Y);
            Assert.AreEqual(pic.ImagePath, pic2.ImagePath);
            Assert.AreEqual(pic.Size, pic2.Size);
            Assert.AreEqual(pic.Value, pic2.Value);
            Assert.AreEqual(pic.Value, pic2.Value);
            Assert.AreEqual(pic.Type, pic2.Type);


        }

        [TestMethod]
        public void TestMap()
        {
            Map map = Map.Instance;
            Map map2 = Map.Instance;
            Assert.AreEqual(map, map2);
        }
        [TestMethod]
        public void TestMove()
        {
            Player player = createTestPlayer();
            Move move = new Move(player,'W');
            Move movetest = move;
            move.MoveD();
            Assert.AreEqual(move.Direction, 'D');
            move.MoveS();
            Assert.AreEqual(move.Direction, 'S'); 
            move.MoveW();
            Assert.AreEqual(move.Direction, 'W');  
            move.MoveA();
            Assert.AreEqual(move.Direction, 'A');

            move.UndoD();
            Assert.AreEqual(move.Direction, 'D');
            move.UndoS();
            Assert.AreEqual(move.Direction, 'S');
            move.UndoW();
            Assert.AreEqual(move.Direction, 'W');
            move.UndoA();
            Assert.AreEqual(move.Direction, 'A');

            move.UpdatePlayer(movetest.Player);

            Assert.AreEqual(movetest.Player, move.Player);

        }
        public Player createTestPlayer()
        {
            Player player = new Player("test", 3, 10, 25, 16);
            player.SetCoordinates(100, 100);
            player.Rotation = 'W';
            return player;
        }

        [TestMethod]
        public void TestAddPlayerOnJoinHub()
        {
            GameHub hub = new GameHub();
            hub.AddPlayerOnJoin(createTestPlayer(10, 10, 'W', 3));
            Assert.AreEqual(3,hub.data.GetPlayers().Count);
        }


        public Enemy createTestEnemy()
        {
            Enemy enemy = new Enemy();
            enemy.Size = 10;
            enemy.X = 100;
            enemy.Y = 100;
            enemy.Rotation = 'W';
            return enemy;
        }
        public Pickup createTestPickup(int x, int y)
        {
            Pickup pickup = new Pickup();
            pickup.Size = 10;
            pickup.X = x;
            pickup.Y = y;
            return pickup;
        }

        public Projectile createTestProjectile(int x, int y, char dir)
        {
            Projectile projectile = new Projectile();
            projectile.X = x;
            projectile.Y = y;
            projectile.Direction = dir;
            return projectile;
        }
    }
}
