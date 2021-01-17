using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScrabbleGame.Interfaces;
using System;

namespace ScrabbleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //configure console logging
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Logger is working!");

            // Get Service and call method
            var service = serviceProvider.GetService<IEnglishDictionaryService>();
            var words = service.GetWords();
            Console.WriteLine(words[0]);
        }
    }
}
