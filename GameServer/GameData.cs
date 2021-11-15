﻿using Class_diagram;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Astronautai;
using System.Linq;
using Astronautai.Classes.Factory;
using GameServer.Classes;
using Astronautai.Classes.Strategy;

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
                    if (!Collides(p.X, p.Y, 5, e.X, e.Y, e.Size))
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
                    if (!Collides(p.X, p.Y, p.Size, p2.X, p2.Y, p2.Size))
                    {
                        collides = false;
                    }
                }
            }
            foreach (Obstacle obs in map.obstacles)
            {

                if (!Collides(p.X, p.Y, p.Size, obs.coordinates.X, obs.coordinates.Y, obs.Size))
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
                if (!Collides(player.X, player.Y, player.Size, enemy.X, enemy.Y, enemy.Size))
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
                if (!Collides(pickup.X, pickup.Y, pickup.Size, player.X, player.Y, player.Size))
                {
                    Player playerUpdated = pickup.Action(player, pickup);

                    UpdateAsteroidCoords();
                    UpdatePlayer(playerUpdated);
                    return false;
                }
            }
            return true;
        }


        public bool CheckMapEdge(int x, int y)
        {
            if (x < 25 || x > 750)
                return false;

            if (y < 25 || y > 550)
                return false;

            return true;
        }

        private bool Collides(int x1, int y1, int size1, int x2, int y2, int size2)
        {
            if ((x1 > x2 && x1 < x2 + size2) && (y1 > y2 && y1 < y2 + size2) ||
                        (x1 + size1 > x2 && x1 + size1 < x2 + size2) && (y1 > y2 && y1 < y2 + size2) ||
                        (x1 > x2 && x1 < x2 + size2) && (y1 + size1 > y2 && y1 + size1 < y2 + size2) ||
                        (x1 + size1 > x2 && x1 + size1 < x2 + size2) &&
                        (y1 + size1 > y2 && y1 + size1 < y2 + size2))
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
                temp.Y = temp.Y - player.Speed;

                if (CheckMapEdge(temp.X, temp.Y - player.Speed) && CheckCollisionPlayers(temp))
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
                temp.X = temp.X - player.Speed;

                if (CheckMapEdge(temp.X - player.Speed, temp.Y) && CheckCollisionPlayers(temp))
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
                temp.Y = temp.Y + player.Speed;

                if (CheckMapEdge(temp.X, temp.Y + player.Speed) && CheckCollisionPlayers(temp))
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
                temp.X = temp.X + player.Speed;

                if (CheckMapEdge(temp.X + player.Speed, temp.Y) && CheckCollisionPlayers(temp))
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
                        enemy.SetMoveStrategy(new SlowMoveStrategy());
                    }
                    else if(averagePlayerHealth < 3 && averagePlayerHealth > 1)
                    {
                        enemy.SetMoveStrategy(new MediumMoveStrategy());
                    }
                    else if(averagePlayerHealth == 1)
                    {
                        enemy.SetMoveStrategy(new FastMoveStrategy());
                    }
                    else
                    {
                        enemy.SetMoveStrategy(new NoneMoveStrategy());
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
                if (!CheckNotCollisionPickup(pickup))
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
            
            Obstacle obs = new Obstacle(0, new Coordinates(100, 100), 25);

            for (int i = 0; i < 6; i++)
            {
                Obstacle obs2 = (Obstacle)obs.CloneDeep();
                obs2.Id = i;
                obs2.coordinates.X = ran.Next(50, 500);
                obs2.coordinates.Y = ran.Next(50, 500);
                map.obstacles.Add(obs2);
            }
            GenerateHazzards();
        }
        public void GenerateHazzards()
        {
            Map map = Map.Instance;
            Random ran = new Random();

            Hazard fire = new Fire(new HazardDamage(), new HazardMovement());
            Hazard water = new Water(new HazardDamage(), new HazardMovement());
            Hazard water1 = new Water(new HazardDamage(), new HazardMovement());
            Hazard water2 = new Water(new HazardDamage(), new HazardMovement());
            Hazard fire1 = new Fire(new HazardDamage(), new HazardMovement());
            Hazard fire2 = new Fire(new HazardDamage(), new HazardMovement());
            Hazard fire3 = new Fire(new HazardDamage(), new HazardMovement());

            fire.X = 200;
            fire.Y = 200;
            fire1.X = 250;
            fire1.Y = 150;
            fire2.X = 400;
            fire2.Y = 300;
            fire3.X = 100;
            fire3.Y = 500;
            water.X = 400;
            water.Y = 100;
            water1.X = 400;
            water1.Y = 600;
            water2.X = 80;
            water2.Y = 100;
            map.hazards.Add(fire);
            map.hazards.Add(fire1);
            map.hazards.Add(fire2);
            map.hazards.Add(fire3);
            map.hazards.Add(water1);
            map.hazards.Add(water2);
            map.hazards.Add(water);

            Console.WriteLine(fire.Effect());
            Console.WriteLine(fire1.Effect());
            Console.WriteLine(fire2.Effect());
            Console.WriteLine(fire3.Effect());
            Console.WriteLine(water.Effect());
            Console.WriteLine(water1.Effect());
            Console.WriteLine(water2.Effect());

        }
        public List<Obstacle> GetObstacles()
        {
            Map map = Map.Instance;
            return map.obstacles;
        }
        public List<Hazard> GetHazards()
        {
            Map map = Map.Instance;
            return map.hazards;
        }
    }
}
