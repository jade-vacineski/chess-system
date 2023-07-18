using ChessSystem;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ThereIsEnemy(Position pos)
        {
            Piece p = board.Piece(pos);
            return p != null && p.color != color;
        }

        private bool Free(Position pos)
        {
            return board.Piece(pos) == null;
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.Piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[board.Line, board.Column];

            var pos = new Position(0, 0);

            if (color == color)
            {
                pos.DefineValue(Position.Line - 1, Position.Column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line - 2, Position.Column);
                var p2 = new Position(Position.Line - 1, Position.Column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && NumberOfMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line - 1, Position.Column - 1);
                if (board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line - 1, Position.Column + 1);
                if (board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                if (Position.Line == 3)
                {
                    var left = new Position(pos.Line, Position.Column - 1);
                    if (board.ValidPosition(left) && ThereIsEnemy(left) && board.Piece(left) == match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column - 1] = true;
                    }
                    var right = new Position(Position.Line, Position.Column + 1);
                    if (board.ValidPosition(right) && ThereIsEnemy(right) && board.Piece(right) == match.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column - 1] = true;
                    }
                }
            }
            else
            {
                pos.DefineValue(Position.Line + 1, Position.Column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line + 2, Position.Column);
                var p2 = new Position(Position.Line + 1, Position.Column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && NumberOfMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line + 1, Position.Column - 1);
                if (board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line + 1, Position.Column + 1);
                if (board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 4)
                {
                    var left = new Position(Position.Line, Position.Column - 1);
                    if (board.ValidPosition(left) && ThereIsEnemy(left) && board.Piece(left) == match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    var right = new Position(Position.Line, Position.Column + 1);
                    if (board.ValidPosition(right) && ThereIsEnemy(right) && board.Piece(right) == match.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}