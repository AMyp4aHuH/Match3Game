using Match3Game.MatrixElements.Destroyers;
using Match3Game.Providers;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements
{
    public class BlackDestroyer : Destroyer
    {
        public BlackDestroyer()
        {
            SetForeground(TextureProvider.DestroyerTexture, new Point(4, 1), new Point(3, 0));
        }
    }
}
