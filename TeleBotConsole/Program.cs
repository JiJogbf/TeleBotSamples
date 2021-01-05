using System;using Telegram.Bot;
using Telegram.Bot.Args;

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

        public Program(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
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

                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "You said:\n" + e.Message.Text
                );
            }
        }


        static void Main()
        {
            var program = new Program(
                new TelegramBotClient("1550556419:AAH_gA2Kn465tliKxTh8sP7EarHGI4EUsIw")); 
            program.Run();
        }
    }
}
