using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronautai.Classes.Command
{
    class MoveList
    {
        private readonly List<ICommand> _commands;
        private ICommand _command;

        public MoveList()
        {
            _commands = new List<ICommand>();
        }

        public void SetCommand(ICommand command) => _command = command;

        public void Invoke()
        {
            _commands.Add(_command);
            _command.ExecuteAction();
        }

        public void Undo()
        {
            foreach (var command in Enumerable.Reverse(_commands))
            {
                command.Undo();
            }
        }
        public char GetLast()
        {
            if (_commands.Count() < 2)
            {
                return 'W';
            }
            return _commands[_commands.Count() - 2].LastMove();
        }
    }
}
