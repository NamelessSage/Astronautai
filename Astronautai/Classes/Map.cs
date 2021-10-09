

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
		public List<Obstacles> obstacles = new List<Obstacles>();
		public List<Projectile> projectiles = new List<Projectile>();
		public int Score = 0;
		public int Diffiulty = 0;
		public List<Pickup> pickups = new List<Pickup>();
		public List<Player> players = new List<Player>();
		private Map() { }
		public static Map Instance
		{
			get { return instance; }
		}
	}
}
