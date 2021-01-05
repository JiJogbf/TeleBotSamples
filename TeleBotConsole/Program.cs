using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeleBotConsole
{

    class Program
    {
        static void FirstTestRun()
        {
            var token = "1550556419:AAH_gA2Kn465tliKxTh8sP7EarHGI4EUsIw";
            var botClient = new TelegramBotClient(token);
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );
        }

        private ITelegramBotClient botClient;
        private ICommands commands;

        public Program(ITelegramBotClient botClient, ICommands commands)
        {
            this.botClient = botClient;
            this.commands = commands;
        }

        public void Run()
        {
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");          
            Console.ReadKey();

            botClient.StopReceiving();
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                var text = e.Message.Text;
                var items = text.Split(' ');
                var commandKey = items[0];
                if (commands.Has(commandKey))
                {
                    var command = commands.GetCommand(commandKey);
                    var result = command.Execute(text);
                    var message = await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "Command '" + commandKey + "' executed. \n" + result
                    );
                    //^\.([a-z]+)\s(.+)$
                }
                else
                {
                    var message = await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "No such command:\n" + e.Message.Text
                    );
                }
            }
        }

        class HelpCommand: ICommand
        {
            public string Execute(string content)
            {
                return "Available commands:\n.calc - calc simple expression.\n.help - print this list\n";
            }
        }

        public void Settup()
        {
            commands.Add(".calc", new CalculateCommand(".calc"));
            commands.Add(".help", new HelpCommand());
        }

        static void Main()
        {
            var program = new Program(
                new TelegramBotClient("1550556419:AAH_gA2Kn465tliKxTh8sP7EarHGI4EUsIw"),
                new LoggableCommands(new DefaultCommands()));
            program.Settup();
            program.Run();
        }
    }
}
