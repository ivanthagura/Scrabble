using ScrabbleGame.Utils;
using System.Collections.Generic;
using System.Linq;

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

        public bool AddWordToBoard(List<Tile> tilesFromTray, Tile selectedTileFromBoard, string word)
        {
            int wordStartingPoint;
            int startingPointRow;
            int startingPointColumn;
            var middleSquare = GetMiddleSquare();

            if (!middleSquare.IsSquareFilled())
            {
                wordStartingPoint = (word.Length / 2);
                startingPointRow = middleSquare.Row;
                //    horizontalOrVertical.Equals(ConstantStrings.VERTICAL) ? middleSquare.Row - wordStartingPoint : middleSquare.Row;
                startingPointColumn = middleSquare.Column - wordStartingPoint;
                //  horizontalOrVertical.Equals(ConstantStrings.HORIZONTAL) ? middleSquare.Column - wordStartingPoint : middleSquare.Column;

                foreach (var tile in tilesFromTray)
                {
                    Squares[startingPointRow, startingPointColumn].Tile = tile;
                    startingPointColumn += 1;
                }

                return true;
            }
            else
            {
                GameSquare selectedSquare = null;
                foreach (var square in Squares)
                {
                    if(square.Tile == selectedTileFromBoard)
                    {
                        selectedSquare = square;
                    }
                }
                var letterAvailabilities = GetLetterAvailabilities(selectedSquare);
                if(letterAvailabilities != null && letterAvailabilities.Count > 0)
                {
                    var positionOfSelectedLetter = word.IndexOf(selectedTileFromBoard.Letter);

                    // selected letter is first letter in word
                    if (positionOfSelectedLetter == 0)
                    {
                        // start with vertical
                        if(letterAvailabilities.Contains(LetterAvailability.Bottom))
                        {
                            startingPointRow = selectedSquare.Row;
                            startingPointColumn = selectedSquare.Column;

                            // block top
                            selectedSquare.IsTopFilled = true;

                            // go to second letter
                            startingPointRow += 1;

                            foreach (var letter in tilesFromTray)
                            {
                                Squares[startingPointRow, startingPointColumn].Tile = letter;

                                if(letter == tilesFromTray.Last())
                                {
                                    // block bottom
                                    Squares[startingPointRow, startingPointColumn].IsBottomFilled = true;
                                }

                                startingPointRow += 1;
                            }
                        }
                        // move to horizontal
                        else if (letterAvailabilities.Contains(LetterAvailability.Right))
                        {
                            startingPointRow = selectedSquare.Row;
                            startingPointColumn = selectedSquare.Column;

                            // block left
                            selectedSquare.IsLeftFilled = true;

                            // go to second letter
                            startingPointColumn += 1;

                            foreach (var letter in tilesFromTray)
                            {
                                Squares[startingPointRow, startingPointColumn].Tile = letter;

                                if (letter == tilesFromTray.Last())
                                {
                                    // block right
                                    Squares[startingPointRow, startingPointColumn].IsRightFilled = true;
                                }

                                startingPointColumn += 1;
                            }
                        }
                    }
                    // selected letter is last letter in word
                    else if(positionOfSelectedLetter == word.Length - 1)
                    {
                        // start with vertical
                        if (letterAvailabilities.Contains(LetterAvailability.Top))
                        {
                            startingPointRow = selectedSquare.Row;
                            startingPointColumn = selectedSquare.Column;

                            // block bottom
                            selectedSquare.IsBottomFilled = true;

                            // go to first letter
                            startingPointRow -= (word.Length - 1);

                            foreach (var letter in tilesFromTray)
                            {
                                Squares[startingPointRow, startingPointColumn].Tile = letter;

                                if (letter == tilesFromTray.First())
                                {
                                    // block top
                                    Squares[startingPointRow, startingPointColumn].IsTopFilled = true;
                                }

                                startingPointRow += 1;
                            }
                        }
                        // move to horizontal
                        else if (letterAvailabilities.Contains(LetterAvailability.Left))
                        {
                            startingPointRow = selectedSquare.Row;
                            startingPointColumn = selectedSquare.Column;

                            // block right
                            selectedSquare.IsRightFilled = true;

                            // go to first letter
                            startingPointColumn -= (word.Length - 1);

                            foreach (var letter in tilesFromTray)
                            {
                                Squares[startingPointRow, startingPointColumn].Tile = letter;

                                if (letter == tilesFromTray.First())
                                {
                                    // block left
                                    Squares[startingPointRow, startingPointColumn].IsLeftFilled = true;
                                }

                                startingPointColumn += 1;
                            }
                        }
                    }
                    // selected letter is in the middle
                    else
                    {
                        // start with vertical
                        if (letterAvailabilities.Contains(LetterAvailability.Top) 
                            && letterAvailabilities.Contains(LetterAvailability.Bottom))
                        {
                            startingPointRow = selectedSquare.Row;
                            startingPointColumn = selectedSquare.Column;

                            // block top and bottom
                            selectedSquare.IsTopFilled = true;
                            selectedSquare.IsBottomFilled = true;

                            // go to first letter
                            startingPointRow -= positionOfSelectedLetter;

                            foreach (var letter in tilesFromTray)
                            {
                                if (startingPointRow == selectedSquare.Row)
                                {
                                    startingPointRow += 1;
                                }
                                Squares[startingPointRow, startingPointColumn].Tile = letter;

                                if (letter == tilesFromTray.First())
                                {
                                    // block top
                                    Squares[startingPointRow, startingPointColumn].IsTopFilled = true;
                                }
                                if (letter == tilesFromTray.Last())
                                {
                                    // block bottom
                                    Squares[startingPointRow, startingPointColumn].IsBottomFilled = true;
                                }

                                startingPointRow += 1;
                            }
                        }
                        // move to horizontal
                        else if (letterAvailabilities.Contains(LetterAvailability.Left)
                            && letterAvailabilities.Contains(LetterAvailability.Right))
                        {
                            startingPointRow = selectedSquare.Row;
                            startingPointColumn = selectedSquare.Column;

                            // block left and right
                            selectedSquare.IsLeftFilled = true;
                            selectedSquare.IsRightFilled = true;

                            // go to first letter
                            startingPointColumn -= positionOfSelectedLetter;

                            foreach (var letter in tilesFromTray)
                            {
                                if (startingPointColumn == selectedSquare.Column)
                                {
                                    startingPointColumn += 1;
                                }
                                Squares[startingPointRow, startingPointColumn].Tile = letter;

                                if (letter == tilesFromTray.First())
                                {
                                    // block left
                                    Squares[startingPointRow, startingPointColumn].IsLeftFilled = true;
                                }
                                if (letter == tilesFromTray.Last())
                                {
                                    // block right
                                    Squares[startingPointRow, startingPointColumn].IsRightFilled = true;
                                }

                                startingPointColumn += 1;
                            }
                        }
                    }
                    return true;
                }
                return false;
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

        public List<Tile> GetAvailableLetters()
        {
            var letters = new List<Tile>();
            foreach (var square in Squares)
            {
                setSquareFilledPositions(square);
                if (square.IsSquareFilled())
                {
                    var squareAvailabities = GetLetterAvailabilities(square);
                    if (squareAvailabities.Count > 0)
                    {
                        letters.Add(square.Tile);
                    }
                }
            }

            return letters;
        }

        private void setSquareFilledPositions(GameSquare square)
        {
            if (!square.IsTopCorner)
            {
                var topSquare = Squares[square.Row - 1, square.Column];
                if (topSquare.IsSquareFilled())
                {
                    square.IsTopFilled = true;
                }
            }
            if(!square.IsBottomCorner)
            {
                var bottomSquare = Squares[square.Row + 1, square.Column];
                if (bottomSquare.IsSquareFilled())
                {
                    square.IsBottomFilled = true;
                }
            }
            if (!square.IsLeftCorner)
            {
                var leftSquare = Squares[square.Row, square.Column - 1];
                if (leftSquare.IsSquareFilled())
                {
                    square.IsLeftFilled = true;
                }
            }
            if (!square.IsRightCorner)
            {
                var rightSquare = Squares[square.Row, square.Column + 1];
                if (rightSquare.IsSquareFilled())
                {
                    square.IsRightFilled = true;
                }
            }
            if (!square.IsTopCorner && !square.IsLeftCorner)
            {
                var topLeftSquare = Squares[square.Row - 1, square.Column - 1];
                if (topLeftSquare.IsSquareFilled())
                {
                    square.IsTopLeftFilled = true;
                }
            }
            if (!square.IsTopCorner && !square.IsRightCorner)
            {
                var topRightSquare = Squares[square.Row - 1, square.Column + 1];
                if (topRightSquare.IsSquareFilled())
                {
                    square.IsTopRightFilled = true;
                }
            }
            if (!square.IsBottomCorner && !square.IsLeftCorner)
            {
                var bottomLeftSquare = Squares[square.Row + 1, square.Column - 1];
                if (bottomLeftSquare.IsSquareFilled())
                {
                    square.IsBottomLeftFilled = true;
                }
            }
            if (!square.IsBottomCorner && !square.IsRightCorner)
            {
                var bottomRightSquare = Squares[square.Row + 1, square.Column + 1];
                if (bottomRightSquare.IsSquareFilled())
                {
                    square.IsBottomRightFilled = true;
                }
            }
        }

        private List<LetterAvailability> GetLetterAvailabilities(GameSquare square)
        {
            var availabilities = new List<LetterAvailability>();

            if(square == null)
            {
                return null;
            }

            if(!square.IsTopFilled && !square.IsTopLeftFilled && !square.IsTopRightFilled)
            {
                availabilities.Add(LetterAvailability.Top);
            }
            if (!square.IsBottomFilled && !square.IsBottomLeftFilled && !square.IsBottomRightFilled)
            {
                availabilities.Add(LetterAvailability.Bottom);
            }
            if (!square.IsLeftFilled && !square.IsTopLeftFilled && !square.IsBottomLeftFilled)
            {
                availabilities.Add(LetterAvailability.Left);
            }
            if (!square.IsRightFilled && !square.IsTopRightFilled && !square.IsBottomRightFilled)
            {
                availabilities.Add(LetterAvailability.Right);
            }

            return availabilities;
        }
    }
}
