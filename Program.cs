using ChessSystem;
using Chess;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);
                board.InsertPiece(new Rook(board, Color.Black), new Position(0, 2));
                board.InsertPiece(new Rook(board, Color.Black), new Position(1, 3));
                board.InsertPiece(new King(board, Color.Black), new Position(2, 5));

                Screen.PrintBoard(board);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
