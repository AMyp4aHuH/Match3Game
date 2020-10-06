using Microsoft.Xna.Framework;

namespace Match3Game.Matrix
{
    public class BlueTile : Tile
    {
        public BlueTile() : base()
        {
            SetForeground(TextureManager.BlueDefaultTexture, new Point(4,1), Point.Zero);
        }
    }
}
