using ChessSystem;

namespace ChessSystemConsole
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {

            for (int i = 0; i < board.Line; i++)
            {
                for (int j = 0; j < board.Column; j++)
                {

                    if (board.ValidPosition == null)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(board.Piece(i, j) + " ");
                    }
                }
            }

        }

    }
}