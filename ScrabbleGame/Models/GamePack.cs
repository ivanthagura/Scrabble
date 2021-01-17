using System.Collections.Generic;

namespace ScrabbleGame.Models
{
    public class GamePack
    {
        public GameBoard GameBoard { get; private set; }
        public Tile[] Tiles { get; private set; }
        public Rack Player1Rack { get; private set; }
        public Rack Player2Rack { get; private set; }
        public List<string> Dictionary { get; private set; }

        public GamePack(GameBoard gameBoard, Tile[] tiles, Rack player1Rack, Rack player2Rack, List<string> dictionary)
        {
            GameBoard = gameBoard;
            Tiles = tiles;
            Player1Rack = player1Rack;
            Player2Rack = player2Rack;
            Dictionary = dictionary;
        }
    }
}
