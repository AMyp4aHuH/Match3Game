using Microsoft.Xna.Framework;

namespace Match3Game.Matrix
{
    public class PurpleTile : Tile
    {
        public PurpleTile() : base()
        {
            SetForeground(TextureManager.PurpleDefaultTexture, new Point(4, 1), Point.Zero);
        }

    }
}
