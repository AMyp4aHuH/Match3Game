using Microsoft.Xna.Framework;

namespace Match3Game.Matrix
{
    public class YellowTile : Tile
    {
        public YellowTile() : base()
        {
            SetForeground(TextureManager.YellowDefaultTexture, new Point(4, 1), Point.Zero);
        }
    }
}
