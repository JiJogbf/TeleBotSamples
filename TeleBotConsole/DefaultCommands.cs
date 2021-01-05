using System;
using System.Collections.Generic;
using System.Text;

namespace TeleBotConsole
{
    class DefaultCommands : ICommands
    {
        private Dictionary<string, ICommand> commands =
                new Dictionary<string, ICommand>();

        public void Add(string key, ICommand command)
        {
            if (!Has(key))
            {
                commands.Add(key, command);
            }
        }

        public bool Has(string key)
        {
            return commands.ContainsKey(key);
        }

        public ICommand GetCommand(string key)
        {
            return commands[key];
        }
    }

}
