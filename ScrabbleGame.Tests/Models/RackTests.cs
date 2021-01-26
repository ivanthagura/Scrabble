using ScrabbleGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ScrabbleGame.Tests.Models
{
    public class RackTests
    {
        [Fact]
        public void Adding_Points_To_Rack_Sets_Point()
        {
            var testRack = new Rack("test rack", true);

            Assert.Equal(0, testRack.Score);

            // Add points
            testRack.AddPoints(5);

            Assert.Equal(5, testRack.Score);
        }

        [Fact]
        public void Selecting_Letter_From_Board_Returns_Selected_Letter()
        {
            var testRack = new Rack("test rack", true);
            var tiles = new List<Tile>
            {
                new Tile('A'),
                new Tile('B'),
                new Tile('C'),
                new Tile('D')
            };
            testRack.Tiles = tiles;

            var letterString = testRack.PrintAvailableLetters();
            Assert.Equal("A , B , C , D", letterString);
        }
    }
}
