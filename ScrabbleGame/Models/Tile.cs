using System;

namespace ScrabbleGame.Models
{
    public class Tile
    {
        public char Letter { get; private set; }
        public int Point { get; private set; }

        public Tile(int point = 1)
        {
            // Set random letter
            var random = new Random();
            var letterNumber = random.Next(0, 26);
            Letter = (char)('A' + letterNumber);
            Point = point;
        }
    }
}
