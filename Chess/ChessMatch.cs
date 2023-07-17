using System.Text.RegularExpressions;
using System.Xml;
using ChessSystem;

namespace Chess
{
    class ChessMatch
    {

        public Board Board { get; private set; }
        public int Rotation { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private readonly HashSet<Piece> _pieces;
        private readonly HashSet<Piece> _captured;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Rotation = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
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
        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> asst = new HashSet<Piece>();
            foreach (Piece x in _captured)
            {
                if (x.color == color)
                {
                    asst.Add(x);
                }
            }
            return asst;
        }

        public HashSet<Piece> piecesInPlay(Color color)
        {
            HashSet<Piece> asst = new HashSet<Piece>();
            foreach (Piece x in _pieces)
            {
                if (x.color == color)
                {
                    asst.Add(x);
                }
            }
            asst.ExceptWith(capturedPieces(color));
            return asst;
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

        private void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void InsertPiece()
        {
            InsertNewPiece('c', 1, new Rook(Board, Color.White));
            InsertNewPiece('c', 2, new Rook(Board, Color.White));
            InsertNewPiece('c', 3, new Rook(Board, Color.White));
            InsertNewPiece('d', 1, new Rook(Board, Color.White));
            InsertNewPiece('d', 2, new Rook(Board, Color.Black));
            InsertNewPiece('d', 3, new Rook(Board, Color.Black));
            InsertNewPiece('e', 1, new Rook(Board, Color.Black));
            InsertNewPiece('e', 2, new Rook(Board, Color.Black));


        }

    }
}