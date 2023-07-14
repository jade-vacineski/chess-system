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

                while (!chessMatch.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);
                    Console.WriteLine();
                    System.Console.WriteLine("Rotation: " + chessMatch.Rotation);
                    System.Console.WriteLine("Waiting for move: " + chessMatch.CurrentPlayer);

                    Console.WriteLine("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();
                    chessMatch.ValidateOriginPosition(origin);

                    bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);

                    Console.WriteLine("Destiny: ");
                    Position destiny = Screen.ReadPosition().ToPosition();

                    chessMatch.PerformMovement(origin, destiny);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}




