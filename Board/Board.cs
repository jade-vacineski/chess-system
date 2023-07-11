namespace ChessSystem
{
    class Board
    {
        public int Line { get; set; }
        public int Column { get; set; }
        private Piece[,] _piece;

        public Board(int line, int column)
        {
            _piece = new Piece[line, column];
            Line = line;
            Column = column;
        }
    }
}