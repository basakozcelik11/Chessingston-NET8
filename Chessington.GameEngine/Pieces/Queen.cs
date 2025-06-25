using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);
            IEnumerable<Square> squares = MoveDiagonally(position);
            squares = squares.Concat(MoveLaterally(position));

            return squares;
        }
    }
}