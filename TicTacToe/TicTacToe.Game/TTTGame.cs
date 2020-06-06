using TicTacToe.Game.DataStructures;

namespace TicTacToe.Game
{
    public class TTTGame
    {
        public int MovesLeft { get; private set; }
        public char Turn { get; private set; }
        public char[,] Board { get; private set; }

        public double[] BoardAsDoubleArray
        {
            get
            {
                int i = 0;
                double[] oneDim = new double[9];

                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        char val = Board[x, y];
                        double numVal = 0;

                        if (val == 'x')
                            numVal = 1;
                        else if (val == 'o')
                            numVal = -1;
                        else
                            numVal = 0.01;

                        oneDim[i++] = numVal;
                    }
                }

                return oneDim;
            }
        }

        public TTTGame()
        {
            Board = new char[3, 3];

            MovesLeft = 9;
        }

        public bool CheckMove(char player, TTTCoord coord)
        {
            if (Turn != default && player != Turn)
                return false;

            return Board[coord.X, coord.Y] == default;
        }

        public TTTModel PlayMove(char player, TTTCoord coord)
        {
            if (Board[coord.X, coord.Y] == default)
            {
                Board[coord.X, coord.Y] = player;

                Turn = OppositePlayer(player);

                return new TTTModel
                {
                    Player = player,
                    MovesLeft = --MovesLeft,
                    Move = coord,
                    Board = BoardAsDoubleArray,
                    Winnner = this.MovesLeft < 5 ? CheckBoard() : default
                };
            }

            return null;
        }

        public char CheckBoard()
        {
            TTTCoord start;

            // Horizontals
            for (int x = 0; x < 3; x++) 
            {
                start = new TTTCoord(x, 0);

                if (CheckLine(start, new TTTCoord(x, 2)))
                    return Board[start.X, start.Y];
            }

            // Verticals
            for (int y = 0; y < 3; y++) 
            {
                start = new TTTCoord(0, y);

                if (CheckLine(start, new TTTCoord(2, y)))
                    return Board[start.X, start.Y];
            }

            // Diagonals
            if (CheckLine(new TTTCoord(0, 0), new TTTCoord(2, 2))) 
                return Board[0, 0];
            else if (CheckLine(new TTTCoord(2, 0), new TTTCoord(0, 2)))
                return Board[2, 0];

            return default;
        }
        
        public char OppositePlayer(char currentPlayer)
        {
            return currentPlayer == 'x' ? 'o' : 'x';
        }

        private bool CheckLine(TTTCoord startCoord, TTTCoord endCoord)
        {
            char player = Board[startCoord.X, startCoord.Y];

            if (player != default && player == Board[endCoord.X, endCoord.Y])
            {
                TTTCoord midCoord = new TTTCoord
                {
                    X = MidCoordinate(startCoord.X, endCoord.X),
                    Y = MidCoordinate(startCoord.Y, endCoord.Y)
                };

                return player == Board[midCoord.X, midCoord.Y];
            }

            return false;
        }

        private int MidCoordinate(int pointA, int pointB)
        {
            if (pointA == pointB)
                return pointA;

            int val = (pointA + pointB) / 2;

            return val < 0 ? 0 : val;
        }
    }
}
