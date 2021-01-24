using ScrabbleGame.Interfaces;
using ScrabbleGame.Models;
using System;
using System.Collections.Generic;

namespace ScrabbleGame.Services
{
    public class GameService : IGameService
    {
        private readonly IEnglishDictionaryService _englishDictionaryService;
        private GamePack Game;

        public GameService(IEnglishDictionaryService englishDictionaryService)
        {
            _englishDictionaryService = englishDictionaryService;
        }

        public void StartGame()
        {
            Console.WriteLine("Enter Player 1 Name");
            var player1Name = Console.ReadLine();
            Console.WriteLine("Enter Player 2 Name");
            var player2Name = Console.ReadLine();

            Game = CreateGamePack(player1Name, player2Name);

            Game.AssignTilesToPlayerRack(Game.Player1Rack);
            Game.AssignTilesToPlayerRack(Game.Player2Rack);

            bool gameOn;
            do
            {
                PrintGame();
                gameOn = Game.PlayTurn();
            } while (gameOn);

            Console.WriteLine();
            Console.WriteLine("Game is finished");
            Console.WriteLine($"{Game.Player1Rack.Name}'s score : {Game.Player1Rack.Score}");
            Console.WriteLine($"{Game.Player2Rack.Name}'s score : {Game.Player2Rack.Score}");

            if (Game.Player1Rack.Score == Game.Player2Rack.Score)
            {
                Console.WriteLine("Game is tied");
            }
            else
            {
                var winner = Game.Player1Rack.Score > Game.Player2Rack.Score ? Game.Player1Rack.Name : Game.Player2Rack.Name;
                Console.WriteLine($"The winner is : {winner}");
            }
            Console.WriteLine("Thank you for playing :)");
        }

        private GamePack CreateGamePack(string player1Name, string player2Name)
        {
            var gameBoard = new GameBoard();

            var tilePack = CreateTilePack();

            var player1Rack = new Rack(player1Name, true);
            var player2Rack = new Rack(player2Name, false);

            var dictionary = _englishDictionaryService.GetWords();

            var gamePack = new GamePack(gameBoard, tilePack, player1Rack, player2Rack, dictionary);
            return gamePack;
        }

        private void PrintGame()
        {
            Console.WriteLine();
            Console.WriteLine("  -------------------------------------------------------------");
            for (int i = 0; i < Game.GameBoard.Squares.GetLength(0); i++)
            {               
                Console.Write("  |");
                for (int j = 0; j < Game.GameBoard.Squares.GetLength(1); j++)
                {
                    Console.Write(Game.GameBoard.Squares[i, j].Value());
                }
                Console.WriteLine("");
                Console.WriteLine("  -------------------------------------------------------------");
            }
            Console.WriteLine();
        }

        private List<Tile> CreateTilePack()
        {
            // Standard Scrabble tile letter distribution
            // A-11, B-2, C-2, D-4, E-12, F-2, G-3, H-2, I-9, J-1, K-1, L-4, M-2, N-6, O-8, P-2, Q-1, R-6, S-4, T-6, U-4, V-2, W-2, X-1, Y-2, Z-1
            var letters = new char[] { 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'B', 'B',
            'C', 'C', 'D', 'D', 'D', 'D', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E',
            'F', 'F', 'G', 'G', 'G', 'H', 'H', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'J', 'K',
            'L', 'L', 'L', 'L', 'M', 'M', 'N', 'N', 'N', 'N', 'N', 'N', 'O', 'O', 'O', 'O', 'O', 'O',
            'O', 'O', 'P', 'P', 'Q', 'R', 'R', 'R', 'R', 'R', 'R', 'S', 'S', 'S', 'S', 'T', 'T', 'T',
            'T', 'T', 'T', 'U', 'U', 'U', 'U', 'V', 'V', 'W', 'W', 'X', 'Y', 'Y', 'Z'};

            // Shuffle letters
            var random = new Random();
            for (int x = 0; x < 100; x++)
            {
                for (int i = letters.Length; i > 1; i--)
                {
                    // Pick random element to swap.
                    int j = random.Next(i); // 0 <= j <= i-1
                    // Swap.
                    var tmp = letters[j];
                    letters[j] = letters[i - 1];
                    letters[i - 1] = tmp;
                }
            }

            var tilePack = new List<Tile>();
            foreach (var letter in letters)
            {
                tilePack.Add(new Tile(letter,1));
            }

            return tilePack;
        }
    }
}
