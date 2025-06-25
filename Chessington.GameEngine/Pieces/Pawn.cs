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
                var newPosition = Square.At(position.Row + 1, position.Col);
                if (!board.CheckBlocked(Player, newPosition))
                {
                    squares.Add(newPosition);
                }
                if (position.Row == 1)
                {
                    var newTempPosition = Square.At(position.Row + 2, position.Col);
                    if (!board.CheckBlocked(Player, newTempPosition) && !board.CheckBlocked(Player, newPosition))
                    {
                        squares.Add(newTempPosition);
                    }
                }
            
                
            }
            else
            {
                var newPosition = Square.At(position.Row - 1, position.Col);
                if (!board.CheckBlocked(Player, newPosition)){
                    squares.Add(newPosition);
                }
                if (position.Row == 6)
                {
                    var newTempPosition = Square.At(position.Row - 2, position.Col);
                    if (!board.CheckBlocked(Player, newTempPosition) && !board.CheckBlocked(Player, newPosition))
                    {
                        squares.Add(newTempPosition);
                    }
                }
            
            }

            return squares;
        }
    }
}