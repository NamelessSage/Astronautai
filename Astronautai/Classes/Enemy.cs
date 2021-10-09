/**
 * @(#) Enemy.cs
 */

namespace Class_diagram
{
	public class Enemy : Coordinates
	{
		public int Id { get; set; }

		public int Health { get; set; }

		public int Points { get; set; }

		public int Size { get; set; }

		public int Damage { get; set; }

		public char Rotation { get; set; }

		public Enemy()
		{

		}

	}
	
}
