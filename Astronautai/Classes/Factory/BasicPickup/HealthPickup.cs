

using Astronautai.Classes.Proxy;
/**
* @(#) Health.cs
*/
namespace Class_diagram
{
	public class HealthPickup : Pickup
	{
		public override Player Action(Player player, Pickup pickup)
		{
			ParameterProxy proxy = new ParameterProxy(player);
			proxy.Heal(pickup.Value);
			//player.AddHealth(pickup.Value);
			player.Affect("Stunned");
			return player;
		}
	}
	
}
