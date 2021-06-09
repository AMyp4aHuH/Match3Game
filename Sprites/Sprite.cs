using Match3Game.Interfaces;
using Match3Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Sprites
{
    public class Sprite : IGameElement
    {
        protected Texture2D foreground;
        public Vector2 PositionOnScreen;
        public float Scale = 1;

        public Sprite()
        {

        }

        public Sprite (Texture2D texture)
        {
            foreground = texture;
        }

        /// <summary> Needed for draw on screen. </summary>
        protected Rectangle frame => new Rectangle(
            0, 
            0, 
            foreground.Width, 
            foreground.Height
            );
        
        /// <summary> Needed for collision detection. </summary>
        public Rectangle Rectangle => new Rectangle(
            (int)PositionOnScreen.X, 
            (int)PositionOnScreen.Y, 
            (int)(foreground.Width * Scale), 
            (int)(foreground.Height * Scale)
            );

        public virtual void SetForeground(Texture2D texture)
        {
            foreground = texture;
        }

        public virtual void DisplayOnCenterScreen()
        {
            var indentWidth = (Scene.ScreenSize.X - Rectangle.Width) / 2;
            var indentHeight = (Scene.ScreenSize.Y - Rectangle.Height) / 2;
            PositionOnScreen = new Vector2(indentWidth, indentHeight);
        }

        public virtual void DisplayOnCenterSprite(Sprite sprite, int indentWidth = 0, int indentHeight = 0)
        {
            indentWidth += (sprite.Rectangle.Width - Rectangle.Width) / 2;
            indentHeight += (sprite.Rectangle.Height - Rectangle.Height) / 2;
            PositionOnScreen = new Vector2(sprite.Rectangle.X + indentWidth, sprite.Rectangle.Y + indentHeight);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (foreground != null)
            {
                spriteBatch.Draw(
                    foreground,
                    PositionOnScreen,
                    frame,
                    Color.White,
                    0,
                    Vector2.Zero,
                    Scale,
                    SpriteEffects.None,
                    0
                    );
            }
        }

        public virtual void Update(int milliseconds)
        {
            
        }
    }
}
