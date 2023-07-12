namespace ChessSystem
{
    class Board
    {
        public int Line { get; set; }
        public int Column { get; set; }
        private readonly Piece[,] _piece;

        public Board(int line, int column)
        {
            _piece = new Piece[line, column];
            Line = line;
            Column = column;
        }

        public Piece Pieces(int line, int column)
        {
            return _piece[line, column];
        }

        public Piece Piece(Position position)
        {
            return _piece[position.Line, position.Column];
        }

        public bool ThereIspiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void InsertPiece(Piece pieces, Position position)
        {
            if (ThereIspiece(position))
            {
                throw new BoardException("there is already a piece in that position");
            }
            _piece[position.Line, position.Column] = pieces;
            pieces.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }

            Piece asst = Piece(position);
            asst.Position = null;
            _piece[position.Line, position.Column] = null;
            return asst;

        }

        public bool ValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Line || position.Column < 0 || position.Column >= Column)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid Position!");
            }
        }

    }
}