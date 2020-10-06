using Microsoft.Xna.Framework;

namespace Match3Game.Matrix
{
    public class BlackDestroyer : Destroyer
    {
        public BlackDestroyer()
        {
            SetForeground(TextureManager.DestroyerTexture, new Point(4, 1), new Point(3, 0));
        }
    }
}
