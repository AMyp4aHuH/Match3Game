using Match3Game.Providers;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements.DefaultTiles
{
    public class YellowTile : Tile
    {
        public YellowTile() : base()
        {
            SetForeground(TextureProvider.YellowDefaultTexture, new Point(4, 1), Point.Zero);
        }
    }
}
