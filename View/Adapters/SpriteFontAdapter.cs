using Match3Game.Interfaces;
using Match3Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Scenes.Adapters
{
    public class SpriteFontAdapter : IGameElement
    {
        private SpriteFont sprite;
        public Vector2 PositionOnScreen;
        private string text = "";

        /// <summary>
        /// Use when we need display static text.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="position"> Position on the game screen. </param>
        /// <param name="text"> Static text for display on the geme screen. </param>
        public SpriteFontAdapter(SpriteFont sprite, Vector2 position, string text)
        {
            this.sprite = sprite;
            this.PositionOnScreen = position;
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                sprite,
                text,
                PositionOnScreen, 
                Color.White
                );
            
        }

        /// <summary> Empty method. </summary>
        public void Update(int milliseconds) 
        { 
        
        }

        public virtual void DisplayOnCenterSprite(Sprite sprite, int indentWidth = 0, int indentHeight = 0)
        {
            indentWidth += (sprite.Rectangle.Width - (int)this.sprite.MeasureString(text).X) / 2;
            indentHeight += (sprite.Rectangle.Height - (int)this.sprite.MeasureString(text).Y) / 2;

            PositionOnScreen = new Vector2(sprite.Rectangle.X + indentWidth, sprite.Rectangle.Y + indentHeight);
        }
    }
}
