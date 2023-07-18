using ChessSystem;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch Match;
        public King(Board board, Color color, ChessMatch chessMatch) : base(board, color)
        {
            this.Match = chessMatch;
        }

        

        public override string ToString()
        {
            return "k";
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
            var position = new Position(0, 0);

            //above
            position.DefineValue(position.Line - 1, position.Column);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //NE
            position.DefineValue(position.Line - 1, position.Column + 1);
            if (board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Line, position.Column] = true;
            }

            //right
            position.DefineValue(position.Line, position.Column + 1);
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

            //down
            position.DefineValue(position.Line + 1, position.Column);
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

            //left
            position.DefineValue(position.Line, position.Column - 1);
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

            //special move 
            if (NumberOfMovements == 0 && !Match.Check)
            {
                var rookPosition1 = new Position(position.Line, position.Column + 3);
                if (RookTestForRock(rookPosition1))
                {
                    var position1 = new Position(position.Line, position.Column + 1);
                    var position2 = new Position(position.Line, position.Column + 2);

                    if (board.Piece(position1) == null && board.Piece(position2) == null)
                    {
                        matrix[position.Line, position.Column + 2] = true;
                    }
                }
                //special move
                var rookPosition2 = new Position(position.Line, position.Column - 4);
                if (RookTestForRock(rookPosition2))
                {
                    var position1 = new Position(position.Line, position.Column - 1);
                    var position2 = new Position(position.Line, position.Column - 2);
                    var position3 = new Position(position.Line, position.Column - 3);


                    if (board.Piece(position1) == null && board.Piece(position2) == null && board.Piece(position3) == null)
                    {
                        matrix[position.Line, position.Column - 2] = true;
                    }
                }
            }

            return matrix;
        }
    }
}