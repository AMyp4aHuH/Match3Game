﻿using Match3Game.Common;
using Match3Game.Interfaces;
using Match3Game.Matrix;
using Match3Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.View
{
    public class Sprite : IGameElement
    {
        public Vector2 PositionOnScreen;
        public float Scale = 1;
        protected Texture2D foreground;

        public Sprite()
        {

        }

        public Sprite (Texture2D texture)
        {
            foreground = texture;
        }

        protected Point currentFrame = new Point(0, 0);
        protected Point spriteSize = new Point(1, 1);



        /// <summary> Needed for draw on screen. </summary>
        public Rectangle frame => new Rectangle((int)PositionOnScreen.X, (int)PositionOnScreen.Y, (int)(foreground.Width / spriteSize.X), (int)(foreground.Height / spriteSize.Y));
        
        /// <summary> Needed for collision detection. </summary>
        public Rectangle Rectangle => new Rectangle((int)PositionOnScreen.X, (int)PositionOnScreen.Y, (int)((foreground.Width / spriteSize.X) * Scale), (int)((foreground.Height / spriteSize.Y) * Scale));

        public virtual void SetForeground(Texture2D texture)
        {
            foreground = texture;
        }

        public virtual void SetForeground(Texture2D texture, Point spriteSize, Point currentFrame)
        {
            foreground = texture;
            this.spriteSize = spriteSize;
            this.currentFrame = currentFrame;
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
            spriteBatch.Draw(foreground, PositionOnScreen, new Rectangle(currentFrame.X * frame.Width, currentFrame.Y * frame.Height, frame.Width, frame.Height), Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public virtual void Update(int milliseconds)
        {

        }
    }
}
