using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Astronautai
{
    public abstract class Hazard
    {
        static Random random = new Random(444);
        const int timerIntervalMinimum = 1000;
        const int timerIntervalMaximum = 2000;

        protected IBridge bridge1;
        protected IBridge bridge2;

        public int id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public char MoveDirection { get; set; }
        public bool MoveSwitch { get; set; }

        public abstract string Effect();
        public abstract void Move1();
        public abstract void Move2();

        private Timer _timer;
        private int _timerInterval = 1000;

        protected Hazard(IBridge bridge1, IBridge bridge2)
        {
            _timerInterval = random.Next(timerIntervalMinimum, timerIntervalMaximum);
            _timer = new Timer(_timerInterval);
            _timer.Elapsed += new ElapsedEventHandler(MovementLock);

            this.bridge1 = bridge1;
            this.bridge2 = bridge2;
        }

        public void SetMoveDirection(char direction)
        {
            MoveDirection = direction;
        }

        public void MoveTemplate()
        {
            if(!_timer.Enabled)
                _timer.Start();
            if (MoveSwitch)
                Move1();
            else
                Move2();
        }

        public void MovementLock(object source, ElapsedEventArgs e)
        {
            MoveSwitch = !MoveSwitch;
            _timer.Stop();
        }
    }
}
