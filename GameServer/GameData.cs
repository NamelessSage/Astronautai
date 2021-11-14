using Class_diagram;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Astronautai;
using System.Linq;
using Astronautai.Classes.Factory;
using GameServer.Classes;

namespace GameServer
{
    public class GameData
    {
        public static Map map;

        public EnemySpawner enemySpawner = new EnemySpawner();
        public PickupSpawnerAdapter pickupSpawner = new PickupSpawnerAdapter();

        ObjectDestructor destructor = new ObjectDestructor();

        public GameData()
        {

        }

        public List<Player> GetPlayers()
        {
            Map map = Map.Instance;
            return map.players;
        }

        public void AddPlayer(Player player)
        {
            Map map = Map.Instance;
            map.players.Add(player);
        }

        public void UpdatePlayer(Player player)
        {
            Map map = Map.Instance;
            for (int i = 0; i < map.players.Count; i++)
            {
                if (map.players[i].Username == player.Username)
                {
                    map.players[i] = player;
                }
            }
        }

        public Map GetMap()
        {
            Map map = Map.Instance;
            return map;
        }

        public void AddProjectile(Projectile projectile)
        {
            Map map = Map.Instance;
            map.projectiles.Add(projectile);
        }

        public void AddPickup(Pickup pickup)
        {
            Map map = Map.Instance;
            map.pickups.Add(pickup);
        }

        public List<Projectile> GetProjectiles()
        {
            Map map = Map.Instance;
            return map.projectiles;
        }

        public void UpdateProjectileCoords()
        {
            Map map = Map.Instance;
            foreach (Projectile p in map.projectiles)
            {
                if(destructor.RemoveProjectile(p))
                {
                    break;
                }
                foreach(Enemy e in map.enemies)
                {
                    if (!Collides(p.GetCoordinates(), 5, e.GetCoordinates(), e.Size))
                    {
                        e.Health--;
                        if (e.Health <= 0)
                        {
                            destructor.RemoveEnemyNoCheck(e);
                        }
                        destructor.RemoveProjectileNoCheck(p);
                    }
                }
                switch (p.Direction)
                {
                    case 'W':
                        p.Y = p.Y - 10;
                        break;
                    case 'A':
                        p.X = p.X - 10;
                        break;
                    case 'S':
                        p.Y = p.Y + 10;
                        break;
                    case 'D':
                        p.X = p.X + 10;
                        break;
                    default:
                        break;
                }
            }
        }

        public bool CheckCollisionPlayers(Player p)
        {
            bool collides = true;
            Map map = Map.Instance;
            foreach (Player p2 in map.players)
            {
                if (p.Username != p2.Username)
                {
                    if (!Collides(new Coordinates(p.X, p.Y), p.Size, new Coordinates(p2.X, p2.Y), p2.Size))
                    {
                        collides = false;
                    }
                }
            }
            foreach (Obstacle obs in map.obstacles)
            {

                if (!Collides(new Coordinates(p.X, p.Y), p.Size, new Coordinates(obs.X, obs.Y), obs.Size))
                {
                    collides = false;
                }
            }
            return collides;
        }

