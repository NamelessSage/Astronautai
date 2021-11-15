/**
 * @(#) Projectile.cs
 */

namespace Class_diagram
{
	public class Projectile
	{
		public int Id { get; set; }
		public Player Player { get; set; }
		public char Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Projectile(Player player) : base()
        {
            Player = player;
            Direction = player.Rotation;
        }
        public Coordinates GetCoordinates()
        {
            return new Coordinates(X, Y);
        }
    }
}
