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
            List<Square> squares = AvailableMoves(KnightDirection, position, board, 1);
            return squares;
        }
    }
}