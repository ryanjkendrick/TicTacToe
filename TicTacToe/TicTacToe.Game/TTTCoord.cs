namespace TicTacToe.Game
{
    public class TTTCoord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TTTCoord()
        {
        }

        public TTTCoord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int ToInt()
        {
            return (Y * 3) + X + 1;
        }
    }
}