        public bool CheckCollisionEnemy(Enemy enemy)
        {
            Map map = Map.Instance;
            foreach (Player player in map.players)
            {
                if (!Collides(new Coordinates(player.X, player.Y), player.Size, new Coordinates(enemy.X, enemy.Y), enemy.Size))
                {
                    player.Health -= enemy.Damage;
                    UpdatePlayer(player);
                    return false;
                }
            }
            return true;
        }
        public bool CheckNotCollisionPickup(Pickup pickup)
        {
            Map map = Map.Instance;
            foreach (Player player in map.players)
            {
                if (!Collides(new Coordinates(pickup.X, pickup.Y), pickup.Size, new Coordinates(player.X, player.Y), player.Size))
                {
                    Player playerUpdated = pickup.Action(player, pickup);

                    UpdateAsteroidCoords();
                    UpdatePlayer(playerUpdated);
                    return false;
                }
            }
            return true;
        }
        public bool CheckMapEdge(Coordinates coords)
        {
            if (coords.X < 25 || coords.X > 750)
                return false;

            if (coords.Y < 25 || coords.Y > 550)
                return false;

            return true;
        }
        private bool Collides(Coordinates o1, int size1, Coordinates o2, int size2)
        {
            if ((o1.X > o2.X && o1.X < o2.X + size2) && (o1.Y > o2.Y && o1.Y < o2.Y + size2) ||
                        (o1.X + size1 > o2.X && o1.X + size1 < o2.X + size2) && (o1.Y > o2.Y && o1.Y < o2.Y + size2) ||
                        (o1.X > o2.X && o1.X < o2.X + size2) && (o1.Y + size1 > o2.Y && o1.Y + size1 < o2.Y + size2) ||
                        (o1.X + size1 > o2.X && o1.X + size1 < o2.X + size2) &&
                        (o1.Y + size1 > o2.Y && o1.Y + size1 < o2.Y + size2))
            {
                return false;
            }
            return true;
        }
        public Player PlayerCanMove(Player player)
        {
            if (player.Rotation == 'W')
            {
                Player temp = new Player();
                temp.SetCoordinates(player.X, player.Y);
                temp.Size = player.Size;
                Coordinates coordinates = new Coordinates(temp.X, temp.Y - player.Speed);
                temp.Y = temp.Y - player.Speed;

                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    player.Y = temp.Y;
                    return player;
                }
            }
            else if (player.Rotation == 'A')
            {
                Player temp = new Player();
                temp.SetCoordinates(player.X, player.Y);
                temp.Size = player.Size;
                Coordinates coordinates = new Coordinates(temp.X - player.Speed, temp.Y);
                temp.X = temp.X - player.Speed;
                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    player.X = temp.X;
                    return player;
                }
            }
            else if (player.Rotation == 'S')
            {
                Player temp = new Player();
                temp.SetCoordinates(player.X, player.Y);
                temp.Size = player.Size;
                Coordinates coordinates = new Coordinates(temp.X, temp.Y + player.Speed);
                temp.Y = temp.Y + player.Speed;
                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    player.Y = temp.Y;
                    return player;
                }
            }
            else if (player.Rotation == 'D')
            {
                Player temp = new Player();
                temp.SetCoordinates(player.X, player.Y);
                temp.Size = player.Size;
                Coordinates coordinates = new Coordinates(temp.X + player.Speed, temp.Y);
                temp.X = temp.X + player.Speed;
                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    player.X = temp.X;
                    return player;
                }
            }
            return player;
        }
        public void AddAsteroid()
        {
            Map map = Map.Instance;
            map.enemies.Add(enemySpawner.Spawn(map));
        }

        public List<Enemy> GetEnemies()
        {
            Map map = Map.Instance;
            return map.enemies;
        }

        public List<Pickup> GetPickups()
        {
            Map map = Map.Instance;
            return map.pickups;
        }

        public void UpdateAsteroidCoords()
        {
            Map map = Map.Instance;
            GameHub hub = new GameHub().GetHub();


            foreach (Enemy enemy in map.enemies)
            {
                if (destructor.RemoveEnemy(enemy))
                {
                    break;
                }

                if (CheckCollisionEnemy(enemy))
                {
                    int averagePlayerHealth = GetAveragePlayerHealth();
                    if (averagePlayerHealth == 3)
                    {
                        enemy.SetMoveSlow();
                    }
                    else if(averagePlayerHealth < 3 && averagePlayerHealth > 1)
                    {
                        enemy.SetMoveAverage();
                    }
                    else
                    {
                        enemy.SetMoveFast();
                    }
                    enemy.Move();
                }
                else
                {
                    destructor.RemoveEnemyNoCheck(enemy);
                    break;
                }
            }
        }

        public int UpdatePickups()
        {
            Map map = Map.Instance;
            foreach (Pickup pickup in map.pickups)
            {
                if (CheckNotCollisionPickup(pickup))
                {
                    
                }
                else
                {
                    int id = pickup.Id;
                    map.pickups.Remove(pickup);
                    return id;
                }
            }
            return -1;
        }

        public int GetAveragePlayerHealth()
        {
            Map map = Map.Instance;
            int count = 0;
            foreach (Player player in map.players)
            {
                if(player.Health <= 0) 
                {
                    count += 0;
                }
                else
                {
                    count += player.Health;
                }
                
            }

            return count / map.players.Count;
        }

        public void GenerateObstacles()
        {
            Map map = Map.Instance;
            Random ran = new Random();
            Obstacle obs = new Obstacle(0,0,0, 25);
            
            for (int i = 0; i < 6; i++)
            {
                Obstacle obs2 = (Obstacle)obs.Clone();
                obs2.Id = i;
                obs2.X = ran.Next(50, 500);
                obs2.Y = ran.Next(50, 500);
                map.obstacles.Add(obs2);
            }

        }
        public List<Obstacle> GetObstacles()
        {
            Map map = Map.Instance;
            return map.obstacles;
        }
    }
}
