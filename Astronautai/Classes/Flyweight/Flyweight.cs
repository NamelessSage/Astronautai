using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai
{
    public class Flyweight
    {
        private Image _image;

        public Flyweight(Image img)
        {
            this._image = img;
        }
        public Image GetImage()
        {
            return _image;
        }

    }
}
