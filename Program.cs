﻿using ChessSystem;
using Chess;
namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
             try {
                ChessMatch match = new ChessMatch();

                while (!match.Finished) {

                    try {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.PerformsMove(origin, destiny);
                    }
                    catch (BoardException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}





