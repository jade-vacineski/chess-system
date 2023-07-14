using ChessSystem;

namespace Chess
{
    class ChessMatch
    {

        public Board Board { get; private set; }
        public int Rotation { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Rotation = 1;
            CurrentPlayer = Color.White;
            InsertPiece();
        }

        public void PerformMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumberOfMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);

        }

        public void PerformsMove(Position origin, Position destiny)
        {
            PerformMovement(origin, destiny);
            Rotation++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("there is no piece in the chosen origin position.");
            }
            if (CurrentPlayer != Board.Piece(pos).color)
            {
                throw new BoardException("The source piece chosen is not yours.");
            }
            if (!Board.Piece(pos).ThereArePossibleMoves())
            {
                throw new BoardException("There are no possible moves for the chosen piece.");
            }
        }

        public void ValidateDestinyPosition(Position origem, Position destiny)
        {
            if (!Board.Piece(origem).CanMoveTo(destiny))
            {
                throw new BoardException("Destination position is invalid.");
            }
        }

        public void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        private void InsertPiece()
        {

            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('c', 1).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('c', 2).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('c', 3).ToPosition());

        }

    }
}