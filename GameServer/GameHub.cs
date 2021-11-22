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
using Astronautai.Classes.States;
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
        private int _timerInterval = 100;
        
        public GameHub()
        {

        }

        public GameHub GetHub()
        {
            return this;
        }

        public void AddPlayerOnJoin(Player player)
        {
            player.SetState(new Normal(player.Speed));
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
            player = SetState(player);
            player = data.PlayerCanMove(player);
            data.UpdatePlayer(player);
            Clients.All.movePlayer(player);
        }

        public void UndoMovePlayer(Player player)
        {
            player = SetState(player);
            data.UpdatePlayer(player);
            Clients.All.movePlayer(player);
        }

        public void AddProjectile(Projectile projectile, Player player)
        {
            player = SetState(player);
            data.UpdatePlayer(player);
            data.AddProjectile(projectile);
        }

        public void UpdateTicks(object source, ElapsedEventArgs e)
        {
            List<Hazard> hazards = data.GetHazards();
            List<Fire> fire = new List<Fire>();
            List<Water> water = new List<Water>();

            Console.WriteLine(hazards.Count);
            
            foreach (Hazard hazard in hazards)
            {
                if (hazard.Effect().Contains("Damage"))
                    fire.Add((Fire)hazard);
                if (hazard.Effect().Contains("Slowdown"))
                    water.Add((Water)hazard);
            }

            subject.Notify();
            data.UpdateProjectileCoords();
            data.UpdateAsteroidCoords();
            Clients.All.updateTicks(data.GetProjectiles());     //Update projectiles
            Clients.All.updateTicksAsteroids(data.GetEnemies());//Update enemies
            Clients.All.updatePlayerData(data.GetPlayers());    //Update players
            int deletePickupId = data.UpdatePickups();
            
            Clients.All.updateTicksPickups(data.GetPickups(), deletePickupId); //update pickups
            UpdateEffects();

            Clients.All.updateTicksHazards(fire, water, data.UpdateHazards()); //

            if (data.GetAveragePlayerHealth() <= 0)
            {
                GameOver();
            }
        }

        public void UpdateEffects()
        {
            List<Player> players = data.GetPlayers();
            foreach (Player p in players)
            {
                p.DecreaseDurration();
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

        public Player SetState(Player player)
        {
            Player serverPlayer = data.GetPlayers().Find(p => p.Username == player.Username);
            player.SetState(serverPlayer.GetState());
            player.Effect = serverPlayer.Effect;
            player.TickDurration = serverPlayer.TickDurration;
            return player;
        }

    } 
}
