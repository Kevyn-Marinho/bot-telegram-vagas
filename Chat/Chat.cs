using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KMessage.Core.Chats
{

    public class Chat
    {
        public int Id { get; }
        public ChatType Type { get; }
        public string Technology { get; }

        public Chat(int id, ChatType type, string technology)
        {
            this.Id = id;
            this.Type = type;
            this.Technology = technology;
        }

        public override string ToString()
        {

            return this.Technology.ToLower().Replace("#", "sharp").Replace(".net", "dotnet");
        }

        public async Task SendMessageAsync(TelegramBotClient client, Message message)
        {
            var chatName = message.Chat.FirstName;
            var text = message.Text;

            await client.SendTextMessageAsync(
                chatId: this.Id,
                text: $"Recebi uma vaga de {this.Type} com #{this.ToString()} de " +
                        $"{chatName}, se liga\n\n... {text}");
        }
    }

}