using ChessSystem;

namespace Chess
{
    class Pawn : Piece
    {

        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "P";
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
                if (board.ValidPosition(pos) && livre(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line - 2, Position.Column);
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if (board.ValidPosition(p2) && livre(p2) && board.ValidPosition(pos) && livre(pos) && NumberOfMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line - 1, Position.Column - 1);
                if (board.ValidatePosition(pos) && existeInimigo(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line - 1, posicao.coluna + 1);
                if (board.ValidPosition(pos) && existeInimigo(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 3)
                {
                    var left = new Position(pos.Line, Position.Column - 1);
                    if (board.ValidPosition(left) && existeInimigo(left) && board.Piece(left) == ChessMatch.vulneravelEnPassant)
                    {
                        mat[left.Line - 1, left.Column - 1] = true;
                    }
                    var right = new Position(Position.Line, Position.Column + 1);
                    if (board.ValidPosition(right) && existeInimigo(right) && board.Piece(right) == ChessMatch.vulneravelEnPassant)
                    {
                        mat[right.Line - 1, right.Column - 1] = true;
                    }
                }
            }
            else
            {
                pos.DefineValue(Position.Line + 1, Position.Column);
                if (board.ValidPosition(pos) && livre(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line + 2, Position.Column);
                var p2 = new Position(Position.Line + 1, Position.Column);
                if (board.ValidPosition(p2) && livre(p2) && board.ValidPosition(pos) && livre(pos) && NumberOfMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line + 1, Position.Column - 1);
                if (board.ValidatePosition(pos) && existeInimigo(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValue(Position.Line + 1, Position.Column + 1);
                if (board.ValidPosition(pos) && existeInimigo(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 4)
                {
                    var left = new Position(Position.Line, Position.Column - 1);
                    if (board.ValidPosition(left) && existeInimigo(left) && board.Piece(left) == ChessMatch.vulneravelEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    var right = new Position(Position.Line, Position.Column + 1);
                    if (board.ValidatePosition(right) && existeInimigo(right) && board.Piece(right) == ChessMatch.vulneravelEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}