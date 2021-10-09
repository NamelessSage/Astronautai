using Astronautai.Classes.Builder;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes
{
    class PickupBuilder : IBuilder 
    { 
        private Pickup pic;
        public PickupBuilder()
        {
            pic = new Pickup();
        }

        public Pickup GetBuildable()
        {
            try
            {
                return pic;
            }
            finally
            {
                reset();
            }
        }
   
        public void SetValue(int value)
        {
            pic.Value = value;
        }

        public void SetImg(string img)
        {
            pic.ImageDirectoryPath = img;
        }
        //public void SetType(string type)
        //{
        //    pic.Type = type;
        //}

        public void SetCordin(int x, int y)
        {
            pic.X = x;
            pic.Y = y;
        }
        public void SetSize(int size) 
        {
            pic.Size = size;
        }

        public void reset()
        {
            pic = new Pickup();
        }
    }
}
