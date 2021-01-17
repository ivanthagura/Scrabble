using ScrabbleGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrabbleGame.Models
{
    public class GamePack
    {
        public GameBoard GameBoard { get; private set; }
        public List<Tile> Tiles { get; private set; }
        public Rack Player1Rack { get; private set; }
        public Rack Player2Rack { get; private set; }
        public List<string> Dictionary { get; private set; }

        public GamePack(GameBoard gameBoard, List<Tile> tiles, Rack player1Rack, Rack player2Rack, List<string> dictionary)
        {
            GameBoard = gameBoard;
            Tiles = tiles;
            Player1Rack = player1Rack;
            Player2Rack = player2Rack;
            Dictionary = dictionary;
        }

        public void AssignTilesToPlayer1Rack()
        {
            var rackCount = Player1Rack.Tiles.Count;
            while (rackCount < Player1Rack.Size)
            {
                Player1Rack.Tiles.Add(Tiles.First());
                Tiles.RemoveAt(0);
                rackCount++;
            }
        }

        public void AssignTilesToPlayer2Rack()
        {
            var rackCount = Player2Rack.Tiles.Count;
            while (rackCount < Player2Rack.Size)
            {
                Player2Rack.Tiles.Add(Tiles.First());
                Tiles.RemoveAt(0);
                rackCount++;
            }
        }

        public bool PlayTurn()
        {
            if(Player1Rack.IsTurn)
            {
                Play(Player1Rack);
            }
            else if(Player2Rack.IsTurn)
            {
                Play(Player2Rack);
            }
            return false;
        }

        private bool Play(Rack Player)
        {
            Console.WriteLine($"{Player.Name}'s turn");
            bool isValidWord;

            do
            {
                var word = Player.PlayWord();
                if (word.Equals(ConstantStrings.YEILD))
                {
                    Player.SetTurn(false);
                    break;
                }
                else if (!WordIsFormedFromAvailableLetters(Player.Tiles, word.ToUpper()))
                {
                    Console.WriteLine(word + " is not created from the available word. Please try again or type yeild to give up");
                    isValidWord = false;
                }
                else
                {
                    isValidWord = ValidateWord(word);
                    if (!isValidWord)
                    {
                        Console.WriteLine(word + " is not a valid word. Please try again or type yeild to give up");
                    }
                    else
                    {
                        Console.WriteLine(word);
                    }
                }
            } while (!isValidWord);
            
            return true;
        }

        private bool WordIsFormedFromAvailableLetters(List<Tile> playerTiles, string word)
        {
            var availableLetters = playerTiles.Select(t => t.Letter).ToList();
            foreach (var letter in word)
            {
                if(availableLetters.Contains(letter))
                {
                    availableLetters.Remove(letter);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateWord(string word)
        {
            return Dictionary.Contains(word);
        }
    }
}
