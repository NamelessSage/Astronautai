

using System.Collections.Generic;
/**
* @(#) Map.cs
*/
namespace Class_diagram
{
	public class Map
	{
		private static Map instance = null;
		private static object threadLock = new object();
		public List<Enemy> enemies;
		public List<Obstacles> obstacles;
		public int Score;
		public int Diffiulty;
		public List<Pickups> pickups;
		public List<Player> players;

        public static Map getInstance()
        {
            lock (threadLock)
            {
				if(instance == null)
                {
					instance = new Map();
                }
            }

			return instance;
        }

		public void AddEnemy(Enemy enemy)
        {
			enemies.Add(enemy);
		}
		public void RemoveEnemy(Enemy enemy)
		{
			enemies.Remove(enemy);
		}

		public void AddObstacle(Obstacles obstacle)
		{
			obstacles.Add(obstacle);
		}
		public void RemoveObstacles(Obstacles obstacle)
		{
			obstacles.Remove(obstacle);
		}

		public void AddPickups(Pickups pickup)
		{
			pickups.Add(pickup);
		}
		public void RemovePickups(Pickups pickup)
		{
			pickups.Remove(pickup);
		}

		public void AddPlayer(Player player)
		{
			players.Add(player);
		}
		public void RemovePlayer(Player player)
		{
			players.Remove(player);
		}

	}
}
