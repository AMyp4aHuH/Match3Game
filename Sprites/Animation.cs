using Match3Game.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game
{
    public class Animation
    {
        private Texture2D texture;
        private Texture2D detailTexture;

        public Texture2D Texture => texture;
        public Texture2D DetailTexture => detailTexture;

        public readonly int FrameCount;

        public int FrameWidth
        {
            // Assume square frames.
            get { return texture.Height; }
        }
        public int FrameHeight
        {
            get { return texture.Height; }
        }

        public Rectangle CurrentFrame => new Rectangle(
            currentFrameIndex * FrameWidth,
            0,
            FrameWidth,
            FrameHeight
            );

        public int currentFrameIndex = 0;
        private float frameSpeed;
        private float elapsedTime;
        public readonly bool IsLooping;

        public Animation(Texture2D texture, float frameSpeed, bool isLooping = false)
        {
            this.texture = texture;
            this.frameSpeed = frameSpeed;
            IsLooping = isLooping;
            FrameCount = texture.Width / FrameHeight;
        }

        /// <summary>
        /// Main texture and detail texture have be size equals.
        /// </summary>
        /// <param name="texture"> Detail texture. </param>
        public void SetDetailTexture(Texture2D texture)
        {
            if (texture.Height != this.texture.Height && texture.Width != this.texture.Width)
            {
                throw new ArgumentException("Textures size not equals.");
            }

            detailTexture = texture;
        }

        public void Reset()
        {
            elapsedTime = 0;
            currentFrameIndex = 0;
        }

        public bool NextFrame(float elapsedTime)
        {
            this.elapsedTime += elapsedTime;
            if (this.elapsedTime < frameSpeed)
            {
                return true;
            }

            this.elapsedTime -= frameSpeed;
            if (currentFrameIndex + 1 >= FrameCount)
            { 
                if (!IsLooping)
                {
                    return false;
                }
                else
                {
                    currentFrameIndex = 0;
                }
            }
            else
            {
                currentFrameIndex++;
            }

            return true;
        }
    }
}
