/**
 * @(#) AverageAsteroid.cs
 */

namespace Class_diagram
{
	public class AverageAsteroid : Enemy
	{
		public AverageAsteroid(int id, char rotation, Coordinates coordinates) : base()
		{
			Id = id;
			Health = 2;
			Damage = 1;
			Points = 200;
			Size = 35;
			X = coordinates.X;
			Y = coordinates.Y;
			Rotation = rotation;
		}
	}
	
}
