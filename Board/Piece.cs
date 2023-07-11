namespace Board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color color { get; protected set; }
        public int NumberOfMovements { get; set; }
        public Board board { get; protected set; }

        public Piece(Position position, Board board, Color color)
        {
            Position = position;
            NumberOfMovements = 0;
            this.board = board;
            this.color = color;

        }

        
    }
}