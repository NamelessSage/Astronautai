using Astronautai.Classes.Builder;
using Astronautai.Classes.Factory;
using Astronautai.Classes.Factory.BasicPickup;
using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes
{
    public abstract class PickupBuilder : IBuilder 
    { 
        protected Pickup pickup;

        public PickupBuilder()
        {
            pickup = new Pickup();
        }

        public abstract Pickup GetBuildable();
       

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

        public void SetType(string type)
        {
            if (type == "AmmoPickup")
            {
                pickup = new AmmoPickup();
            }
            if (type == "HealthPickup")
            {
                pickup = new HealthPickup();
            }
            if (type == "SpeedPickup")
            {
                pickup = new SpeedPickup();
            }
            if (type == "MultiPickup")
            {
                pickup = new MultiPickup();
            }
            if (type == "TeleportPickup")
            {
                pickup = new TeleportPickup();
            }
        }

        public abstract void Reset();

        public void SetEffects(List<Pickup> effects)
        {
            pickup.Effects = effects;
        }
    }
}
