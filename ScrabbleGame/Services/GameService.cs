using ScrabbleGame.Interfaces;
using ScrabbleGame.Models;
using System;
using System.Collections.Generic;

namespace ScrabbleGame.Services
{
    public class GameService : IGameService
    {
        private readonly IEnglishDictionaryService _englishDictionaryService;

        public GameService(IEnglishDictionaryService englishDictionaryService)
        {
            _englishDictionaryService = englishDictionaryService;
        }

        public void StartGame()
        {
            var game = CreateGamePack();
            for (int i = 0; i < game.GameBoard.Squares.GetLength(0); i++)
            {
                for (int j = 0; j < game.GameBoard.Squares.GetLength(1); j++)
                {
                    Console.Write(game.GameBoard.Squares[i,j].Value());
                }
                Console.WriteLine("");
            }         
        }

        private GamePack CreateGamePack()
        {
            var gameBoard = new GameBoard();
            
            var tiles = new List<Tile>();
            for (int tile = 0; tile < 100; tile++)
            {
                tiles.Add(new Tile());
            }

            var player1Rack = new Rack();
            var player2Rack = new Rack();

            var dictionary = _englishDictionaryService.GetWords();

            var gamePack = new GamePack(gameBoard, tiles.ToArray(), player1Rack, player2Rack, dictionary);
            return gamePack;
        }
    }
}
