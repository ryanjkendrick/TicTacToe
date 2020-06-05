using System.Linq;
using TicTacToe.Game.DataStructures;

namespace TicTacToe.DataRecorder.Models
{
    public class TrainingModel
    {
        public char FirstPlayer { get; set; }
        public int MovesLeft { get; set; }
        public string[] Moves { get; set; }
        public char Winnner { get; set; }

        public TrainingModel()
        {
        }

        public TrainingModel(TTTModel[] models)
        {
            FirstPlayer = models[0].Player;
            MovesLeft = models[models.Length - 1].MovesLeft;
            Moves = models.Select(x => $"{x.Move.X}{x.Move.Y}").ToArray();
            Winnner = models[models.Length - 1].Winnner;
        }
    }
}
