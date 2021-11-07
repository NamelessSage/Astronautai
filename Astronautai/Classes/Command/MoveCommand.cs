using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_diagram;

namespace Astronautai.Classes
{
    class MoveCommand : ICommand
    {
        private readonly Move _move;
        private readonly char _direction;
        public bool IsCommandExecuted { get; private set; }

        public MoveCommand(Move move, char dir)
        {
            _move = move;
            _direction = dir;
        }
        public void ExecuteAction()
        {
            if (_direction == 'W')
            {
                _move.MoveW();
                IsCommandExecuted = true;
            }
            else if(_direction == 'S')
            {
                _move.MoveS();
                IsCommandExecuted = true;
            }
            else if (_direction == 'D')
            {
                _move.MoveD();
                IsCommandExecuted = true;
            }
            else
            {
                _move.MoveA();
                IsCommandExecuted = true;
            }

        }

        public void Undo()
        {
            if (!IsCommandExecuted)
                return;

            if (_direction == 'W')
            {
                _move.UndoW();
                IsCommandExecuted = false;
            }
            else if (_direction == 'S')
            {
                _move.UndoS();
                IsCommandExecuted = false;
            }
            else if (_direction == 'D')
            {
                _move.UndoD();
                IsCommandExecuted = false;
            }
            else
            {
                _move.UndoA();
                IsCommandExecuted = false;
            }
        }
        public char LastMove()
        {
            return _direction;
        }
    }
}
