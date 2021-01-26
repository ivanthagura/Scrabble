namespace ScrabbleGame.Models
{
    public class Tile
    {
        public char Letter { get; private set; }
        public int Point { get; private set; }

        public Tile(char letter, int point = 1)
        {
            Letter = letter;
            Point = point;
        }
    }
}
