using ChessSystem;
namespace Chess
{

    class Bishop : Piece
    {

        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.Piece(pos);
            return p == null || p.color != color;
        }


        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[board.Line, board.Column];
            Position position = new Position(0, 0);

            //NE
            position.DefineValue(position.Line - 1, position.Column + 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //southeast
            position.DefineValue(position.Line + 1, position.Column + 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //southwest
            position.DefineValue(position.Line + 1, position.Column - 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //northwest
            position.DefineValue(position.Line - 1, position.Column - 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            return matrix;
        }

    }
}