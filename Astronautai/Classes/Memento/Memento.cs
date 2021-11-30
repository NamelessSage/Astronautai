using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Memento
{
    public class Memento
    {
        string username;
        int X;
        int Y;

        public Memento(string name, int X, int Y)
        {
            this.username = name;
            this.X = X;
            this.Y = Y;
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public int Xcord
        {
            get { return X; }
            set { X = value; }
        }
        public int Ycord
        {
            get { return Y; }
            set { Y = value; }
        }
    }
}
