﻿using Match3Game.Common;
using Match3Game.Matrix;
using Match3Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;


namespace Match3Game.Matrix
{
    public class Tile : TileSprite
    {
        public TileType Type;

        public TileState State { get; private set; }

        public delegate void TileDestroy(object sender, TileEventArgs e);
        public event TileDestroy Destroying;

        public Tile()
        {
            State = TileState.Idle;
            this.Type = TileType.Default;
        }

        public void SetPosition(Cell position)
        {
            var windthIndentInsideCell = (Matrix.CellSize - Rectangle.Width) / 2;
            var heightIndentInsideCell = (Matrix.CellSize - Rectangle.Height) / 2;
            PositionOnScreen = new Vector2(position.C * Matrix.CellSize + Matrix.WidthIndent + windthIndentInsideCell, position.R * Matrix.CellSize + Matrix.HeightIndent + heightIndentInsideCell);
        }

        public override void Update(int elapsedTime)
        {
            switch (State)
            {
                case TileState.Idle:
                    {
                        DisplayIdle();
                        break;
                    }

                case TileState.Move:
                    {
                        //DisplayIdle();
                        bool IsEnd = DisplayMove(elapsedTime);
                        if(IsEnd)
                        {
                            State = TileState.Idle;
                        }
                        break;
                    }

                case TileState.Select:
                    {
                        DisplaySelect(elapsedTime);
                        break;
                    }     

                case TileState.Destroy:
                    {
                        bool IsEnd = DisplayDestroy(elapsedTime);
                        if(IsEnd)
                        {
                            State = TileState.Empty;
                        }
                        break;
                    }
                
                case TileState.WaitDestroy:
                    {
                        bool IsEnd = DisplayWaitDestroy(elapsedTime);
                        if (IsEnd)
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
                        Destroying?.Invoke(this, new TileEventArgs(PositionOnScreen, Type, stateBeforeDestroy, PositionHelper.GetCellByTilePosition(PositionOnScreen)));
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
            float[] positions = FillQueue(distance: Matrix.CellSize);

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

        public void GenerateMoveExistTile(int oldRow, int newRow)
        { 
            State = TileState.Move;

            int distance = (newRow - oldRow) * Matrix.CellSize;
            PositionOnScreen = new Vector2(PositionOnScreen.X, PositionOnScreen.Y - distance);

            float[] positions = FillQueue(30, distance);

            for (int i = 0; i < positions.Count(); i++)
            {
                movePositions.Enqueue(new Vector2(0, positions[i]));
            }
        }

        public void GenerateMoveNewTile(int countRows)
        {
            State = TileState.Move;

            int distance = Matrix.HeightIndent + Matrix.CellSize * countRows;
            PositionOnScreen = new Vector2(PositionOnScreen.X, PositionOnScreen.Y - distance);

            float[] positions = FillQueue(30, distance);

            for (int i = 0; i < positions.Count(); i++)
            {
                movePositions.Enqueue(new Vector2(0, positions[i]));
            }
        }

    }
}
