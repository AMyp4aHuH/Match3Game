using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game
{
    public static class TextureManager
    {
        private static ContentManager content;
        public static ContentManager Content { set => content = value; }

        

        #region DefaultTilesTextures

        private static Texture2D destroyerTileTexture;
        public static Texture2D DestroyerTexture => 
            destroyerTileTexture is null ? destroyerTileTexture = content.Load<Texture2D>(@"Tiles/tileDestroyer") : destroyerTileTexture;

        private static Texture2D pinkDefaultTexture;
        public static Texture2D PinkDefaultTexture =>
            pinkDefaultTexture is null ? pinkDefaultTexture = content.Load<Texture2D>(@"Tiles/pinkTile") : pinkDefaultTexture;

        private static Texture2D purpleDefaultTexture;
        public static Texture2D PurpleDefaultTexture =>
            purpleDefaultTexture is null ? purpleDefaultTexture = content.Load<Texture2D>(@"Tiles/purpleTile") : purpleDefaultTexture;

        private static Texture2D greenDefaultTexture;
        public static Texture2D GreenDefaultTexture =>
            greenDefaultTexture is null ? greenDefaultTexture = content.Load<Texture2D>(@"Tiles/greenTile") : greenDefaultTexture;

        private static Texture2D yellowDefaultTexture;
        public static Texture2D YellowDefaultTexture =>
            yellowDefaultTexture is null ? yellowDefaultTexture = content.Load<Texture2D>(@"Tiles/yellowTile") : yellowDefaultTexture;

        private static Texture2D blueDefaultTexture;
        public static Texture2D BlueDefaultTexture =>
            blueDefaultTexture is null ? blueDefaultTexture = content.Load<Texture2D>(@"Tiles/blueTile") : blueDefaultTexture;

        private static Texture2D verticaDefaultlLineTexture;
        public static Texture2D VerticaDefaultlLineTexture =>
            verticaDefaultlLineTexture is null ? verticaDefaultlLineTexture = content.Load<Texture2D>(@"Tiles/verticalDetail") : verticaDefaultlLineTexture;

        private static Texture2D horizontalDefaultLineTexture;
        public static Texture2D HorizontalDefaultLineTexture =>
            horizontalDefaultLineTexture is null ? horizontalDefaultLineTexture = content.Load<Texture2D>(@"Tiles/horizontalDetail") : horizontalDefaultLineTexture;

        private static Texture2D bombDefaultTexture;
        public static Texture2D BombDefaultTexture =>
            bombDefaultTexture is null ? bombDefaultTexture = content.Load<Texture2D>(@"Tiles/bomb") : bombDefaultTexture;

        #endregion

        #region CommonTextures

        private static Texture2D background;
        public static Texture2D Background =>
            background is null ? background = content.Load<Texture2D>(@"background") : background;

        private static Texture2D backgroundMatrix;
        public static Texture2D BackgroundMatrix =>
            backgroundMatrix is null ? backgroundMatrix = content.Load<Texture2D>(@"Common/backgroundMatrix") : backgroundMatrix;

        private static Texture2D buttonPlay;
        public static Texture2D ButtonPlay =>
            buttonPlay is null ? buttonPlay = content.Load<Texture2D>(@"Buttons/buttonStart") : buttonPlay;

        private static Texture2D buttonOK;
        public static Texture2D ButtonOK =>
            buttonOK is null ? buttonOK = content.Load<Texture2D>(@"Buttons/buttonOK") : buttonOK;

        private static Texture2D messageBoxBackground;
        public static Texture2D MessageBoxBackground =>
            messageBoxBackground is null ? messageBoxBackground = content.Load<Texture2D>(@"Common/backgroundDialog") : messageBoxBackground;

        #endregion

    }
}
