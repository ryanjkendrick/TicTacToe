using System;
using System.Collections.Generic;
using TicTacToe.DataRecorder;
using TicTacToe.Game;
using TicTacToe.Game.DataStructures;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");

            Console.WriteLine("\nWhat would you like to do?\n");

            Console.WriteLine("1. Play the game");
            Console.WriteLine("2. Quit");

            Console.WriteLine();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    PlayGame();
                    break;
                case "2":
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
                Console.WriteLine("Vertical coordinate:");
                string vert = Console.ReadLine();

                Console.WriteLine("Horizontal coordinate:");
                string hori = Console.ReadLine();

                model = game.PlayMove(player, new TTTCoord(Convert.ToInt32(vert) - 1, Convert.ToInt32(hori) - 1));
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
    }
}
