using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrabbleGame.Interfaces;
using ScrabbleGame.Services;

namespace ScrabbleGame
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();
            services.Configure<DictionaryConfiguration>(Configuration.GetSection("EnglishDictionary"));

            services.AddSingleton<IEnglishDictionaryService, EnglishDictionaryService>();
            services.AddSingleton<IGameService, GameService>();
        }
    }
}
