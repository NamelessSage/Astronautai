/**
 * @(#) Pickups.cs
 */

namespace Class_diagram
{
	public class Pickup : Coordinates
	{
		public int Id { get; set; }
		public int Value { get; set; }
		public string ImagePath { get; set; }
		public int Size { get; set; }

		public Pickup()
		{

		}
    }
}
