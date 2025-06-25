using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);
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
    }
}