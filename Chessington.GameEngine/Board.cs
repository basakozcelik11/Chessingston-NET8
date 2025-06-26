using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece?[,] board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; }
        
        public Square[] KingLocations { get; private set; }

        public bool IsWhiteKingChecked { get; private set; }
        public bool IsBlackKingChecked { get; private set; }
        public bool CheckMate { get; private set; }


        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,]? boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize];
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
            KingLocations = [Square.At(7, 4), Square.At(0, 4)];
            IsBlackKingChecked = false;
            IsWhiteKingChecked = false;
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }

        public Piece? GetPiece(Square square)
        {
            return board[square.Row, square.Col];
        }

        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]!);
            }

            //Move the piece and set the 'from' square to be empty.
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;

            if (movingPiece is King)
            {
                KingLocations[(int)CurrentPlayer] = to;
            } 
            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
           
            OnCurrentPlayerChanged(CurrentPlayer);           
        }

        public void MovePiece(Square from, Square to, Piece[,] copyBoard)
        {
            CheckMate = false;
            var movingPiece = copyBoard[from.Row, from.Col];
            if (movingPiece == null) { return; }
    
            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (copyBoard[to.Row, to.Col] != null)
            {
                OnPieceCaptured(copyBoard[to.Row, to.Col]!);
            }

            //Move the piece and set the 'from' square to be empty.
            copyBoard[to.Row, to.Col] = copyBoard[from.Row, from.Col];
            copyBoard[from.Row, from.Col] = null;

            if (movingPiece is King)
            {
                KingLocations[(int)CurrentPlayer] = to;
            } 
            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
        }

        private bool IsCheckMate(Player player, Piece?[,] board)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
            {
                for (var col = 0; col < GameSettings.BoardSize; col++)
                {
                    Piece? currentPiece = board[row, col];

                    if (currentPiece != null && currentPiece.Player == player)
                    {
                        IEnumerable<Square> nextAvailableMoves = currentPiece.GetAvailableMoves(this);
                        foreach (Square move in nextAvailableMoves)
                        {
                            Piece[,] copyBoard = (Piece[,])board.Clone();
                            var storeKingLocation = KingLocations[(int)player];
                            MovePiece(FindPiece(currentPiece), move, copyBoard);
                            if (!IsKingChecked(player, copyBoard))
                            {
                                return false;
                            }
                            Console.WriteLine("CheckMate" + KingLocations[(int)player]);
                            KingLocations[(int)player] = storeKingLocation;
                            Console.WriteLine("CheckMate Restored" + KingLocations[(int)player]);
                        }
                    }
                }
            }
            return true;
        }
        
        public bool IsCheckMate(Player player)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
            {
                for (var col = 0; col < GameSettings.BoardSize; col++)
                {
                    Piece? currentPiece = board[row, col];

                    if (currentPiece != null && currentPiece.Player == player)
                    {
                        IEnumerable<Square> nextAvailableMoves = currentPiece.GetAvailableMoves(this);
                        foreach (Square move in nextAvailableMoves)
                        {
                            Piece[,] copyBoard = (Piece[,])board.Clone();
                            var storeKingLocation = KingLocations[(int)player];
                            MovePiece(Square.At(row, col), move, copyBoard);
                            if (!IsKingChecked(player, copyBoard))
                            {
                                return false;
                            }
                            KingLocations[(int)player] = storeKingLocation;                           
                        }
                    }
                }
            }
            return true;
        }

        private bool IsKingChecked(Player player, Piece[,] board)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
            {
                for (var col = 0; col < GameSettings.BoardSize; col++)
                {
                    Piece? currentPiece = board[row, col];

                    if (currentPiece != null && currentPiece.Player != player)
                    {
                        IEnumerable<Square> nextAvailableMoves = currentPiece.GetAvailableMoves(new Board(player,board));
                        Console.WriteLine("Check"+KingLocations[(int)player]);
                        if (nextAvailableMoves.Any(move => move.Equals(KingLocations[(int)player])))
                        {
                            return true;

                        }
                    }
                }
            }
            return false;
        }

        public bool IsKingChecked(Player player)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
            {
                for (var col = 0; col < GameSettings.BoardSize; col++)
                {
                    Piece? currentPiece = board[row, col];

                    if (currentPiece != null && currentPiece.Player != player)
                    {
                        IEnumerable<Square> nextAvailableMoves = currentPiece.GetAvailableMoves(this); ;
                        if (nextAvailableMoves.Any(move => move.Equals(KingLocations[(int)player])))
                        {
                            return true;

                        }
                    }
                }
            }
            return false;
        }

        public bool CheckWithinBounds(Square position)
        {
            if (0 <= position.Col && position.Col < GameSettings.BoardSize)
            {
                if (0 <= position.Row && position.Row < GameSettings.BoardSize)
                {
                    return true;
                }
            }
            return false;
        }

        public delegate void PieceCapturedEventHandler(Piece piece);

        public event PieceCapturedEventHandler? PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler? CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }

        
    }
}
