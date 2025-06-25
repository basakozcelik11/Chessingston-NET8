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

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public List<Square> MoveDiagonally(Square position)
        {
            List<Square> squares = new List<Square>();
            int j = 1;

            //forward direction
            for (int i = position.Col + 1; i < 8; i++)
            {
                if (position.Row + j < 8)
                {
                    squares.Add(Square.At(position.Row + j, i));
                }
                if (position.Row - j >= 0)
                {
                    squares.Add(Square.At(position.Row - j, i));
                }
                j++;
            }

            //backward direction
            j = 1;
            for (int i = position.Col - 1; i >= 0; i--)
            {
                if (position.Row + j < 8)
                {
                    squares.Add(Square.At(position.Row + j, i));
                }
                if (position.Row - j >= 0)
                {
                    squares.Add(Square.At(position.Row - j, i));
                }
                j++;
            }
            return squares;
        }

        public List<Square> MoveLaterally(Square position)
        {
            List<Square> squares = new List<Square>();
            for (var i = 0; i < 8; i++)
                squares.Add(Square.At(position.Row, i));

            for (var i = 0; i < 8; i++)
                squares.Add(Square.At(i, position.Col));

            squares.RemoveAll(s => s == position);
            return squares;
        }

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }
    }
}