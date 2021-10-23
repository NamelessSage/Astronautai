/**
 * @(#) Decorator.cs
 */

namespace Class_diagram
{
	public abstract class Decorator : PoweredPlayer
	{
        protected PoweredPlayer player;


        public Decorator(PoweredPlayer p)
        {
            this.player = p;
        }
    }
	
}
