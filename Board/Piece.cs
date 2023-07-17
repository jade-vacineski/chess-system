namespace ChessSystem
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color color { get; protected set; }
        public int NumberOfMovements { get; set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            NumberOfMovements = 0;
            this.board = board;
            this.color = color;
        }

        public void IncreaseNumberOfMoves()
        {
            NumberOfMovements++;
        }

        public void DecrementNumberOfMoves()
        {
            NumberOfMovements--;
        }


        public bool ThereArePossibleMoves()
        {
            bool[,] matrix = PossibleMoves();
            for (int i = 0; i < board.Line; i++)
            {
                for (int j = 0; j < board.Column; j++)
                {
                    if (matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoves()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMoves();

    }
}