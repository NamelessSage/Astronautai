﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Astronautai;
using Astronautai.Classes.Factory;
using Class_diagram;
using System.Windows.Forms;
using System.Timers;


namespace GameServer
{
    [HubName("serveris")]
    public class GameHub : Hub
    {
        GameData data = new GameData();
        bool started = false;
        private System.Timers.Timer _timer;
        private int _timerInterval = 50;

        int counter = 0;
        
        public GameHub()
        {

        }

        public GameHub GetHub()
        {
            return this;
        }

        public void AddPlayerOnJoin(Player player)
        {
            data.AddPlayer(player);
        }

        public void GetPlayers()
        {
            List<Player> players = data.GetPlayers();

            Clients.All.getPlayers(players);
        }

        public void GetPlayersCaller()
        {

            List<Player> players = data.GetPlayers();
            Console.WriteLine("Returnin players to caller - " + players.Count());

            Clients.Caller.getPlayersCaller(players); //SEND TO EVERYONE ??
        }

        public void StartGame()
        {
            Console.WriteLine("Game started");
            if (!started)
            {
                started = true;
                StartTimer();

                Clients.All.startGame(true);
            }
        }

        public void MovePlayer(Player player)
        {
            player = data.PlayerCanMove(player);
            data.UpdatePlayer(player);
            Console.WriteLine($"Moving player: {player.Username} {player.X} {player.Y} {player.Rotation}");
            Clients.All.movePlayer(player.Username, player.X, player.Y, player.Rotation);
        }

        public void AddProjectile(Projectile projectile, Player player)
        {
            Console.WriteLine("Adding projectile with id = " + projectile.Id);
            data.UpdatePlayer(player);
            data.AddProjectile(projectile);
        }

        public void UpdateTicks(object source, ElapsedEventArgs e)
        {
            data.UpdateProjectileCoords();

            int deleteEnemyId = data.UpdateAsteroidCoords();
            Clients.All.updateTicks(data.GetProjectiles());

            Clients.All.updateTicksAsteroids(data.GetEnemies(), deleteEnemyId);

            int deletePickupId = data.UpdatePickups();
            Clients.All.updateTicksPickups(data.GetPickups(), deletePickupId);

            Clients.All.updatePlayerData(data.GetPlayers());
            counter++;
            if(counter > 10)
            {
                data.AddAsteroid();
                counter = 0;
            }
        }

        public void GetProjectiles()
        {
            Map map = data.GetMap();
            Clients.Caller.getProjectiles(map.projectileCounter);
            map.projectileCounter++;
        }

        public void AddPickup()
        {
            //Pickup pickup = (Pickup)data.pickupFactory.BuildPickup("Ammo", 1);

            //Pickup pickup = (Pickup)data.onepickupFactory.CreateHealthPickup();
            Console.WriteLine("ADD PICKUP 0");
            Pickup pickup = data.pickupSpawner.SpawnRandom();
            Console.WriteLine("ADD PICKUp 1");
            data.AddPickup(pickup);

            Clients.All.addPickup(pickup);
        }

        public void StartTimer()
        {
            Console.WriteLine("started timer");
            _timer = new System.Timers.Timer(_timerInterval);
            _timer.Elapsed += new ElapsedEventHandler(UpdateTicks);
            _timer.Start();
        }


        public void DestroyProjectile(Projectile projectile)
        {
            Map map = Map.Instance;
            Clients.All.removeProjectile(projectile.Id);
            map.projectiles.Remove(projectile);
        }
    } 
}
