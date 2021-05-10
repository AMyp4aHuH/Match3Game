using Match3Game.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match3Game.MatrixElements;

namespace Match3Game.Sprites
{
    public class TileSprite : Sprite
    {
        private Texture2D detail;
        private const int selectPeriod = 100;
        private const int movePeriod = 10;
        private const int createPeriod = 40;
        private const int destroyPeriod = 40;
        private const int waitDestroyDelay = 250;
        private int elapsedTime = 0;
        protected Queue<Vector2> movePositions = new Queue<Vector2>();

        public void SetDetail(Texture2D texture)
        {
            detail = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (detail != null)
            {
                spriteBatch.Draw(
                    detail,
                    PositionOnScreen,
                    new Rectangle(
                        currentFrame.X * frame.Width,
                        currentFrame.Y * frame.Height,
                        frame.Width, frame.Height
                        ),
                    Color.White,
                    0,
                    Vector2.Zero,
                    Scale,
                    SpriteEffects.None,
                    0
                );
            }
        }

        public virtual void NextIdleAnimationFrame()
        {
            elapsedTime = 0;
            currentFrame = new Point(0, 0);
        }

        public virtual void NextSelectAnimationFrame(int elapsedTime)
        {
            this.elapsedTime += elapsedTime;
            if (this.elapsedTime >= selectPeriod)
            {
                this.elapsedTime -= selectPeriod;

                ++currentFrame.X;
                if (currentFrame.X >= spriteSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= spriteSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public virtual bool NextMoveAnimationFrame(int elapsedTime)
        {
            currentFrame = new Point(0, 0);
            this.elapsedTime += elapsedTime;
            if (movePositions.Count > 0)
            {
                if (this.elapsedTime > movePeriod)
                {
                    this.elapsedTime -= movePeriod;
                    Vector2 deq = movePositions.Dequeue();
                    PositionOnScreen = new Vector2(PositionOnScreen.X + deq.X, PositionOnScreen.Y + deq.Y);
                }
            }
            else
            {
                NextIdleAnimationFrame();
                return false;
            }
            return true;
        }

        public virtual bool NextDestroyAnimationFrame(int elapsedTime)
        {
            this.elapsedTime += elapsedTime;
            if (this.elapsedTime >= destroyPeriod)
            {
                this.elapsedTime -= destroyPeriod;

                ++currentFrame.X;
                if (currentFrame.X >= spriteSize.X)
                {
                    ++currentFrame.Y;
                    if (currentFrame.Y >= spriteSize.Y)
                    {
                        this.elapsedTime = 0;
                        return false;
                    }
                }
            }
            return true;
        }

        public virtual bool NextWaitDestroyAnimationFrame(int elapsedTime)
        {
            this.elapsedTime += elapsedTime;
            if (this.elapsedTime >= waitDestroyDelay)
            {
                this.elapsedTime = 0;
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual bool NextCreateAnimationFrame(int elapsedTime)
        {
            this.elapsedTime += elapsedTime;
            if (this.elapsedTime >= createPeriod)
            {
                this.elapsedTime -= createPeriod;
                --currentFrame.X;
                if (currentFrame.X <= 0)
                {
                    --currentFrame.Y;
                    if (currentFrame.Y <= 0)
                    {
                        this.elapsedTime = 0;
                        currentFrame = Point.Zero;
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countTiks"> Frames count in MOVE animation. </param>
        /// <param name="distance"> Distance in pixels. </param>
        protected float[] FillMoveQueue(int countTiks = 20, int distance = 26)
        {
            if (countTiks % 2 != 0)
            {
                countTiks -= 1;
            }

            float[] distanceBetweenTiks = new float[countTiks];
            for (int i = 1; i <= countTiks / 2; i++)
            {
                distanceBetweenTiks[i - 1] = i * i;
            }

            float sum = distanceBetweenTiks.Sum() * 2;

            float coeff = distance / sum;

            for (int i = 1; i <= countTiks / 2; i++)
            {
                distanceBetweenTiks[i - 1] *= coeff;
                distanceBetweenTiks[countTiks - i] = distanceBetweenTiks[i - 1];
            }

            return distanceBetweenTiks;
        }
    }
}
