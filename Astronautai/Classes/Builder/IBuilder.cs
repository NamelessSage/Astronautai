using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Builder
{
    interface IBuilder
    {
        void SetType(string type);
        void SetId(int id);
        void SetValue(int value);
        void SetCoordinates(int x, int y);
        void SetImage(string img);
        void SetSize(int size);
        void Reset();
    }
}
