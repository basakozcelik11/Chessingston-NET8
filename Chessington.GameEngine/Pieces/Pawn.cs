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
            Console.WriteLine(position);
            if (this.Player == Player.Black)
            {
                var newLocation = new Square(position.Row + 1, position.Col);
                IEnumerable<Square> squares = new Square[] { newLocation };
                return squares;
            }
            else
            {
                var newLocation = new Square(position.Row - 1, position.Col);
                IEnumerable<Square> squares = new Square[] { newLocation };
                return squares;
            }
        }
    }
}