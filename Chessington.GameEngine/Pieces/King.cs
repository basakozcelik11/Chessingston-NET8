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
             IEnumerable<Square> squares = AvailableMoves(DiagonalDirection, position, board, 1);
            squares = squares.Concat(AvailableMoves(LateralDirection, position, board, 1));
            return squares;
        }
    }
}