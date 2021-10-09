﻿using Class_diagram;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Astronautai;

namespace GameServer
{
    public class GameData
    {
        public static Map map;
        public EnemyCreator creator = new EnemyCreator();

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

        public void AddAsteroid(string size)
        {
            Map map = Map.Instance;
            map.enemies.Add(creator.factoryMethod(size));
        }

        public List<Enemy> GetEnemies()
        {
            Map map = Map.Instance;
            return map.enemies;
        }

        public void UpdateAsteroidCoords()
        {
            Map map = Map.Instance;
            foreach (Enemy p in map.enemies)
            {
                switch (p.Rotation)
                {
                    case 'W':
                        p.Y = p.Y - 1;
                        break;
                    case 'A':
                        p.X = p.X - 1;
                        break;
                    case 'S':
                        p.Y = p.Y + 1;
                        break;
                    case 'D':
                        p.X = p.X + 1;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
