using Class_diagram;
using System;

namespace Astronautai.Classes.States
{
    public class Normal : State
    {

        public Normal(State state) : 
            this(state.MoveSpeed)
        {
        }

        public Normal(int moveSpeed)
        {
            this.moveSpeed = moveSpeed;
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
            else if (moveSpeed == 10)
            {
                Console.WriteLine("switching to slowed state");
                return new Slowed(this);
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
