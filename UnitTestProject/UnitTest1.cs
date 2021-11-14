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
            Player player = createTestPlayer();
            int oldSpeed = player.Speed;
            OnePickupFactory factory = new OnePickupFactory();
            SpeedPickup pickup = (SpeedPickup)factory.CreateSpeedPickup();
            
            player = pickup.Action(player, pickup);

            Assert.AreEqual(oldSpeed + 1, player.Speed);
        }


        [TestMethod]
        public void TestPlayerCanMoveTrue()
        {
            Player player = createTestPlayer();
            Player playerMove = gameData.PlayerCanMove(createTestPlayer());

            Assert.AreEqual(player.Y - player.Speed, playerMove.Y);
        }
        [TestMethod]
        public void TestCheckCollisionPlayersTrue()
        {
            Player player = createTestPlayer();
            Assert.AreEqual(true, gameData.CheckCollisionPlayers(player));
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
            Pickup pickup = createTestPickup(10,10);
            Assert.AreEqual(true, gameData.CheckNotCollisionPickup(pickup));
            pickup = createTestPickup(100, 100);
            Assert.AreEqual(false, gameData.CheckNotCollisionPickup(pickup));
        }
        [TestMethod]
        public void TestAddPlayerAndGetPlayer()
        {
            Player player = createTestPlayer();
            gameData.AddPlayer(player);
            Assert.AreEqual(player, gameData.GetPlayers()[0]);
        }

        [TestMethod]
        public void TestAddProjectileAndGetProjectile()
        {
            Projectile projectile = createTestProjectile(10,10);
            gameData.AddProjectile(projectile);
            Assert.AreEqual(projectile, gameData.GetProjectiles()[0]);
        }

        [TestMethod]
        public void TestUpdateProjectilesCoords()
        {
            Projectile projectile = createTestProjectile(10, 10);
            gameData.UpdateProjectileCoords();
            Assert.AreEqual(projectile.Y - 10, gameData.GetProjectiles()[0].Y);
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
        public void TestBuilder()
        {
            PickupBuilder builder = new PickupBuilder();
            builder.SetId(10);
            builder.SetCoordinates(10, 10);
            builder.SetImage("10");
            builder.SetSize(10);
            builder.SetValue(10);
            Pickup pic = builder.GetBuildable();
            Pickup pic2 = new Pickup() { Id = 10, X = 10, Y = 10, ImagePath = "10", Size = 10, Value = 10 };
            Assert.AreEqual(pic.X, pic2.X);
            Assert.AreEqual(pic.Y, pic2.Y);
            Assert.AreEqual(pic.ImagePath, pic2.ImagePath);
            Assert.AreEqual(pic.Size, pic2.Size);
            Assert.AreEqual(pic.Value, pic2.Value);
        }

        [TestMethod]
        public void TestMap()
        {
            Map map = Map.Instance;
            Map map2 = Map.Instance;
            Assert.AreEqual(map, map2);
        }

        public Player createTestPlayer()
        {
            Player player = new Player("test", 3, 10, 25, 16);
            player.Speed = 10;
            player.SetCoordinates(100, 100);
            player.Rotation = 'W';
            return player;
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

        public Projectile createTestProjectile(int x, int y)
        {
            Projectile projectile = new Projectile();
            projectile.X = x;
            projectile.Y = y;
            projectile.Direction = 'W';
            return projectile;
        }
    }
}
