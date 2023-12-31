using ChessSystem;

namespace Chess
{
    class Knight : Piece
    {

        public Knight(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.Piece(pos);
            return p == null || p.color != color;
        }

        private bool RookTestForRock(Position pos)
        {
            Piece p = board.Piece(pos);
            return p != null && p is Rook && p.color == color && p.NumberOfMovements == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[board.Line, board.Column];
            Position position = new Position(0, 0);

            //above
            position.DefineValue(position.Line - 1, position.Column - 2);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //NE
            position.DefineValue(position.Line - 2, position.Column - 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //right
            position.DefineValue(position.Line - 2, position.Column + 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //southeast
            position.DefineValue(position.Line - 1, position.Column + 2);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //down
            position.DefineValue(position.Line + 1, position.Column + 2);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //southwest
            position.DefineValue(position.Line + 2, position.Column + 2);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //left
            position.DefineValue(position.Line + 2, position.Column - 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //northwest
            position.DefineValue(position.Line + 1, position.Column - 2);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            return matrix;
        }
    }
}