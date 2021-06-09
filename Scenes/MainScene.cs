using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Match3Game.MatrixElements;
using Match3Game.Common;
using Match3Game.Controls;
using Match3Game.Providers;
using Match3Game.Sprites;
using Match3Game.Sprites.Adapters;
using Matrix = Match3Game.MatrixElements.Matrix;

namespace Match3Game.Scenes
{
    public class MainScene : Scene
    {
        private SpriteFont spriteFontScore;
        private Timer timer;
        private Matrix matrix;

        public override void Load(ContentManager content)
        {
            Sprite background = new Sprite(TextureProvider.Common.Background);
            background.DisplayOnCenterScreen();

            spriteFontScore = FontProvider.DefaultFont;

            var tileFactory = new DefaultTileFactory();

            matrix = new Matrix(
                8,
                0.7f,
                0.9f,
                ScreenSize,
                tileFactory
                );

            timer = new Timer(60000);
            timer.Action += GameOver;

            Sprite backgroundMatrix = new Sprite(TextureProvider.Common.BackgroundMatrix);
            backgroundMatrix.Scale = 
                (float)((Matrix.CellSize * matrix.Rows) + matrix.Rows) / backgroundMatrix.Rectangle.Height;
            backgroundMatrix.DisplayOnCenterScreen();

            Sprite backgroundScore = new Sprite(TextureProvider.Common.ScoreBackground);
            backgroundScore.Scale = 0.5f;
            backgroundScore.DisplayOnCenterScreen();
            backgroundScore.PositionOnScreen = new Vector2(20, backgroundScore.PositionOnScreen.Y);

            AddChild(background);
            AddChild(backgroundScore);
            AddChild(backgroundMatrix);
            AddChild(matrix);
            AddChild(timer);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(
                spriteFontScore, 
                $"Score: {GameAnalytics.Instance.Score}", 
                new Vector2(35,202), 
                Color.White);
            spriteBatch.DrawString(
                spriteFontScore, 
                $"Time: {((timer is null) ? 0 : timer.DelayTime / 1000 )}",
                new Vector2(35, 240),
                Color.White); 
        }

        public override void Update(int elapsedTime)
        {
            base.Update(elapsedTime);
        }
    
        public void GameOver()
        {
            RemoveChild(timer);
            matrix.GameOver();

            Sprite messageBackground = new Sprite(TextureProvider.Common.MessageBoxBackground);
            messageBackground.Scale = 0.5f;
            messageBackground.DisplayOnCenterScreen();

            SpriteFontAdapter messageSprite = 
                new SpriteFontAdapter(FontProvider.DefaultFont, Vector2.Zero, "Game Over!");
            messageSprite.DisplayOnCenterSprite(messageBackground, 5, -30);

            Button buttonOk = new Button(TextureProvider.Common.ButtonBackground, FontProvider.DefaultFont, "OK");
            buttonOk.Scale = 0.5f;
            buttonOk.DisplayOnCenterSprite(messageBackground, 5, 20);
            buttonOk.Click += ButtonClickOK;

            AddChild(messageBackground);
            AddChild(messageSprite);
            AddChild(buttonOk);
        }

        public void ButtonClickOK()
        {
            GameAnalytics.Instance.Reset();
            MouseClickDetector.Clear();
            ScenesManager.ChangeScene<StartScene>();
        }
    }
}
