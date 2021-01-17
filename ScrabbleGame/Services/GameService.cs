using ScrabbleGame.Interfaces;
using ScrabbleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

            Game.AssignTilesToPlayer1Rack();
            Game.AssignTilesToPlayer2Rack();

            bool gameOn;
            do
            {
                PrintGame();
                gameOn = Game.PlayTurn();
            } while (gameOn);
        }

        private GamePack CreateGamePack(string player1Name, string player2Name)
        {
            var gameBoard = new GameBoard();
            
            var tilePack = new List<Tile>();
            for (int tile = 0; tile < 100; tile++)
            {
                tilePack.Add(new Tile());
            }

            var player1Rack = new Rack(player1Name, true);
            var player2Rack = new Rack(player2Name, false);

            var dictionary = _englishDictionaryService.GetWords();

            var gamePack = new GamePack(gameBoard, tilePack, player1Rack, player2Rack, dictionary);
            return gamePack;
        }

        private void PrintGame()
        {
            for (int i = 0; i < Game.GameBoard.Squares.GetLength(0); i++)
            {
                
                Console.Write("|");
                for (int j = 0; j < Game.GameBoard.Squares.GetLength(1); j++)
                {
                    Console.Write(Game.GameBoard.Squares[i, j].Value());
                }
                Console.WriteLine("");
                Console.WriteLine("_______________________________");
            }
        }
    }
}
