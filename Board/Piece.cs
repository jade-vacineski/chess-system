namespace ChessSystem
{
    class Piece
    {
        public Position Position { get; set; }
        public Color color { get; protected set; }
        public int NumberOfMovements { get; set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            NumberOfMovements = 0;
            this.board = board;
            this.color = color;
        }

        public void IncreaseNumberOfMoves()
        {
            NumberOfMovements++;
        }

    }
}