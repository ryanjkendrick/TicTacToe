using System;
using System.IO;

using TicTacToe.Game.DataStructures;

namespace TicTacToe.DataRecorder
{
    public class TSVRecorder
    {
        private const string FILENAME = "TTTTrainingData.tsv";

        public void SaveGameToCSV(TTTModel[] game)
        {
            string gameVal = ModelSerializer.SerializeGame(game);
            bool addHeader = !File.Exists(FILENAME);

            using (var file = File.AppendText(FILENAME))
            {
                if (addHeader)
                    file.WriteLine(ModelSerializer.GetTSVHeaders());

                file.WriteLine(gameVal);
            }
        }
    }
}
