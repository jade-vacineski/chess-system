using ChessSystem;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(8, 8);

            Screen.PrintBoard(board);
            
            System.Console.WriteLine();

        }
    }
}
