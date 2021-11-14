﻿using Class_diagram;
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
