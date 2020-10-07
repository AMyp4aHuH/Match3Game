using Match3Game.Providers;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements.DefaultTiles
{
    public class GreenTile : Tile
    {
        public GreenTile() : base()
        {
            SetForeground(TextureProvider.GreenDefaultTexture, new Point(4, 1), Point.Zero);
        }
    }
}
