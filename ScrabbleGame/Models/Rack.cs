using ScrabbleGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrabbleGame.Models
{
    public class Rack
    {
        public string Name { get; private set; }
        public bool IsTurn { get; private set; }
        public int Size { get; private set; }
        public List<Tile> Tiles { get; set; }
        public int Score { get; private set; }
        public bool Yeild { get; private set; }

        public Rack(string name, bool isTurn, int size = 7)
        {
            Name = name;
            IsTurn = isTurn;
            Size = size;
            Tiles = new List<Tile>();
            Score = 0;
            Yeild = false;
        }

        public void AddPoints(int points)
        {
            Score += points;
        }

        public void SetPlayerName(string name)
        {
            Name = name;
        }

        public void SetTurn(bool turn)
        {
            IsTurn = turn;
        }

        public void YeildGame()
        {
            Yeild = true;
        }

        public string PlayWord()
        {
            Console.WriteLine("Available Letters : " + PrintAvailableLetters());
            var word = Console.ReadLine();
            return word.ToLower();
        }

        public string PlayHorizontalOrVertical()
        {
            Console.WriteLine("Play Horizontal(h) or Vertical(v)?");
            var decision = "";
            var validDecision = false;
            do
            {
                decision = Console.ReadLine();
                if (decision.Equals("h"))
                {
                    decision = ConstantStrings.HORIZONTAL;
                    validDecision = true;
                }
                else if (decision.Equals("v"))
                {
                    decision = ConstantStrings.VERTICAL;
                    validDecision = true;
                }
                else
                {
                    Console.WriteLine("Invalid decision. Press 'h' for Horizontal and 'v' for Vertical");
                }
            } while (!validDecision);

            return decision;
        }

        public string PrintAvailableLetters()
        {
            var delimiter = " , ";
            return Tiles.Select(t => t.Letter.ToString()).Aggregate((i, j) => i + delimiter + j);
        }
    }
}
