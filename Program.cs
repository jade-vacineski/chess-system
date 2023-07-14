using ChessSystem;
using Chess;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var chessMatch = new ChessMatch();

                while (!chessMatch.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.board);

                    
                    Console.WriteLine("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();

                    bool[,] possiblePositions = chessMatch.board.Piece(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(chessMatch.board);

                    Console.WriteLine("Destiny: ");
                    Position destiny = Screen.ReadPosition().ToPosition();

                    chessMatch.PerformMovement(origin, destiny);

                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
