/**
 * @(#) BigAsteroid.cs
 */

namespace Class_diagram
{
	public class BigAsteroid : Enemy
	{
		public BigAsteroid(int id, char rotation) : base()
        {
			Id = id;
			Health = 3;
			Damage = 2;
			Points = 300;
			Size = 50;
			X = 500;
			Y = 500;
			Rotation = rotation;
        }
	}
	
}
