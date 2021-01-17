using System.Collections.Generic;

namespace ScrabbleGame.Models
{
    public class Rack
    {
        public int Size { get; private set; }
        public List<Tile> Tiles { get; set; }

        public Rack(int size = 7)
        {
            Size = size;
            Tiles = new List<Tile>();
        }
    }
}
