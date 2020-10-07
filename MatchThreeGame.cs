using Match3Game.Common;
using Match3Game.Scenes;
using Match3Game.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match3Game
{
    public class MatchThreeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameAnalytics gameAnalytics;

        public MatchThreeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            gameAnalytics = new GameAnalytics();
            Scene.GameAnalytics = gameAnalytics;
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Scene.ScreenSize = new Point(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";
            TextureProvider.Content = Content;
            FontProvider.Content = Content;
            ScenesManager.Content = Content;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseClickDetector.Update();
            ScenesManager.Update(gameTime.ElapsedGameTime.Milliseconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            ScenesManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
