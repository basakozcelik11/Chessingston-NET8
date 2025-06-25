using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);
            List<Square> squares = new List<Square>();
            for (int i = position.Col - 1; i <= position.Col + 1; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var nextSquare = Square.At(position.Row + j, i);
                    if (board.CheckWithinBounds(nextSquare))
                    {
                        squares.Add(nextSquare);
                    }
                }              
            }
            squares.Remove(position);
            return squares;
        }
    }
}