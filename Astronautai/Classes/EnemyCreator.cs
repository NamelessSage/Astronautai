/**
 * @(#) EnemyCreator.cs
 */

namespace Class_diagram
{
	public class EnemyCreator : Creator
	{
		static System.Random Creations = new System.Random();
		static System.Random random = new System.Random();

		public Class_diagram.Enemy CreateAsteroid(string asteroid)
		{
			var chars = new char[] {'W','A','S','D','Q','E','Z','C'};

			switch (asteroid)
            {
				case "Big":
					BigAsteroid big = new BigAsteroid(Creations.Next(int.MaxValue), chars[random.Next(chars.Length)]);
					return big;
				case "Average":
					 AverageAsteroid average = new AverageAsteroid(Creations.Next(int.MaxValue), chars[random.Next(chars.Length)]);
					return average;
				case "Small":
					SmallAsteroid small = new SmallAsteroid(Creations.Next(int.MaxValue), chars[random.Next(chars.Length)]);
					return small;
				default:
					return null;
			}
		}
		
	}
	
}
