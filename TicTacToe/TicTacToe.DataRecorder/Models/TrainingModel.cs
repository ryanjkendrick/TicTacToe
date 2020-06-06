using System.Linq;
using TicTacToe.Game.DataStructures;

namespace TicTacToe.DataRecorder.Models
{
    public class TrainingModel
    {
        public int MoveX { get; set; }
        public int MoveY { get; set; }
        public double[] Board { get; set; }
        public double Winnner { get; set; }

        public TrainingModel()
        {
        }

        public TrainingModel(TTTModel model)
        {
            MoveX = model.Move.X;
            MoveY = model.Move.Y;
            Board = model.Board;
            Winnner = ChangeCharPlayerToDouble(model.Winnner);
        }

        private double ChangeCharPlayerToDouble(char player)
        {
            double numVal = 0;

            if (player == 'x')
                numVal = 1;
            else if (player == 'o')
                numVal = -1;
            else
                numVal = 0.01;

            return numVal;
        }
    }
}
