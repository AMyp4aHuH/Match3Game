using Microsoft.Xna.Framework;

namespace Match3Game.Matrix
{
    public class PinkTile : Tile
    {
        public PinkTile() : base()
        {
            SetForeground(TextureManager.PinkDefaultTexture, new Point(4, 1), Point.Zero);
        }
    }
}
