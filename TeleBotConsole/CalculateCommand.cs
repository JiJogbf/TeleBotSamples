using System;

namespace TeleBotConsole
{
    class CalculateCommand : ICommand
    {

        private string command;
        public CalculateCommand(string command)
        {
            this.command = command;
        }

        public string Execute(string content)
        {  
            try
            {
                var preContent = content.Replace(command, "").Trim();
                var items = preContent.Split(' ');
                var first = items[0];
                var second = items[2];
                var op = items[1];

                // ^([0-9]+)\s+(\+\-)\s+([0-9]+)$
                var result = Int32.Parse(first) + Int32.Parse(second);
                Console.WriteLine($"preContent='{preContent}'");
                return $"Result is {result}";
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception accured: {e.Message}");
                return e.Message;
            }
        }
    }
}
