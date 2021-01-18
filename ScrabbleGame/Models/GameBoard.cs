using ScrabbleGame.Utils;
using System.Collections.Generic;

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

        public void AddWordToBoard(List<Tile> word, string horizontalOrVertical)
        {
            var middleSquare = GetMiddleSquare();
            if(!middleSquare.IsSquareFilled())
            {
                var wordStartingPoint = (word.Count / 2);
                var startingPointRow =
                    horizontalOrVertical.Equals(ConstantStrings.VERTICAL) ? middleSquare.Row - wordStartingPoint : middleSquare.Row;
                var startingPointColumn =
                    horizontalOrVertical.Equals(ConstantStrings.HORIZONTAL) ? middleSquare.Column - wordStartingPoint : middleSquare.Column;

                if (horizontalOrVertical.Equals(ConstantStrings.HORIZONTAL))
                {
                    foreach (var letter in word)
                    {
                        Squares[startingPointRow, startingPointColumn].Tile = letter;
                        startingPointColumn += 1;
                    }
                }
                else
                {
                    foreach (var letter in word)
                    {
                        Squares[startingPointRow, startingPointColumn].Tile = letter;
                        startingPointRow += 1;
                    }
                }
            }
        }

        public GameSquare GetMiddleSquare()
        {
            foreach (var square in Squares)
            {
                if (square.IsMiddle)
                {
                    return square;
                }
            }
            return null;
        }        

        public List<char> GetAvailableLetters()
        {
            var letters = new List<char>();
            foreach (var square in Squares)
            {
                if (square.Tile != null)
                {
                    letters.Add(square.Tile.Letter);
                }
            }

            return letters;
        }
    }
}
