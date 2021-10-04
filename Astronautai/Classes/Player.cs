/**
 * @(#) Player.cs
 */

namespace Class_diagram
{
	public class Player : Coordinates
	{
		public int Id;
		public int Health;
		public int Size;

        public Player()
        {
        }

        public Player(int id)
        {
            Id = id;
        }

        public Player(int id, int health, int size)
        {
            Id = id;
            Health = health;
            Size = size;
        }
    }
	
}
