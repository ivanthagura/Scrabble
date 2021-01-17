namespace ScrabbleGame.Models
{
    public class GameSquare
    {
        public int Row { get; set; }
        public int Column { get; private set; }
        public Tile Tile { get; set; }
        public bool IsMiddle { get; set; }
        public bool IsTopCorner { get; set; }
        public bool IsBottomCorner { get; set; }
        public bool IsLeftCorner { get; set; }
        public bool IsRightCorner { get; set; }

        public GameSquare(int row, int column, int totalRows, int totalColumns)
        {
            Row = row;
            Column = column;
            Tile = null;

            if(row == 0)
            {
                IsTopCorner = true;
            }
            if(row == (totalRows - 1))
            {
                IsBottomCorner = true;
            }
            if (column == 0)
            {
                IsLeftCorner = true;
            }
            if (column == (totalColumns - 1))
            {
                IsRightCorner = true;
            }
            if(row == (totalRows/2) && column == (totalColumns/2))
            {
                IsMiddle = true;
            }
        }

        public string Value()
        {
            if (Tile != null)
            {
                return $" {Tile.Letter} |";
            }

            return $"   |";
        }
    }
}
