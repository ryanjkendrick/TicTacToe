using System.Collections.Generic;
using System.Reflection;
using TicTacToe.DataRecorder.Models;
using TicTacToe.Game.DataStructures;

namespace TicTacToe.DataRecorder
{
    public static class ModelSerializer
    {
        public static string GetTSVHeaders()
        {
            TrainingModel trainingModel = new TrainingModel();
            List<string> headers = new List<string>();

            foreach (PropertyInfo prop in trainingModel.GetType().GetProperties())
            {
                headers.Add(prop.Name);
            }

            return string.Join("    ", headers);
        }

        public static string SerializeGame(TTTModel[] moves)
        {
            TrainingModel trainingModel = new TrainingModel(moves);

            return $"{trainingModel.FirstPlayer}    {trainingModel.MovesLeft}   {string.Join(",", trainingModel.Moves)} {trainingModel.Winnner}";
        }
    }
}
