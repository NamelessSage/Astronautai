using System.Collections.Generic;
/**
* @(#) Pickups.cs
*/
namespace Class_diagram
{
	public class Pickup
	{
		public int Id { get; set; }
		public int Value { get; set; }
		public string Type { get; set; }
		public string ImagePath { get; set; }
		public int Size { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public List<Pickup> Effects { get; set; }

		public Pickup()
		{

		}

		public virtual Player Action(Player player, Pickup pickup) { return new Player(); }
    }
}
