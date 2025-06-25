using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            //upward direction
            var position = board.FindPiece(this);
            List<Square> squares = new List<Square>();

            squares.Add(Square.At(position.Row + 2, position.Col + 1));
            squares.Add(Square.At(position.Row + 2, position.Col - 1));
            squares.Add(Square.At(position.Row + 1, position.Col + 2));
            squares.Add(Square.At(position.Row + 1, position.Col - 2));
            squares.Add(Square.At(position.Row - 2, position.Col + 1));
            squares.Add(Square.At(position.Row - 2, position.Col - 1));
            squares.Add(Square.At(position.Row - 1, position.Col + 2));
            squares.Add(Square.At(position.Row - 1, position.Col - 2));

            foreach (Square item in squares)
            {
                if (!board.CheckWithinBounds(item))
                {
                    squares.Remove(item);
                }
            }


            return squares;
        }
    }
}