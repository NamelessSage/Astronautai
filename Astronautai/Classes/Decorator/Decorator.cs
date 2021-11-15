/**
 * @(#) Decorator.cs
 */

namespace Class_diagram
{
	abstract class Decorator : PlayerImage
    {
        protected PlayerImage player;
        protected string image = "";

        public Decorator(PlayerImage p)
        {
            this.player = p;
        }
        public string GetImage()
        {
            return string.Format("{0},{1}", player.GetImage(), image);
        }
    }
	
}
