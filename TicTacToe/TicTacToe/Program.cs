using System;
using System.Collections.Generic;
using TicTacToe.DataRecorder;
using TicTacToe.Game;
using TicTacToe.Game.DataStructures;

namespace TicTacToe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");

            Console.WriteLine("\nWhat would you like to do?\n");

            Console.WriteLine("1. Play the game");
            Console.WriteLine("2. Play 5000 random games");
            Console.WriteLine("3. Quit");

            Console.WriteLine();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    PlayGame();
                    break;

                case "2":
                    PlayRandomGames(5000);

                    Console.WriteLine("\nClearing down training file...");
                    TSVRecorder tsvRecorder = new TSVRecorder();
                    tsvRecorder.RemoveEmptyLines();
                    Console.WriteLine("\nCleared down training file.");

                    break;

                case "3":
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }

        private static void ShowBoard(char[,] board)
        {
            Console.WriteLine("\n    1   2   3");

            for (int x = 0; x < 3; x++)
            {
                Console.WriteLine("    -   -   -");
                Console.Write($"{x + 1} ");

                for (int y = 0; y < 3; y++)
                {
                    char tileValue = board[x, y];
                    Console.Write(tileValue == default ? "|   " : $"| {tileValue} ");
                }

                Console.Write("|\n");
            }

            Console.WriteLine("    -   -   -\n");
        }

        private static void PlayGame()
        {
            TTTGame game = new TTTGame();
            TTTModel model = new TTTModel();
            List<TTTModel> models = new List<TTTModel>();
            char player = 'x';

            ShowBoard(game.Board);

            do
            {
                TTTCoord move;

                do
                {
                    Console.WriteLine("Vertical coordinate:");
                    string vert = Console.ReadLine();

                    Console.WriteLine("Horizontal coordinate:");
                    string hori = Console.ReadLine();

                    move = new TTTCoord(Convert.ToInt32(vert) - 1, Convert.ToInt32(hori) - 1);
                } while (!game.CheckMove(player, move));

                model = game.PlayMove(player, move);
                player = game.Turn;

                models.Add(model);

                ShowBoard(game.Board);

                Console.WriteLine();
            } while (model.Winnner == default);

            char result = model.Winnner;

            if (result != default)
                Console.WriteLine(result + " wins! " + game.MovesLeft + " moves left.");
            else
                Console.WriteLine("No one wins.");

            Console.WriteLine("\nSaving game to file...\n");

            TSVRecorder tsvRecorder = new TSVRecorder();
            tsvRecorder.SaveGameToCSV(models.ToArray());

            Console.WriteLine("Game saved.");
        }

        private static void PlayRandomGames(int numberOfGames)
        {
            char startPlayer = 'x';
            Random random = new Random();

            for (int i = 0; i < numberOfGames; i++)
            {
                TTTGame game = new TTTGame();
                TTTModel model = new TTTModel();
                List<TTTModel> models = new List<TTTModel>();
                char player = startPlayer;

                do
                {
                    TTTCoord move;

                    do
                    {
                        move = new TTTCoord(random.Next(3), random.Next(3));
                    } while (!game.CheckMove(player, move));

                    model = game.PlayMove(player, move);
                    player = game.Turn;

                    models.Add(model);
                } while (model.Winnner == default && game.MovesLeft > 0);

                ShowBoard(game.Board);

                char result = model.Winnner;

                if (result != default)
                    Console.WriteLine(result + " wins! " + game.MovesLeft + " moves left.");
                else
                    Console.WriteLine("No one wins.");

                Console.WriteLine("\nSaving game to file...\n");

                TSVRecorder tsvRecorder = new TSVRecorder();
                tsvRecorder.SaveGameToCSV(models.ToArray());

                Console.WriteLine($"Game {i + 1} saved.");

                startPlayer = game.OppositePlayer(player);
            }
        }
    }
}