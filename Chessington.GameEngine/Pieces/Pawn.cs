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
            IEnumerable<Square> squares = new List<Square>();

            if (this.Player == Player.Black)
            {
                if (position.Row == 1)
                {
                    squares = squares.Append(new Square(position.Row + 2, position.Col));
                }
                squares = squares.Append(new Square(position.Row + 1, position.Col));
                
            }
            else
            {
                if (position.Row == 7)
                {
                    squares = squares.Append(new Square(position.Row - 2, position.Col));
                }
                squares = squares.Append(new Square(position.Row - 1, position.Col));
            }
            return squares;
        }
    }
}