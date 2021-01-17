using System.Collections.Generic;

namespace ScrabbleGame.Models
{
    public class GameBoard
    {
        const int row = 15;
        const int column = 15;

        public GameSquare[,] Squares { get; private set; }

        public GameBoard()
        {
            Squares = new GameSquare[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Squares[i, j] = new GameSquare(i, j);
                }
            }
        }
    }
}
