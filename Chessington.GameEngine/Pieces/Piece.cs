using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }
        public List<(int,int)> DiagonalDirection = [
                (1, 1),
                (1, -1),
                (-1, 1),
                (-1, -1)
            ];
            
        public List<(int,int)> LateralDirection = [
                (1, 0),
                (0, 1),
                (-1, 0),
                (0, -1)
            ];

        public List<(int, int)> KnightDirection = [
            (2, 1),
            (2, -1),
            (-2, 1),
            (-2, -1),
            (1, 2),
            (1, -2),
            (-1, 2),
            (-1, -2)
        ];


        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public List<Square> AvailableMoves(List<(int, int)> directions, Square position, Board board, int? upperBound = GameSettings.BoardSize, bool isPawn = false)
        {
            List<Square> squares = [];
            foreach ((int, int) direction in directions)
            {
                var dx = direction.Item1;
                var dy = direction.Item2;
                var nextPosition = Square.At(position.Row + dx, position.Col + dy);
                var upperBoundTemp = upperBound;
                while (board.CheckWithinBounds(nextPosition) && upperBoundTemp > 0)
                {
                    var piece = board.GetPiece(nextPosition);
                    if (piece != null)
                    {
                        if (piece.Player != Player && !isPawn)
                        {
                            squares.Add(nextPosition);
                        }
                        break;
                    }
                    squares.Add(nextPosition);
                    nextPosition = Square.At(nextPosition.Row + dx, nextPosition.Col + dy);
                    upperBoundTemp--;
                }
            }
            return squares;
        }

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }
    }
}