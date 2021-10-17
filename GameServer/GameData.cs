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
        public int playerMoveSpeed = 10;
        public PickupFactory pickupFactory = new PickupFactory();
        public OnePickupFactory onepickupFactory = new OnePickupFactory();
        public MaxPickupFactory maxpickupFactory = new MaxPickupFactory();
        public EnemySpawner spawner = new EnemySpawner();
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
            Map map = Map.Instance;
            foreach (Player p2 in map.players)
            {
                if (p.Username != p2.Username)
                {
                    if (!Collides(new Coordinates(p.X,p.Y), p.Size, new Coordinates(p2.X, p2.Y), p2.Size))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CheckCollisionEnemy(Enemy enemy)
        {
            Map map = Map.Instance;
            foreach (Player player in map.players)
            {
                if (!Collides(new Coordinates(enemy.X, enemy.Y), enemy.Size, new Coordinates(player.X, player.Y), player.Size))
                {
                    player.Health -= enemy.Damage;
                    UpdatePlayer(player);
                    return false;
                }
            }
            return true;
        }

        public bool CheckCollisionPickup(Pickup pickup)
        {
            Map map = Map.Instance;
            foreach (Player player in map.players)
            {
                if (!Collides(new Coordinates(pickup.X, pickup.Y), pickup.Size, new Coordinates(player.X, player.Y), player.Size))
                {
                    if(player.Health < 3) //MAX HEALTH 3
                    {
                        player.Health += pickup.Value;
                    }
                    UpdateAsteroidCoords();
                    UpdatePlayer(player);
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

        public Player PlayerCanMove(Player p)
        {
            if (p.Rotation == 'W')
            {
                Player temp = new Player();
                temp.SetCoordinates(p.X, p.Y);
                temp.Size = p.Size;
                Coordinates coordinates = new Coordinates(temp.X, temp.Y - playerMoveSpeed);
                temp.Y = temp.Y - playerMoveSpeed;

                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    p.Y = temp.Y;
                    return p;
                }
            }
            else if (p.Rotation == 'A')
            {
                Player temp = new Player();
                temp.SetCoordinates(p.X, p.Y);
                temp.Size = p.Size;
                Coordinates coordinates = new Coordinates(temp.X- playerMoveSpeed, temp.Y);
                temp.X = temp.X - playerMoveSpeed;
                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    p.X = temp.X;
                    return p;
                }
            }
            else if (p.Rotation == 'S')
            {
                Player temp = new Player();
                temp.SetCoordinates(p.X, p.Y);
                temp.Size = p.Size;
                Coordinates coordinates = new Coordinates(temp.X, temp.Y + playerMoveSpeed);
                temp.Y = temp.Y + playerMoveSpeed;
                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    p.Y = temp.Y;
                    return p;
                }
            }
            else if (p.Rotation == 'D')
            {
                Player temp = new Player();
                temp.SetCoordinates(p.X, p.Y);
                temp.Size = p.Size;
                Coordinates coordinates = new Coordinates(temp.X+ playerMoveSpeed, temp.Y);
                temp.X = temp.X + playerMoveSpeed;
                if (CheckMapEdge(coordinates) && CheckCollisionPlayers(temp))
                {
                    p.X = temp.X;
                    return p;
                }
            }
            return p;
        }

        public void AddAsteroid()
        {
            Map map = Map.Instance;
            map.enemies.Add(spawner.CreateAsteroid(map));
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

        public int UpdateAsteroidCoords()
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
                        enemy.MoveSlow();
                    }
                    else if(averagePlayerHealth < 3 && averagePlayerHealth > 1)
                    {
                        enemy.MoveAverage();
                    }
                    else
                    {
                        enemy.MoveFast();
                    }
                }
                else
                {
                    int id = enemy.Id;
                    map.enemies.Remove(enemy);
                    return id;
                }
            }
            return -1;
        }

        public int UpdatePickups()
        {
            Map map = Map.Instance;
            foreach (Pickup pickup in map.pickups)
            {
                if (CheckCollisionPickup(pickup))
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
                count += player.Health; 
            }

            return count / map.players.Count;
        }
    }
}
