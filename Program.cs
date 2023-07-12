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
                    Screen.PrintBoard(chessMatch.board);
                    Console.WriteLine("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();
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
