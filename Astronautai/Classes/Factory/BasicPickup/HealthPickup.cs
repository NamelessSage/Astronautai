/**
 * @(#) Health.cs
 */

namespace Class_diagram
{
	public class HealthPickup : Pickup
	{
		public override Player Action(Player player, Pickup pickup)
		{
			player.AddHealth(pickup.Value);
			player.Affect("Stunned");
			return player;
		}
	}
	
}
