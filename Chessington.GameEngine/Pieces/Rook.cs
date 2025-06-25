using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);
            List<Square> squares = new List<Square>();

            for (var i = 0; i < 8; i++)
                squares.Add(Square.At(position.Row, i));

            for (var i = 0; i < 8; i++)
                squares.Add(Square.At(i, position.Col));

            squares.RemoveAll(s => s == position);

            return squares;
        }
    }
}