using Class_diagram;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GameServer;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        GameData gameData = new GameData();
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
        public void TestCheckNotCollisionPickupTrue()
        {
            Pickup pickup = createTestPickup();
            Assert.AreEqual(true, gameData.CheckNotCollisionPickup(pickup));
        }

        public Player createTestPlayer()
        {
            Player player = new Player("test", 3, 10, 25, 16);
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
            return enemy;
        }
        public Pickup createTestPickup()
        {
            Pickup pickup = new Pickup();
            pickup.Size = 10;
            pickup.X = 100;
            pickup.Y = 100;
            return pickup;
        }
    }
}
