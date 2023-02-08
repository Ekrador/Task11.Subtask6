using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Services;

namespace UtilityBot
{
    static class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Starting Service");
            await host.RunAsync();
            Console.WriteLine("Service stopped");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddHostedService<Bot>();
        }
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6137671982:AAHYT4ma9bBCNSMCQm7hmWuPvW6wRKWsI2U",               
            };
        }
    }
}