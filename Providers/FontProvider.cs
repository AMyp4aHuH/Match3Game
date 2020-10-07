using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Providers
{
    public static class FontProvider
    {
        private static ContentManager content;
        public static ContentManager Content { set => content = value; }

        private static SpriteFont defaultFont;
        public static SpriteFont DefaultFont =>
            defaultFont is null ? defaultFont = content.Load<SpriteFont>(@"Fonts/galleryFont") : defaultFont;
    }
}
