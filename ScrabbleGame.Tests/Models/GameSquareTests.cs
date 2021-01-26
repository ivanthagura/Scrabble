using ScrabbleGame.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrabbleGame.Tests.Models
{
    public class GameSquareTests
    {
        [Fact]
        public void Creating_GameSquare_Sets_Corners_And_Middle()
        {
            // top corner
            var testGameSquare = new GameSquare(0, 3, 7, 7);
            Assert.True(testGameSquare.IsTopCorner);

            // top corner
            testGameSquare = new GameSquare(6, 3, 7, 7);
            Assert.True(testGameSquare.IsBottomCorner);

            // left corner
            testGameSquare = new GameSquare(3, 0, 7, 7);
            Assert.True(testGameSquare.IsLeftCorner);

            // right corner
            testGameSquare = new GameSquare(3, 6, 7, 7);
            Assert.True(testGameSquare.IsRightCorner);

            // middle
            testGameSquare = new GameSquare(3, 3, 7, 7);
            Assert.True(testGameSquare.IsMiddle);
        }
    }
}
