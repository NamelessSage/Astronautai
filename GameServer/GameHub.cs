using System;
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
using static Astronautai.Classes.Observer;

namespace GameServer
{
    [HubName("serveris")]
    public class GameHub : Hub
    {
        GameData data = new GameData();
        bool started = false;
        private System.Timers.Timer _timer;
        private int _timerInterval = 50;
        Subject subject = new Subject();

        
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
                EnemySpawner enemySpawner = new EnemySpawner();
                PickupSpawner pickupSpawner = new PickupSpawner();
                subject.Attach(enemySpawner);
                subject.Attach(pickupSpawner);

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
            subject.Notify();

            data.UpdateProjectileCoords();

            data.UpdateAsteroidCoords();
            Clients.All.updateTicks(data.GetProjectiles());
            Clients.All.updateTicksAsteroids(data.GetEnemies());

            int deletePickupId = data.UpdatePickups();
            Clients.All.updateTicksPickups(data.GetPickups(), deletePickupId);

            Clients.All.updatePlayerData(data.GetPlayers());
        }

        public void GetProjectiles()
        {
            Map map = data.GetMap();
            Clients.Caller.getProjectiles(map.projectileCounter);
            map.projectileCounter++;
        }

        public void StartTimer()
        {
            Console.WriteLine("started timer");
            _timer = new System.Timers.Timer(_timerInterval);
            _timer.Elapsed += new ElapsedEventHandler(UpdateTicks);
            _timer.Start();
        }
    } 
}
