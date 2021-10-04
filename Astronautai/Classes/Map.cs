/**
 * @(#) Map.cs
 */

namespace Class_diagram
{
	public class Map
	{
		private static Map instance = null;
		private static object threadLock = new object();

		public Enemy enemy;
		public Obstacles obstacles;
		public int Score;
		public int Diffiulty;
		public Pickups pickups;

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
    }
	
}
