/**
 * @(#) SmallAsteroid.cs
 */

namespace Class_diagram
{
	public class SmallAsteroid : Enemy
	{
		public SmallAsteroid(int id, char rotation, Coordinates coordinates) : base()
		{
			Id = id;
			Health = 1;
			Damage = 1;
			Points = 100;
			Size = 20;
			X = coordinates.X;
			Y = coordinates.Y;
			Rotation = rotation;
		}
	}
	
}
