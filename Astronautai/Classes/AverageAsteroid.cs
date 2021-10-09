/**
 * @(#) AverageAsteroid.cs
 */

namespace Class_diagram
{
	public class AverageAsteroid : Enemy
	{
		public AverageAsteroid(int id, char rotation) : base()
		{
			Id = id;
			Health = 2;
			Damage = 1;
			Points = 200;
			Size = 35;
			X = 700;
			Y = 100;
			Rotation = rotation;
		}
	}
	
}
