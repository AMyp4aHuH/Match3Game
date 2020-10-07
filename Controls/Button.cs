using Match3Game.Common;
using Match3Game.Interfaces;
using Match3Game.Sprites;
using Match3Game.Sprites.Adapters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3Game.Controls
{
    public class Button : Sprite, IGameElement
    {
        public Action Click;
        private SpriteFontAdapter spriteFontAdapter;

        public Button(Texture2D texture) : base (texture)
        {
            MouseClickDetector.LeftButtonDown += Notify;
        }

        public Button(Texture2D texture, SpriteFont spriteFont, string text) : base(texture)
        {
            MouseClickDetector.LeftButtonDown += Notify;
            spriteFontAdapter = new SpriteFontAdapter(spriteFont, Vector2.Zero, text);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteFontAdapter?.Draw(spriteBatch);
        }

        public void Notify(object sender, MouseClickEventArgs args)
        {
            if(Rectangle.Intersects(new Rectangle((int)args.ClickPosition.X, (int)args.ClickPosition.Y, 1, 1)))
            {
                // TODO: Make a button for many clicks.
                MouseClickDetector.LeftButtonDown -= Notify;
                Click?.Invoke();
            }
        }

        public override void DisplayOnCenterScreen()
        {
            base.DisplayOnCenterScreen();
            spriteFontAdapter?.DisplayOnCenterSprite(this);
        }

        public override void DisplayOnCenterSprite(Sprite sprite, int indentWidth = 0, int indentHeight = 0)
        {
            base.DisplayOnCenterSprite(sprite, indentWidth, indentHeight);
            spriteFontAdapter?.DisplayOnCenterSprite(this);
        }
    }
}
