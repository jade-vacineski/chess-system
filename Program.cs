using ChessSystem;
using ChessSystem.Chess;
using ChessSystemConsole.Chess;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var board = new Board(8, 8);
                board.InsertPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.InsertPiece(new Rook(board, Color.Black), new Position(0, 2));
                board.InsertPiece(new King(board, Color.Black), new Position(2, 4));

                Screen.PrintBoard(board);
            }

            catch (BoardException e)
            {
                System.Console.WriteLine(e.Message);
            }

        }
    }
}
