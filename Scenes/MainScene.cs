﻿using Microsoft.Xna.Framework;
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

        public override void Load(ContentManager content)
        {
            Sprite background = new Sprite(TextureProvider.Background);
            background.DisplayOnCenterScreen();

            spriteFontScore = FontProvider.DefaultFont;

            Matrix matrix = new Matrix(
                8,
                28,
                ScreenSize,
                new DefaultTileFactory(),
                GameAnalytics
                );

            timer = new Timer(60000);
            timer.Action += StopGame;

            Sprite backgroundMatrix = new Sprite(TextureProvider.BackgroundMatrix);
            backgroundMatrix.Scale = 
                (float)((Matrix.CellSize * matrix.Rows) + matrix.Rows) / backgroundMatrix.Rectangle.Height;
            backgroundMatrix.DisplayOnCenterScreen();

            Sprite backgroundScore = new Sprite(TextureProvider.ScoreBackground);
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
            spriteBatch.DrawString(
                spriteFontScore, 
                $"Score: {GameAnalytics.Score}", 
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
    
        public void StopGame()
        {
            MatrixState.GameOver = true;
            RemoveGameElement(timer);

            Sprite messageBackground = new Sprite(TextureProvider.MessageBoxBackground);
            messageBackground.Scale = 0.5f;
            messageBackground.DisplayOnCenterScreen();

            SpriteFontAdapter messageSprite = 
                new SpriteFontAdapter(FontProvider.DefaultFont, Vector2.Zero, "Game Over!");
            messageSprite.DisplayOnCenterSprite(messageBackground, 5, -30);

            Button buttonOk = new Button(TextureProvider.ButtonBackground, FontProvider.DefaultFont, "OK");
            buttonOk.Scale = 0.5f;
            buttonOk.DisplayOnCenterSprite(messageBackground, 5, 20);
            buttonOk.Click += ButtonClickOK;

            AddGameElement(messageBackground);
            AddGameElement(messageSprite);
            AddGameElement(buttonOk);
        }

        public void ButtonClickOK()
        {
            GameAnalytics.Reset();
            MouseClickDetector.Clear();
            ScenesManager.ChangeScene<StartScene>();
        }
    }
}
