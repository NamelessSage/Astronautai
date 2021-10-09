using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Builder
{
    interface IBuilder
    {
        void SetValue(int value);
        void SetCordin(int x, int y);
        //void SetType(string type);
        void SetImg(string img);
        void SetSize(int size);
        
    }
}
