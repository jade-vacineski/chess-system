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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Rotation = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            InsertPiece();
        }

        public Piece PerformMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumberOfMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);

            if (capturedPiece != null)
            {
                _captured.Add(capturedPiece);
            }
            //special move roque pequeno
            if (p is King && destiny.Column == origin.Column + 2)
            {
                var rookOrigin = new Position(origin.Line, origin.Column + 3);
                var rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(rookOrigin);
                R.IncreaseNumberOfMoves();
                Board.InsertPiece(R, rookDestiny);
            }
            //roque grande
            if (p is King && destiny.Column == origin.Column - 2)
            {
                var rookOrigin = new Position(origin.Line, origin.Column - 4);
                var rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(rookOrigin);
                R.IncreaseNumberOfMoves();
                Board.InsertPiece(R, rookDestiny);
            }
            //en passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position pawnPosition;
                    if (p.color == Color.White)
                    {
                        pawnPosition = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(destiny.Line - 1, destiny.Column);
                    }

                    capturedPiece = Board.RemovePiece(pawnPosition);
                    _captured.Add(capturedPiece);

                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.DecrementNumberOfMoves();

            if (capturedPiece != null)
            {
                Board.InsertPiece(capturedPiece, destiny);
                _captured.Remove(capturedPiece);
            }
            //roque p
            if (p is King && destiny.Column == origin.Column + 2)
            {
                var rookOrigin = new Position(origin.Line, origin.Column + 3);
                var rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(rookOrigin);
                R.DecrementNumberOfMoves();
                Board.InsertPiece(R, rookOrigin);
            }
            //roque g 
            if (p is King && destiny.Column == origin.Column - 2)
            {
                var rookOrigin = new Position(origin.Line, origin.Column - 4);
                var rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(rookDestiny);
                R.DecrementNumberOfMoves();
                Board.InsertPiece(R, rookOrigin);
            }
            //en passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position pawnPosition;
                    if (pawn.color != Color.White)
                    {
                        pawnPosition = new Position(3, destiny.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destiny.Column);
                    }
                    Board.InsertPiece(pawn, pawnPosition);
                }
            }

            Board.InsertPiece(p, origin);
        }

        public void PerformsMove(Position origin, Position destiny)
        {
            Piece capturedPiece = PerformMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("you can't put yourself in check! ");
            }

            Piece p = Board.Piece(destiny);

            //special move promotion
            if (p is Pawn)
            {
                if (p.color == Color.White && destiny.Line == 0 || (p.color == Color.Black && destiny.Line == 7))
                {
                    _pieces.Remove(p);
                    Piece queen = new Queen(Board, p.color);
                    Board.InsertPiece(queen, destiny);
                    _pieces.Add(queen);
                }

            }

            if (IsInCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            }

            else
            {
                Check = false;
            }
            if (Checkmate(Adversary(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Rotation++;
                ChangePlayer();
            }


            //special move en passant
            if (p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }

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

        public void ValidateOriginPosition(Position pos, Position destiny)
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

        private Color Adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        private Piece King(Color color)
        {
            foreach (Piece x in piecesInPlay(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }
        public bool IsInCheck(Color color)
        {
            Piece k = King(color);
            foreach (Piece x in piecesInPlay(Adversary(color)))
            {
                bool[,] mat = x.PossibleMoves();
                if (mat[k.Position.Line, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool Checkmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in piecesInPlay(color))
            {
                bool[,] mat = x.PossibleMoves();

                for (int i = 0; i < Board.Line; i++)
                {
                    for (int j = 0; j < Board.Column; j++)
                    {
                        if (mat[i, j])
                        {
                            var destiny = new Position(i, j);
                            Position origin = x.Position;
                            Piece capturedPiece = PerformMovement(origin, destiny);
                            bool checkmate = IsInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!checkmate)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void InsertPiece()
        {
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Knight(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));

            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

        internal void ValidateOriginPosition(Position origin)
        {
            throw new NotImplementedException();
        }
    }
}