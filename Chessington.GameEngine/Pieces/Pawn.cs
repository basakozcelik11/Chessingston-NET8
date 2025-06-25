using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);
            List<Square> squares = new List<Square>();

            if (this.Player == Player.Black)
            {
                if (position.Row == 1)
                {
                    squares.Add(Square.At(position.Row + 2, position.Col));
                }
                squares.Add(Square.At(position.Row + 1, position.Col));
                
            }
            else
            {
                if (position.Row == 6)
                {
                    squares.Add(Square.At(position.Row - 2, position.Col));
                }
                squares.Add(Square.At(position.Row - 1, position.Col));
            }
            return squares;
        }
    }
}