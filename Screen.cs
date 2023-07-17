using Chess;
using ChessSystem;

namespace ChessSystemConsole
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine("Rotation: " + match.Rotation);
            Console.WriteLine("Waiting for move: " + match.CurrentPlayer);
            if (match.Check)
            {
                Console.WriteLine("Check!");
            }


        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White");
            PrintSet(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black");
            PrintSet(match.capturedPieces(Color.Black));

        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {

            for (int i = 0; i < board.Line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Column; j++)
                {
                    PrintPiece(board.Pieces(i, j));
                }
                System.Console.WriteLine();
            }
            Console.WriteLine(" a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Column; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.Pieces(i, j));
                }
                System.Console.WriteLine();
            }
            Console.WriteLine(" a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }


        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor asst = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = asst;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadPosition()
        {
            var s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }


    }
}