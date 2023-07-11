using ChessSystem;

namespace ChessSystemConsole.Chess
{
    class King : Piece
    {

        public King(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "k";
        }
        
    }
}