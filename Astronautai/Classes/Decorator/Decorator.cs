/**
 * @(#) Decorator.cs
 */

namespace Class_diagram
{
	abstract class Decorator : PlayerImage
    {
        protected PlayerImage player;


        public Decorator(PlayerImage p)
        {
            this.player = p;
        }
    }
	
}
