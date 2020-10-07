using Match3Game.Providers;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements.DefaultTiles
{
    public class BlueTile : Tile
    {
        public BlueTile() : base()
        {
            SetForeground(TextureProvider.BlueDefaultTexture, new Point(4,1), Point.Zero);
        }
    }
}
