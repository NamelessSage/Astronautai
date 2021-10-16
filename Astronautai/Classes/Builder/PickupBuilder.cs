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
        private Pickup pickup;

        public PickupBuilder()
        {
            pickup = new Pickup();
        }

        public Pickup GetBuildable()
        {
            try
            {
                return pickup;
            }
            finally
            {
                Reset();
            }
        }

        public void SetId(int id)
        {
            pickup.Id = id;
        }

        public void SetValue(int value)
        {
            pickup.Value = value;
        }

        public void SetImage(string imagePath)
        {
            pickup.ImagePath = imagePath;
        }

        public void SetCoordinates(int x, int y)
        {
            pickup.X = x;
            pickup.Y = y;
        }
        public void SetSize(int size) 
        {
            pickup.Size = size;
        }

        public void Reset()
        {
            pickup = new Pickup();
        }
    }
}
