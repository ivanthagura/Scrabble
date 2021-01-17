using Microsoft.Extensions.Options;
using ScrabbleGame.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace ScrabbleGame.Services
{
    public class EnglishDictionaryService : IEnglishDictionaryService
    {
        private readonly IOptions<DictionaryConfiguration> _config;

        public EnglishDictionaryService(IOptions<DictionaryConfiguration> config)
        {
            _config = config;
        }

        public List<string> GetWords()
        {
            var words = new List<string>();
            words.AddRange(ReadFromFile());
            return words;
        }

        private string[] ReadFromFile()
        {
            var filePath = _config.Value.FilePath + _config.Value.FileName;
            var lines = File.ReadAllLines(filePath);
            return lines;
        }
    }
}
