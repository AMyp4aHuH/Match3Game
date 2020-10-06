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
                12,
                26,
                ScreenSize,
                new DefaultTileFactory(),
                GameAnalytics
                );

            timer = new Timer(600000);
            timer.Action += StopGame;

            AddGameElement(background);
            AddGameElement(matrix);
            AddGameElement(timer);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //spriteBatch.DrawString(spriteFontScore, $"State: {matrix.State}", new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(spriteFontScore, $"Score: {GameAnalytics.Score}", new Vector2(0, 25), Color.White);
            spriteBatch.DrawString(spriteFontScore, $"Time: {((timer is null) ? 0 : timer.DelayTime / 1000 )}", new Vector2(0, 50), Color.White); 
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
            messageBackground.Scale = 0.8f;
            messageBackground.DisplayOnCenterScreen();

            SpriteFontAdapter messageSprite = new SpriteFontAdapter(FontManager.DefaultFont, new Vector2(0, 0), "Game Over!");
            messageSprite.DisplayOnCenterSprite(messageBackground, 10, -80);

            Button buttonOk = new Button(TextureManager.ButtonOK);
            buttonOk.Click = ButtonClickOK;
            buttonOk.DisplayOnCenterSprite(messageBackground, 10, 0);

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
