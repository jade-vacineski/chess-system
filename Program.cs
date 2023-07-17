using System.Net;
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
                var chessMatch = new ChessMatch();
                try
                {
                    while (!chessMatch.Finished)
                    {

                        Console.Clear();
                        Screen.PrintMatch(chessMatch);

                        Console.WriteLine("Origin: ");
                        Position origin = Screen.ReadPosition().ToPosition();
                        chessMatch.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board, possiblePositions);

                        Console.WriteLine();
                        Console.WriteLine("Destiny: ");
                        Position destiny = Screen.ReadPosition().ToPosition();

                        chessMatch.PerformMovement(origin, destiny);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}




