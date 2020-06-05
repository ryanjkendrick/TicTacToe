using System;
namespace TicTacToe.Game.DataStructures
{
    public class TTTModel
    {
        public char Player { get; set; }
        public int MovesLeft { get; set; }
        public TTTCoord Move { get; set; }
        public char[] Board { get; set; }
        public bool ValidMove { get; set; }
        public char Winnner { get; set; }

        public TTTModel()
        {
        }
    }
}
