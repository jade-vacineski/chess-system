using ChessSystem;
using Chess;


namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var chessPosition = new ChessPosition('c', 7);

            System.Console.WriteLine(chessPosition);

            System.Console.WriteLine(chessPosition.ToPosition());

        }
    }
}
