using Class_diagram;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Astronautai;
using System.Linq;

namespace GameServer
{
    public class GameData
    {
        public static Map map;
        public int playerMoveSpeed = 10;

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
            {

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

        public bool CheckMapEdge(Coordinates coords)
        {
            //switch (type)
            //{
            //    case 'x':
            //        if (player.X < 25 || player.X > 750)
            //            return false;
            //        else
            //            return true;

            //    case 'y':
            //        if (player.Y < 25 || player.Y > 550)
            //            return false;
            //        return true;
            //    default:
            //        return false;
            //}
            // returns true if can move
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



    }
}
