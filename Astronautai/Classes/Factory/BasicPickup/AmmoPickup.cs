/**
 * @(#) Ammo.cs
 */

namespace Class_diagram
{
	public class AmmoPickup : Pickup
	{
        public override Player Action(Player player, Pickup pickup)
        {
            player.AddAmmo(pickup.Value);

            return player;
        }
    }
	
}
