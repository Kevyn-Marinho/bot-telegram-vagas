using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace KMessage.Core.Chats
{
    public static class ChatFactory
    {
        private static string[] FrontEndTecnologies = { "react", "react-native", "fluter", "angular" };
        private static string[] BackendTecnologies = { ".net", "c#", "php", "node" };

        public static List<Chat> CreateMany(IConfigurationSection config, string text)
        {
            var chats = new List<Chat>();

            foreach (var tech in BackendTecnologies)
            {
                if (text.Contains(tech))
                {
                    chats.Add(new Chat(int.Parse(config["backendChat"]), ChatType.Backend ,tech));
                    break;
                }
            }

            foreach (var tech in FrontEndTecnologies)
            {
                if (text.Contains(tech)){
                    chats.Add(new Chat(int.Parse(config["frontendChat"]), ChatType.Frontend, tech));
                    break;
                }
            }

            return chats;
        }
    }
}