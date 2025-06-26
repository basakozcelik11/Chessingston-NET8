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
                squares = AvailableMoves([(1, 0)], position, board, position.Row == 1? 2 : 1, true);  
            
            }
            else
            {
                squares = AvailableMoves([(-1, 0)], position, board, position.Row == 6? 2 : 1, true);         
            }
            return squares;
        }
    }
}