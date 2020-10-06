using Microsoft.Xna.Framework;

namespace Match3Game.Matrix
{
    public class GreenTile : Tile
    {
        public GreenTile() : base()
        {
            SetForeground(TextureManager.GreenDefaultTexture, new Point(4, 1), Point.Zero);
        }
    }
}
