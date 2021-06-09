using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Providers
{
    public static class TextureProvider
    {
        public static ContentManager Content { get; set; }

        public static class BlueTile
        {
            public static TileTextures Textures;
            static BlueTile()
            {
                Textures = new TileTextures(@"Tiles/bIdle", @"Tiles/bDestroy", @"Tiles/bDestroy");
            }
        }

        public static class YellowTile 
        {
            public static TileTextures Textures;
            static YellowTile()
            {
                Textures = new TileTextures(@"Tiles/yIdle", @"Tiles/yDestroy", @"Tiles/yDestroy");
            }
        }

        public static class GreenTile 
        {
            public static TileTextures Textures;
            static GreenTile()
            {
                Textures = new TileTextures(@"Tiles/gIdle", @"Tiles/gDestroy", @"Tiles/gDestroy");
            }
        }

        public static class PinkTile 
        {
            public static TileTextures Textures;
            static PinkTile()
            {
                Textures = new TileTextures(@"Tiles/piIdle", @"Tiles/piDestroy", @"Tiles/piDestroy");
            }
        }

        public static class PurpleTile 
        {
            public static TileTextures Textures;
            static PurpleTile()
            {
                Textures = new TileTextures(@"Tiles/pIdle", @"Tiles/pDestroy", @"Tiles/pDestroy");
            }
        }

        public static class DarkTile 
        {
            public static TileTextures Textures;
            static DarkTile()
            {
                Textures = new TileTextures(@"Tiles/dIdle", @"Tiles/Destroy", @"Tiles/dDestroy", @"Tiles/dCreate");
            }
        }

        public static class HorizontalLineTile
        {
            public static TileTextures Textures;
            static HorizontalLineTile()
            {
                Textures = new TileTextures(@"Tiles/hIdle", @"Tiles/hDestroy", @"Tiles/hDestroy");
            }
        }

        public static class VerticalLineTile
        {
            public static TileTextures Textures;
            static VerticalLineTile()
            {
                Textures = new TileTextures(@"Tiles/vIdle", @"Tiles/vDestroy", @"Tiles/vDestroy");
            }
        }

        public static class BombTile
        {
            public static TileTextures Textures;
            static BombTile()
            {
                Textures = new TileTextures(@"Tiles/bombIdle", @"Tiles/bombDestroy", @"Tiles/bombDestroy");
            }
        }

        public static class Common
        {
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
        }
    }


    public class TileTextures
    {
        private readonly string createPath; // Only destroyers.
        private readonly string idlePath;
        private readonly string selectPath;
        private readonly string destroyPath;
        

        public TileTextures(string idleTexturePath, string selectTexturePath, string destroyTexturePath, string createTextirePath = "") 
        {
            idlePath = idleTexturePath;
            selectPath = selectTexturePath;
            destroyPath = destroyTexturePath;
            createPath = createTextirePath;
        }

        private Texture2D idle;
        public Texture2D Idle =>
            idle is null ? idle = TextureProvider.Content.Load<Texture2D>(idlePath) : idle;

        private Texture2D select;
        public Texture2D Select =>
            select is null ? select = TextureProvider.Content.Load<Texture2D>(selectPath) : select;

        private Texture2D destroy;
        public Texture2D Destroy =>
            destroy is null ? destroy = TextureProvider.Content.Load<Texture2D>(destroyPath) : destroy;

        private Texture2D create;
        public Texture2D Create =>
            create is null ? create = TextureProvider.Content.Load<Texture2D>(createPath) : create;
    }
}
