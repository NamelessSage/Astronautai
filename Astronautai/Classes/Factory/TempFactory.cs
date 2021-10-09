using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Factory
{
    class TempFactory
    {
        public TempFactory()
        {
        }

        public Pickup GetPickups(string type,int x,int y,int value)
        {
            PickupBuilder builder = new PickupBuilder();
            switch (type)
            {
                case "Ammo":
                    builder.SetCordin(x, y);
                    builder.SetImg(@"..//..//Objects//healthPickup.png");
                    builder.SetSize(20);
                    builder.SetValue(value);

                    return builder.GetBuildable();
                case "Health":
                    builder.SetCordin(x, y);
                    builder.SetImg(@"..//..//Objects//healthPickup.png");
                    builder.SetSize(20);
                    builder.SetValue(value);

                    return builder.GetBuildable();
                case "Speed":
                    builder.SetCordin(x, y);
                    builder.SetImg(@"..//..//Objects//healthPickup.png");
                    builder.SetSize(20);
                    builder.SetValue(value);

                    return builder.GetBuildable();
                default:
                    throw new NotSupportedException("type not found");
            }

           
        }

    }
}
