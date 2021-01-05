using System;

namespace TeleBotConsole
{
    class CommandsDecorator : ICommands
    {
        private ICommands origin;

        public CommandsDecorator(ICommands newOrigin)
        {
            this.origin = newOrigin;
        }

        public virtual void Add(string key, ICommand command)
        {
            origin.Add(key, command);
        }

        public virtual bool Has(string key)
        {
            return origin.Has(key);
        }

        public virtual ICommand GetCommand(string key)
        {
            return origin.GetCommand(key);
        }
    }

    class LoggableCommands : CommandsDecorator
    {

        public LoggableCommands(ICommands commands): base(commands) {}

        public override void Add(string key, ICommand command)
        {
            Console.WriteLine($"Command with key={key}");
            base.Add(key, command);
        }
        public override bool Has(string key)
        {
            Console.WriteLine($"Do we have such a key={key}");
            bool result = base.Has(key);
            if (result)
            {
                Console.WriteLine($"Yes, we have key: {key}");
            }
            else
            {
                Console.WriteLine($"No such key: {key}");
            }
            return result;
        }

        public override ICommand GetCommand(string key)
        {
            Console.WriteLine($"Accessing command = {key}");
            return base.GetCommand(key);
        }
    }
}
