using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes
{
    class Observer
    {
        public interface IObserver
        {
            void Update();
        }

        public interface ISubject
        {
            void Attach(IObserver observer);
            void Detach(IObserver observer);
            void Notify();
        }


        //for spawners
        public class Subject : ISubject
        {
            private List<IObserver> _observers = new List<IObserver>();
            public void Attach(IObserver observer)
            {
                this._observers.Add(observer);
            }

            public void Detach(IObserver observer)
            {
                this._observers.Remove(observer);
            }

            public void Notify()
            {
                foreach (var observer in _observers)
                {
                    observer.Update();
                }
            }
        }

    }
}
