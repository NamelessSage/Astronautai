using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.States
{
    class Slowed:State
    {
        public Slowed(State state)
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
            if (moveSpeed == 0)
            {
                Console.WriteLine("switching to stunned state");
                return new Stunned(this);
            }
            else if (moveSpeed < 50 && moveSpeed > 10)
            {
                Console.WriteLine("switching to normal state");
                return new Normal(this);
            }
            else if (moveSpeed == 60)
            {
                Console.WriteLine("switching to damaged state");
                return new Damaged(this);
            }

            return this;
        }
    }
}
