﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            TrainingModel[] trainingModels = moves.Select(x => new TrainingModel(x)).ToArray();
            StringBuilder sb = new StringBuilder();

            foreach (var trainingModel in trainingModels)
            {
                sb.AppendLine($"{trainingModel.MoveX}    {trainingModel.MoveY}   {string.Join(",", trainingModel.Board)} {trainingModel.Winnner}");
            }

            return sb.ToString();
        }
    }
}
