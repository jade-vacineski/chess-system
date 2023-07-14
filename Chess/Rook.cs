using ChessSystem;

namespace Chess
{
    class Rook : Piece
    {

        public Rook(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "R";
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

            //above
            position.DefineValue(board.Line - 1, board.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;

                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.Line = Position.Line - 1;

            }
            //down
            position.DefineValue(board.Line + 1, board.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;

                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.Line = Position.Line + 1;

            }
            //right
            position.DefineValue(board.Line, board.Column + 1);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;

                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.Column = Position.Column + 1;

            }

            //left
            position.DefineValue(board.Line, board.Column - 1);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;

                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.Column = Position.Column - 1;

            }

            return matrix;
        }

    }
}