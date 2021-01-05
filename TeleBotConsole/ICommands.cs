using System;

namespace TeleBotConsole
{
    interface ICommands
    {
        ICommand GetCommand(string key);
        
        bool Has(string key);
        void Add(string key, ICommand command);
    }
}
