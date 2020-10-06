using Match3Game.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match3Game.Matrix;

namespace Match3Game.View
{
    public class TileSprite : Sprite
    {
        private Texture2D detail;

        private const int selectPeriod = 100;
        private const int movePeriod = 10;
        private const int createPeriod = 40;
        private const int destroyPeriod = 40;
        private const int frameWidth = 24;
        private const int frameHeight = 24;
        private const int waitDestroyDelay = 250;
        private int elapsedTime = 0;

        protected Queue<Vector2> movePositions = new Queue<Vector2>();

        public void SetDetail(Texture2D texture)
        {
            detail = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (foreground != null)
            {
                spriteBatch.Draw(
                   foreground,
                   PositionOnScreen,
                   new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight),
                   Microsoft.Xna.Framework.Color.White
                   );
            }

            if (detail != null)
            {
                spriteBatch.Draw(
               detail,
               PositionOnScreen,
               new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight),
               Microsoft.Xna.Framework.Color.White);
            }
        }

        public override void Update(int milliseconds)
        {

        }

        public void DisplayIdle()
        {
            elapsedTime = 0;
            currentFrame = new Point(0, 0);
        }

        public void DisplaySelect(int elapsedTime)
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

        public bool DisplayMove(int elapsedTime)
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
                DisplayIdle();
                return true;
            }

            return false;
        }

        public bool DisplayDestroy(int elapsedTime)
        {
            this.elapsedTime += elapsedTime;
            if (this.elapsedTime >= destroyPeriod)
            {
                this.elapsedTime -= destroyPeriod;

                ++currentFrame.X;
                if (currentFrame.X >= spriteSize.X - 1)
                {
                    ++currentFrame.Y;
                    if (currentFrame.Y >= spriteSize.Y - 1)
                    {
                        this.elapsedTime = 0;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool DisplayWaitDestroy(int elapsedTime)
        {
            this.elapsedTime += elapsedTime;
            if (this.elapsedTime >= waitDestroyDelay)
            {
                elapsedTime = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DisplayCreate(int elapsedTime)
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
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animationTime"> Count frames in MOVE animation. </param>
        /// <param name="distance"> Distance in pixels. </param>
        protected float[] FillQueue(int countTiks = 20, int distance = 26)
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
