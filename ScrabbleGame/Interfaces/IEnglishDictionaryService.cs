using System.Collections.Generic;

namespace ScrabbleGame.Interfaces
{
    public interface IEnglishDictionaryService
    {
        public List<string> GetWords();
    }
}
