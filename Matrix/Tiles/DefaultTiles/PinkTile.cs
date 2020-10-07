using Match3Game.Providers;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements.DefaultTiles
{
    public class PinkTile : Tile
    {
        public PinkTile() : base()
        {
            SetForeground(TextureProvider.PinkDefaultTexture, new Point(4, 1), Point.Zero);
        }
    }
}
