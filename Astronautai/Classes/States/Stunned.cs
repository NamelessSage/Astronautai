using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.States
{
    public class Stunned : State
    {
        public Stunned(State state)
        {
            this.moveSpeed = state.MoveSpeed;
        }

        public override State ChangeSpeed(int curSpeed)
        {
            this.moveSpeed = curSpeed;
            return StateChangeCheck();
        }

        public State StateChangeCheck()
        {
            if (moveSpeed > 0)
            {
                Console.WriteLine("switching to normal state");
                return new Normal(this);
            }

            return this;
        }
    }
}
