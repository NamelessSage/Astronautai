

using System.Collections.Generic;
/**
* @(#) Map.cs
*/
namespace Class_diagram
{
	public class Map
	{
		private static readonly Map instance = new Map();

		public List<Enemy> enemies = new List<Enemy>();
		public List<Obstacle> obstacles = new List<Obstacle>();
		public List<Projectile> projectiles = new List<Projectile>();
		public int Score = 0;
		public int Difficulty = 0;
		public List<Pickup> pickups = new List<Pickup>();
		public List<Player> players = new List<Player>();
		public int projectileCounter = 0;
		public int enemyCounter = 0;

		private Map() 
		{ 

		}

		public static Map Instance
		{
			get { return instance; }
		}
	}
}
