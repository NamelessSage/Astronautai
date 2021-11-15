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
        Subject subject = new Subject();

        bool started = false;
        private System.Timers.Timer _timer;
        private int _timerInterval = 50;
        
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
        public void GetObstacles()
        {
            List<Obstacle> obst = data.GetObstacles();
            Clients.All.getObstacles(obst);
        }
        public void GetHazards()
        {
            List<Hazard> hz = data.GetHazards();
            List<Fire> fire = new List<Fire>();
            List<Water> water = new List<Water>();
            foreach (Hazard h in hz)
            {
                if(h.Effect().Contains("Damage"))
                    fire.Add((Fire)h);
                if (h.Effect().Contains("Slowdown"))
                    water.Add((Water)h);
            }

            Clients.All.getHazards(fire, water);
        }

        public void GetPlayersCaller()
        {
            List<Player> players = data.GetPlayers();
            Clients.Caller.getPlayersCaller(players);
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
            Clients.All.movePlayer(player);
        }

        public void UndoMovePlayer(Player player)
        {
            data.UpdatePlayer(player);
            Clients.All.movePlayer(player);
        }

        public void AddProjectile(Projectile projectile, Player player)
        {
            //Console.WriteLine("Adding projectile with id = " + projectile.Id);
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
            Clients.All.updatePlayerData(data.GetPlayers());
            int deletePickupId = data.UpdatePickups();
            Clients.All.updateTicksPickups(data.GetPickups(), deletePickupId);

            if(data.GetAveragePlayerHealth() <= 0)
            {
                GameOver();
            }
        }

        public void GetProjectiles()
        {
            Map map = data.GetMap();
            Clients.Caller.getProjectiles(map.projectileCounter);
            map.projectileCounter++;
        }

        public void StartTimer()
        {
            _timer = new System.Timers.Timer(_timerInterval);
            _timer.Elapsed += new ElapsedEventHandler(UpdateTicks);
            _timer.Start();
        }

        public void GenerateObstacles()
        {
            data.GenerateObstacles();
        }

        public void GameOver()
        {
            Clients.All.gameOver(true);
            _timer.Stop();
        }
    } 
}
