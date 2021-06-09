using Match3Game.Common;
using Match3Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Match3Game.MatrixElements
{
    public class Tile : Sprite
    {
        public TileType Type;

        public TileState State { get; private set; }
        protected Queue<Vector2> movePositions = new Queue<Vector2>();

        /// <summary>
        /// Size Tile in pixels (tile is square).
        /// </summary>
        public int Size => animationManager.Size;

        public delegate void TileDestroy(object sender, TileEventArgs e);
        public event TileDestroy Destroying;

        protected AnimationManager<AnimationState> animationManager;

        public Tile()
        {
            State = TileState.Idle;
            Type = TileType.Default;
        }

        public void SetPosition(Cell position)
        {
            var windthIndentInsideCell = (Matrix.CellSize - animationManager.Size * Scale) / 2;
            var heightIndentInsideCell = (Matrix.CellSize - animationManager.Size * Scale) / 2;

            PositionOnScreen = new Vector2(
                position.C * Matrix.CellSize + Matrix.WidthIndent + windthIndentInsideCell,
                position.R * Matrix.CellSize + Matrix.HeightIndent + heightIndentInsideCell
                );
        }

        public override void Update(int elapsedTime)
        {
            switch (State)
            {
                case TileState.Idle:
                    {
                        animationManager.Play(AnimationState.Idle, elapsedTime);
                        break;
                    }

                case TileState.Move:
                    {
                        animationManager.Play(AnimationState.Idle, elapsedTime);

                        if (movePositions.Count == 0)
                        {
                            State = TileState.Idle;
                        }
                        else
                        {
                            PositionOnScreen += movePositions.Dequeue();
                        }
                        break;
                    }

                case TileState.Select:
                    {
                        animationManager.Play(AnimationState.Select ,elapsedTime);
                        break;
                    }     

                case TileState.Destroy:
                    {
                        if (!animationManager.Play(AnimationState.Destroy,elapsedTime))
                        {
                            animationManager.Stop();
                            State = TileState.Empty;
                        }
                        break;
                    }
                
                case TileState.WaitDestroy:
                    {
                        if (!animationManager.Play(AnimationState.WaitDestroy, elapsedTime))
                        {
                            ChangeState(TileState.Destroy);
                        }
                        break;
                    }

                case TileState.Empty:
                    {
                        // Wait delete this tile.
                        break;
                    }
            }
        }

        public void ChangeState(TileState state)
        {
            switch(state)
            {
                case TileState.Destroy:
                    {
                        TileState stateBeforeDestroy = this.State;
                        if (Type == TileType.Bomb || Type == TileType.HorizontalLine || Type == TileType.VerticalLine)
                        {
                            stateBeforeDestroy = TileState.Idle;
                        }

                        this.State = TileState.Destroy;
                        Destroying?.Invoke(this, new TileEventArgs(PositionOnScreen, Type, stateBeforeDestroy, PositionConverter.GetCellByTilePosition(PositionOnScreen)));
                        break;
                    }
                default:
                    {
                        this.State = state;
                        break;
                    }
            }
        }

        public bool IsDestroy() => State == TileState.Destroy;

        public bool IsWaitDestroy() => State == TileState.WaitDestroy;

        public bool IsMove() => State == TileState.Move;

        public bool IsEmpty() => State == TileState.Empty;

        public void SwapMove(Direction direction)
        {
            State = TileState.Move;
            Move(direction);
        }

        public void Move(Direction direction)
        {
            float[] positions = FillMoveQueue(distance: Matrix.CellSize);

            switch (direction)
            {
                case Direction.Left:
                    for (int i = 0; i < positions.Count(); i++)
                    {
                        movePositions.Enqueue(new Vector2(positions[i] * (-1), 0));
                    }
                    break;
                case Direction.Up:
                    for (int i = 0; i < positions.Count(); i++)
                    {
                        movePositions.Enqueue(new Vector2(0, positions[i] * (-1)));
                    }
                    break;
                case Direction.Right:
                    for (int i = 0; i < positions.Count(); i++)
                    {
                        movePositions.Enqueue(new Vector2(positions[i], 0));
                    }
                    break;
                case Direction.Down:
                    for (int i = 0; i < positions.Count(); i++)
                    {
                        movePositions.Enqueue(new Vector2(0, positions[i]));
                    }
                    break;
            }
        }

        public void MoveExistingTileDuringGeneration(int oldRow, int newRow)
        { 
            State = TileState.Move;

            int distance = (newRow - oldRow) * Matrix.CellSize;
            PositionOnScreen = new Vector2(PositionOnScreen.X, PositionOnScreen.Y - distance);

            float[] positions = FillMoveQueue(30, distance);

            for (int i = 0; i < positions.Count(); i++)
            {
                movePositions.Enqueue(new Vector2(0, positions[i]));
            }
        }

        public void MoveNewTileDuringGeneration(int countRows)
        {
            State = TileState.Move;

            int distance = Matrix.HeightIndent + Matrix.CellSize * countRows;
            PositionOnScreen = new Vector2(PositionOnScreen.X, PositionOnScreen.Y - distance);

            float[] positions = FillMoveQueue(30, distance);

            for (int i = 0; i < positions.Count(); i++)
            {
                movePositions.Enqueue(new Vector2(0, positions[i]));
            }
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(animationManager.Texture != null)
            {
                spriteBatch.Draw(
                animationManager.Texture,
                PositionOnScreen,
                animationManager.Frame,
                Color.White,
                0,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0);
            }

            if(animationManager.DetailTexture != null)
            {
                spriteBatch.Draw(
                animationManager.DetailTexture,
                PositionOnScreen,
                animationManager.Frame,
                Color.White,
                0,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0);
            }
        }
    }
}
