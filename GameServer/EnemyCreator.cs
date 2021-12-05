using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class EnemyCreator
    {
		static Random random = new Random();
		public Enemy CreateAsteroid(string asteroid, Coordinates coordinates, int id)
		{
			var moveDirections = new char[] { 'W', 'A', 'S', 'D', 'Q', 'E', 'Z', 'C' };

			switch (asteroid)
			{
				case "Big":
					BigAsteroid big = new BigAsteroid(id, moveDirections[random.Next(moveDirections.Length)], coordinates);
					return big;
				case "Average":
					AverageAsteroid average = new AverageAsteroid(id, moveDirections[random.Next(moveDirections.Length)], coordinates);
					return average;
				case "Small":
					SmallAsteroid small = new SmallAsteroid(id, moveDirections[random.Next(moveDirections.Length)], coordinates);
					return small;
				default:
					return null;
			}
		}
	}
}
