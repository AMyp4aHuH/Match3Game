using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Providers
{
    public static class TextureProvider
    {
        public static ContentManager Content { private get; set; }

        #region DefaultTilesTextures

        private static Texture2D destroyerTileTexture;
        public static Texture2D DestroyerTexture => 
            destroyerTileTexture is null ? destroyerTileTexture = Content.Load<Texture2D>(@"Tiles/tileDestroyer") : destroyerTileTexture;

        private static Texture2D pinkDefaultTexture;
        public static Texture2D PinkDefaultTexture =>
            pinkDefaultTexture is null ? pinkDefaultTexture = Content.Load<Texture2D>(@"Tiles/pinkTile") : pinkDefaultTexture;

        private static Texture2D purpleDefaultTexture;
        public static Texture2D PurpleDefaultTexture =>
            purpleDefaultTexture is null ? purpleDefaultTexture = Content.Load<Texture2D>(@"Tiles/purpleTile") : purpleDefaultTexture;

        private static Texture2D greenDefaultTexture;
        public static Texture2D GreenDefaultTexture =>
            greenDefaultTexture is null ? greenDefaultTexture = Content.Load<Texture2D>(@"Tiles/greenTile") : greenDefaultTexture;

        private static Texture2D yellowDefaultTexture;
        public static Texture2D YellowDefaultTexture =>
            yellowDefaultTexture is null ? yellowDefaultTexture = Content.Load<Texture2D>(@"Tiles/yellowTile") : yellowDefaultTexture;

        private static Texture2D blueDefaultTexture;
        public static Texture2D BlueDefaultTexture =>
            blueDefaultTexture is null ? blueDefaultTexture = Content.Load<Texture2D>(@"Tiles/blueTile") : blueDefaultTexture;

        private static Texture2D verticaDefaultlLineTexture;
        public static Texture2D VerticaDefaultlLineTexture =>
            verticaDefaultlLineTexture is null ? verticaDefaultlLineTexture = Content.Load<Texture2D>(@"Tiles/verticalDetail") : verticaDefaultlLineTexture;

        private static Texture2D horizontalDefaultLineTexture;
        public static Texture2D HorizontalDefaultLineTexture =>
            horizontalDefaultLineTexture is null ? horizontalDefaultLineTexture = Content.Load<Texture2D>(@"Tiles/horizontalDetail") : horizontalDefaultLineTexture;

        private static Texture2D bombDefaultTexture;
        public static Texture2D BombDefaultTexture =>
            bombDefaultTexture is null ? bombDefaultTexture = Content.Load<Texture2D>(@"Tiles/bomb") : bombDefaultTexture;

        #endregion

        #region CommonTextures

        private static Texture2D background;
        public static Texture2D Background =>
            background is null ? background = Content.Load<Texture2D>(@"background") : background;

        private static Texture2D backgroundMatrix;
        public static Texture2D BackgroundMatrix =>
            backgroundMatrix is null ? backgroundMatrix = Content.Load<Texture2D>(@"Common/matrixBackground") : backgroundMatrix;

        private static Texture2D messageBoxBackground;
        public static Texture2D MessageBoxBackground =>
            messageBoxBackground is null ? messageBoxBackground = Content.Load<Texture2D>(@"Common/messageBoxBackground") : messageBoxBackground;

        private static Texture2D scoreBackground;
        public static Texture2D ScoreBackground =>
            scoreBackground is null ? scoreBackground = Content.Load<Texture2D>(@"Common/scoreBackground") : scoreBackground;

        private static Texture2D buttonBackground;
        public static Texture2D ButtonBackground =>
            buttonBackground is null ? buttonBackground = Content.Load<Texture2D>(@"Buttons/buttonBackground") : buttonBackground;

        #endregion

    }
}
