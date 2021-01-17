namespace ScrabbleGame.Models
{
    public class GameBoard
    {
        const int totalRows = 15;
        const int totalColumns = 15;

        public GameSquare[,] Squares { get; private set; }

        public GameBoard()
        {
            Squares = new GameSquare[totalRows, totalColumns];
            for (int r = 0; r < totalRows; r++)
            {
                for (int c = 0; c < totalColumns; c++)
                {
                    Squares[r, c] = new GameSquare(r, c, totalRows, totalColumns);
                }
            }
        }

        public GameSquare GetMiddleSquare()
        {
            foreach (var square in Squares)
            {
                if(square.IsMiddle)
                {
                    return square;
                }
            }
            return null;
        }
    }
}
