using Match3Game.Providers;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements.DefaultTiles
{
    public class PurpleTile : Tile
    {
        public PurpleTile() : base()
        {
            SetForeground(TextureProvider.PurpleDefaultTexture, new Point(4, 1), Point.Zero);
        }

    }
}
