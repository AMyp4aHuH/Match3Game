
using Match3Game.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Match3Game.Matrix
{
    /// <summary>
    /// Include picture and move animation.
    /// </summary>
    
    public partial class Tile
    {
        public class TileAnimation
        {
            private Tile tile;

            public Vector2 PositionOnScreen;

            private Texture2D foreground;
            private Texture2D detail;

            private const int selectPeriod = 80;
            private const int movePeriod = 10;
            private const int destroyPeriod = 40;
            private const int frameWidth = 24;
            private const int frameHeight = 24;
            /// <summary> In milliseconds. </summary>
            private const int waitDestroyDelay = 250;

            private Point currentFrame = new Point(0, 0);
            private Point spriteSize = new Point(4, 1);

            public Rectangle Frame => new Rectangle((int)PositionOnScreen.X, (int)PositionOnScreen.Y, foreground.Width / spriteSize.X, foreground.Height / spriteSize.Y);
            /// <summary>
            /// When this variable can be used, it is zero. 
            /// </summary>
            private int elapsedTime = 0;

            private Queue<Vector2> swipePositions = new Queue<Vector2>();

            public TileAnimation(Tile tile)
            {
                this.tile = tile;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                if(foreground != null)
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
                if (swipePositions.Count > 0)
                {
                    if (this.elapsedTime > movePeriod)
                    {
                        this.elapsedTime -= movePeriod;
                        Vector2 deq = swipePositions.Dequeue();
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
                return false;
            }

            public void SetForegraund(Texture2D foregraund) => this.foreground = foregraund;

            public void SetDetail(Texture2D detail) => this.detail = detail;

            public void Move(Direction direction)
            {
                float[] positions = FillQueue(distance: Matrix.CellSize);

                switch (direction)
                {
                    case Direction.Left:
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            swipePositions.Enqueue(new Vector2(positions[i] * (-1), 0));
                        }
                        break;
                    case Direction.Up:
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            swipePositions.Enqueue(new Vector2(0, positions[i] * (-1)));
                        }
                        break;
                    case Direction.Right:
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            swipePositions.Enqueue(new Vector2(positions[i], 0));
                        }
                        break;
                    case Direction.Down:
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            swipePositions.Enqueue(new Vector2(0, positions[i]));
                        }
                        break;
                }
            }

            public void GenerateMoveExistTile(int oldRow, int newRow)
            {
                tile.currentState = tile.futureState = TileState.Move;

                int distance = (newRow - oldRow) * Matrix.CellSize;
                PositionOnScreen = new Vector2(PositionOnScreen.X, PositionOnScreen.Y - distance);

                float[] positions = FillQueue(30, distance);

                for (int i = 0; i < positions.Count(); i++)
                {
                    swipePositions.Enqueue(new Vector2(0, positions[i]));
                }
            }

            public void GenerateMoveNewTile(int countRows)
            {
                tile.currentState = tile.futureState = TileState.Move;

                int distance = Matrix.HeightIndent + Matrix.CellSize * countRows;
                PositionOnScreen = new Vector2(PositionOnScreen.X, PositionOnScreen.Y - distance);

                float[] positions = FillQueue(30, distance);

                for (int i = 0; i < positions.Count(); i++)
                {
                    swipePositions.Enqueue(new Vector2(0, positions[i]));
                }
            }

            // TODO: Distance in cells.
            /// <summary>
            /// 
            /// </summary>
            /// <param name="animationTime"> Count frames in MOVE animation. </param>
            /// <param name="distance"> Distance in pixels. </param>
            private float[] FillQueue(int countTiks = 22, int distance = 26)
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

                //var summ = distanceBetweenTiks.Sum();

                return distanceBetweenTiks;
            }
        
        }
    }
}
