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

        public void RemoveEmptyLines()
        {
            var tempFileName = Path.GetTempFileName();

            try
            {
                using (var streamReader = new StreamReader(FILENAME))
                using (var streamWriter = new StreamWriter(tempFileName))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            streamWriter.WriteLine(line);
                    }
                }

                File.Copy(tempFileName, FILENAME, true);
            }
            finally
            {
                File.Delete(tempFileName);
            }
        }
    }
}
