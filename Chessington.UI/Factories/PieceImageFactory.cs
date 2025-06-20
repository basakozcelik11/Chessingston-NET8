using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Chessington.GameEngine.Pieces;

namespace Chessington.UI.Factories
{
    /// <summary>
    /// Given a piece, returns in image for that piece. Change things here if you want new icons.
    /// </summary>
    public static class PieceImageFactory
    {
        private static readonly Dictionary<Type, string> PieceSuffixes = new Dictionary<Type, string>
        {
            { typeof(Pawn), "P" },
            { typeof(Rook), "R" },
            { typeof(Knight), "N" },
            { typeof(Bishop), "B" },
            { typeof(King), "K" },
            { typeof(Queen), "Q" },
        };

        public static BitmapImage GetImage(Piece piece)
        {
            // Ensure player name is title case ("Black" or "White")
            var playerName = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(piece.Player.ToString().ToLowerInvariant());
            // Suffix is already uppercase from dictionary
            var suffix = PieceSuffixes[piece.GetType()];
            // Build robust URI
            var uri = $"{InterfaceSettings.IconRoot}{playerName} {suffix}.ico";
            return new BitmapImage(new Uri(uri));
        }
    }
}