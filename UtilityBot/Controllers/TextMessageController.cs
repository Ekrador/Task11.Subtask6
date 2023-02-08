using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Посчитать символы" , $"count"),
                        InlineKeyboardButton.WithCallbackData($" Сложить числа" , $"add")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот способен выполнить две задачи.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Бот может посчитать количество символов в вашем сообщении или вычислить сумму введенных вами чисел (в одном сообщении через пробел).{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    string answer = String.Empty;
                    try
                    {
                        answer = Utilities.TextToTask.CallRightTask(message.Text, _memoryStorage.GetSession(message.Chat.Id).Task);
                    }
                    catch (Exception ex)
                    {
                        answer = ex.Message;
                    }
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, answer, cancellationToken: ct);
                    break;
            }
        }
    }
}
