using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using KMessage.Core.Chats;
using Microsoft.Extensions.Configuration;

namespace KMessage.Core
{
    public class Program
    {
        static IConfigurationSection config = new ConfigurationBuilder()
                                                .AddJsonFile("appsettings.json")
                                                .Build()
                                                .GetSection("appsettings")
                                                .GetSection("telegram");
        static TelegramBotClient botClient;

        static void Main(string[] args)
        {
            try
            {

                botClient = new TelegramBotClient(config["clientKey"]);
                var me = botClient.GetMeAsync().Result;

                botClient.OnMessage += Bot_OnMessage;
                botClient.StartReceiving();

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                botClient.StopReceiving();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }

        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == null)
                return;

            var text = e.Message.Text.ToLower();
            var chatId = e.Message.Chat.Id;
            var chatName = e.Message.Chat.FirstName;

            var chats = ChatFactory.CreateMany(config, text);

            foreach (var chat in chats)
            {
                await chat.SendMessageAsync(botClient, e.Message);
            }
        }
    }
}
