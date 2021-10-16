/**
 * @(#) BigAsteroid.cs
 */

namespace Class_diagram
{
	public class BigAsteroid : Enemy
	{
		public BigAsteroid(int id, char rotation, Coordinates coordinates) : base()
        {
			Id = id;
			Health = 3;
			Damage = 2;
			Points = 300;
			Size = 50;
			X = coordinates.X;
			Y = coordinates.Y;
			Rotation = rotation;
        }
	}
}
