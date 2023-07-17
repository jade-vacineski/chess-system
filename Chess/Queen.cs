using ChessSystem;

namespace Chess
{
    class Queen : Piece
    {

        public Queen(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "Q";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.Piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[board.Line, board.Column];
            var position = new Position(0, 0);

            //above*
            position.DefineValue(position.Line - 1, position.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line - 1, position.Column);
            }
            //NE *
            position.DefineValue(position.Line - 1, position.Column + 1);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line - 1, position.Column + 1);
            }

            //southeast *
            position.DefineValue(position.Line + 1, position.Column - 1);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line + 1, position.Column - 1);
            }

            //down *
            position.DefineValue(position.Line + 1, position.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line + 1, position.Column);
            }

            //southwest *
            position.DefineValue(position.Line + 1, position.Column + 1);
            position.DefineValue(position.Line - 1, position.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line + 1, position.Column + 1);
            }

            //left *
            position.DefineValue(position.Line, position.Column - 1);
            position.DefineValue(position.Line - 1, position.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line, position.Column - 1);
            }

            //right
            position.DefineValue(position.Line, position.Column + 1);
            position.DefineValue(position.Line - 1, position.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line, position.Column + 1);
            }

            //northwest *
            position.DefineValue(position.Line - 1, position.Column + 1);
            position.DefineValue(position.Line - 1, position.Column);
            while (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
                if (board.Piece(position) != null && board.Piece(position).color != color)
                {
                    break;
                }
                position.DefineValue(position.Line - 1, position.Column + 1);
            }

            return matrix;
        }
    }
}