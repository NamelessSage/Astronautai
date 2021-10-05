/**
 * @(#) Player.cs
 */

namespace Class_diagram
{
	public class Player
	{
		public string Username { get; set; }
		public int Health { get; set; }
        public int Size { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Player()
        {
        }

        public Player(string username)
        {
            Username = username;
        }

        public Player(string username, int health, int size)
        {
            Username = username;
            Health = health;
            Size = size;
        }

        public void SetCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return Username;
        }
    }
}
