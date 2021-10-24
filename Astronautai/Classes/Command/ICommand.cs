using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes
{
    public interface ICommand
    {
        void ExecuteAction();
        void Undo();
    }
}
