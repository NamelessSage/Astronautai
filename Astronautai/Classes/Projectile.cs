/**
 * @(#) Projectile.cs
 */

namespace Class_diagram
{
	public class Projectile : Coordinates
	{
		public int Id { get; set; }
		public Player Player { get; set; }
		public char Direction { get; set; }

        public Projectile() : base()
        {

        }
    }
}
