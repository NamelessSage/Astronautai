﻿using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class EnemyCreator
    {
		static System.Random Creations = new System.Random();
		static System.Random random = new System.Random();
		public Enemy CreateAsteroid(string asteroid, Coordinates coordinates, int id)
		{
			var chars = new char[] { 'W', 'A', 'S', 'D', 'Q', 'E', 'Z', 'C' };

			switch (asteroid)
			{
				case "Big":
					Console.WriteLine("Spawning Big asteroid x = {0} y = {1} with id = {2}", coordinates.X, coordinates.Y, id);
					BigAsteroid big = new BigAsteroid(id, chars[random.Next(chars.Length)], coordinates);
					return big;
				case "Average":
					Console.WriteLine("Spawning Average asteroid x = {0} y = {1} with id = {2}", coordinates.X, coordinates.Y, id);
					AverageAsteroid average = new AverageAsteroid(id, chars[random.Next(chars.Length)], coordinates);
					return average;
				case "Small":
					Console.WriteLine("Spawning Small asteroid x = {0} y = {1} with id = {2}", coordinates.X, coordinates.Y, id);
					SmallAsteroid small = new SmallAsteroid(id, chars[random.Next(chars.Length)], coordinates);
					return small;
				default:
					return null;
			}
		}
	}
}