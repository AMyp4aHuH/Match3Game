using Match3Game.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Scenes.Adapters
{
    public class Texture2dAdapter : IGameElement
    {
        private Texture2D texture;
        private Vector2 position;

        public Texture2dAdapter (Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        /// <summary> Empty method. </summary>
        public void Update(int milliseconds)
        { 

        }
    }
}
