using ChessSystem;

namespace Chess
{
    class ChessMatch
    {

        public Board board { get; private set; }
        private int rotation;
        private Color currentPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            rotation = 1;
            currentPlayer = Color.White;
            InsertPiece();
        }

        public void PerformMovement(Position origin, Position destiny)
        {
            Piece p = board.RemovePiece(origin);
            p.IncreaseNumberOfMoves();
            Piece capturedPiece = board.RemovePiece(destiny);
            board.InsertPiece(p, destiny);

        }

        private void InsertPiece()
        {

            board.InsertPiece(new Rook(board, Color.Black), new ChessPosition('c', 1).ToPosition());
            board.InsertPiece(new Rook(board, Color.Black), new ChessPosition('c', 2).ToPosition());
            board.InsertPiece(new Rook(board, Color.Black), new ChessPosition('c', 3).ToPosition());

        }
    }
}