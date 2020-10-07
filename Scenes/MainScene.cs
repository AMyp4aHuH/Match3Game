using Match3Game.Common;
using Match3Game.Matrix;
using Match3Game.Scenes.Adapters;
using Match3Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Scenes
{
    public class MainScene : Scene
    {
        private SpriteFont spriteFontScore;
        private Timer timer;

        public override void Load(ContentManager content)
        {
            Sprite background = new Sprite(TextureManager.Background);
            background.DisplayOnCenterScreen();
            spriteFontScore = FontManager.DefaultFont;

            Matrix.Matrix matrix = new Matrix.Matrix(
                8,
                28,
                ScreenSize,
                new DefaultTileFactory(),
                GameAnalytics
                );

            timer = new Timer(3000);
            timer.Action += StopGame;

            Sprite backgroundMatrix = new Sprite(TextureManager.BackgroundMatrix);
            backgroundMatrix.Scale = (float)((Matrix.Matrix.CellSize * matrix.Rows) + matrix.Rows) / backgroundMatrix.Rectangle.Height;
            backgroundMatrix.DisplayOnCenterScreen();

            Sprite backgroundScore = new Sprite(TextureManager.ScoreBackground);
            backgroundScore.Scale = 0.5f;
            backgroundScore.DisplayOnCenterScreen();
            backgroundScore.PositionOnScreen = new Vector2(20, backgroundScore.PositionOnScreen.Y);

            AddGameElement(background);
            AddGameElement(backgroundScore);
            AddGameElement(backgroundMatrix);
            AddGameElement(matrix);
            AddGameElement(timer);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFontScore, $"Score: {GameAnalytics.Score}", new Vector2(35,200), Color.White);
            spriteBatch.DrawString(spriteFontScore, $"Time: {((timer is null) ? 0 : timer.DelayTime / 1000 )}", new Vector2(35, 235), Color.White); 
        }

        public override void Update(int milliseconds)
        {
            base.Update(milliseconds);
        }
    
        public void StopGame()
        {
            Matrix.State.GameOver = true;
            RemoveGameElement(timer);

            Sprite messageBackground = new Sprite(TextureManager.MessageBoxBackground);
            messageBackground.Scale = 0.5f;
            messageBackground.DisplayOnCenterScreen();

            SpriteFontAdapter messageSprite = new SpriteFontAdapter(FontManager.DefaultFont, new Vector2(0, 0), "Game Over!");
            messageSprite.DisplayOnCenterSprite(messageBackground, 5, -30);

            Button buttonOk = new Button(TextureManager.ButtonBackground, FontManager.DefaultFont, "OK");
            buttonOk.Scale = 0.5f;
            buttonOk.DisplayOnCenterSprite(messageBackground, 5, 20);
            buttonOk.Click += ButtonClickOK;

            AddGameElement(messageBackground);
            AddGameElement(messageSprite);
            AddGameElement(buttonOk);
        }

        public void ButtonClickOK()
        {
            Scene.GameAnalytics.Reset();
            MouseClickDetector.Clear();
            ScenesManager.ChangeScene<StartScene>();
        }
    }
}
