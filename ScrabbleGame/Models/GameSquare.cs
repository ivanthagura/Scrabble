namespace ScrabbleGame.Models
{
    public class GameSquare
    {
        public int Row { get; set; }
        public int Column { get; private set; }
        public Tile Tile { get; set; }

        public GameSquare(int row, int column)
        {
            Row = row;
            Column = column;
            Tile = null;
        }

        public string Value()
        {
            if (Tile != null)
            {
                return Tile.Letter.ToString();
            }

            return $"[{Row} {Column}]";
        }
    }
}
