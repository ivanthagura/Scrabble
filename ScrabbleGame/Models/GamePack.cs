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

        public void AssignTilesToPlayerRack(Rack playerRack)
        {
            var rackCount = playerRack.Tiles.Count;
            while (rackCount < playerRack.Size && Tiles.Count > 0)
            {
                playerRack.Tiles.Add(Tiles.First());
                Tiles.RemoveAt(0);
                rackCount++;
            }
        }

        public bool PlayTurn()
        {
            if(Player1Rack.IsTurn && !Player1Rack.Yeild)
            {
                Play(Player1Rack);
                Player2Rack.SetTurn(true);
                return true;
            }
            else if(Player2Rack.IsTurn && !Player2Rack.Yeild)
            {
                Play(Player2Rack);
                Player1Rack.SetTurn(true);
                return true;
            }
            return false;
        }

        private bool Play(Rack Player)
        {
            Console.WriteLine($"{Player.Name}'s turn");
            Console.WriteLine("Type 'yeild' to yeild game");
            Console.WriteLine("Form a word from letters from your rack");
            bool isValidWord;

            do
            {
                var word = Player.PlayWord();
                if (word.Equals(ConstantStrings.YEILD))
                {
                    Player.SetTurn(false);
                    Player.YeildGame();
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
                        Player.AddPoints(word.Length);
                        Console.WriteLine($"Current points for {Player.Name} : {Player.Score}");
                        RemoveWordTilesFromPlayerRack(Player.Tiles, word.ToUpper());
                        Player.SetTurn(false);
                        Console.WriteLine("Remaining letters : " + Player.PrintAvailableLetters());
                        Console.WriteLine("Adding new letters");
                        AssignTilesToPlayerRack(Player);
                        Console.WriteLine("Available letters : " + Player.PrintAvailableLetters());
                        Console.WriteLine();
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

        private void RemoveWordTilesFromPlayerRack(List<Tile> playerTiles, string word)
        {
            foreach (var letter in word)
            {
                var tileToRemove = playerTiles.Where(t => t.Letter.Equals(letter)).First();
                playerTiles.Remove(tileToRemove);
            }
        }
    }
}
