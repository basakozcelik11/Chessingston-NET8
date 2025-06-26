using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        private List<Square> CheckOpponents(Board board, Square position, List<(int, int)> directions)
        {
            List<Square> squares = new List<Square>();
            foreach ((int, int) direction in directions)
            {
                var dx = direction.Item1;
                var dy = direction.Item2;
                var nextPosition = Square.At(position.Row + dx, position.Col + dy);
                if (board.CheckWithinBounds(nextPosition))
                {
                    var piece = board.GetPiece(nextPosition);
                    if (piece != null && piece.Player != Player)
                    {
                        squares.Add(nextPosition);
                    }
                }
            }
            return squares;
            
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);
            IEnumerable<Square> squares = [];

            if (this.Player == Player.Black)
            {
                squares = AvailableMoves([(1, 0)], position, board, position.Row == 1 ? 2 : 1, true);
                squares = squares.Concat(CheckOpponents(board, position, [(1,1), (1,-1)]));

            }
            else
            {
                squares = AvailableMoves([(-1, 0)], position, board, position.Row == 6 ? 2 : 1, true);
                squares = squares.Concat(CheckOpponents(board, position, [(-1,1), (-1,-1)]));
            }

            return squares;
        }
    }
}