using Match3Game.Common;
using Microsoft.Xna.Framework;
using System;

namespace Match3Game.MatrixElements
{
    public partial class Matrix
    {
        class SwapState : MatrixState
        {
            private MatrixState oldState;

            public SwapState(Matrix matrix, MatrixState oldState) : base(matrix)
            {
                this.oldState = oldState;
                Swap(matrix.selectedCellStart, matrix.selectedCellEnd);
            }

            public override void Update(int elapsedTime)
            {
                bool tilesAreMove = false;

                for (var i = 0; i < matrix.Rows; i++)
                {
                    for (var j = 0; j < matrix.Columns; j++)
                    {
                        if (matrix[i, j] != null)
                        {
                            if (matrix[i, j] != null)
                            {
                                matrix[i, j].Update(elapsedTime);
                                if (matrix[i, j].IsMove())
                                {
                                    tilesAreMove = true;
                                }
                            }
                        }
                    }
                }

                if (!tilesAreMove)
                {
                    NextState();
                }
            }

            public Direction GetSwipeDirection(Cell start, Cell end)
            {
                Direction result;
                var xDifference = start.C - end.C; // 2.3
                var yDifference = start.R - end.R; // 3.3

                // Detecting horiaontal of vertical swipe.
                if (
                    (Math.Abs(xDifference) == 1 || Math.Abs(yDifference) == 1) &&
                    (Math.Abs(xDifference) == 0 || Math.Abs(yDifference) == 0)
                    )
                {
                    if (Math.Abs(xDifference) > Math.Abs(yDifference))
                    {
                        // Horizontal swipe.
                        result = (xDifference > 0) ? Direction.Left : Direction.Right;
                    }
                    else
                    {
                        // Vertical swipe.
                        result = (yDifference > 0) ? Direction.Up : Direction.Down;
                    }
                }
                else
                {
                    result = Direction.None;
                }

                return result;
            }

            public Direction GetMirrorDirection(Direction direction)
            {
                switch (direction)
                {
                    case Direction.Left: return Direction.Right;
                    case Direction.Up: return Direction.Down;
                    case Direction.Right: return Direction.Left;
                    case Direction.Down: return Direction.Up;
                    default: return Direction.None;
                }
            }

            public void Swap(Cell cell1, Cell cell2)
            {
                Direction direction = GetSwipeDirection(cell1, cell2);

                if (direction != Direction.None)
                {
                    Tile temp1 = cell1.Tile;
                    Tile temp2 = cell2.Tile;
                    matrix[cell1.R, cell1.C] = null;
                    matrix[cell2.R, cell2.C] = null;
                    matrix[cell1.R, cell1.C] = temp2;
                    matrix[cell2.R, cell2.C] = temp1;
                    matrix[cell1.R, cell1.C].SwapMove(GetMirrorDirection(direction));
                    matrix[cell2.R, cell2.C].SwapMove(direction);
                    cell1.Tile = temp2;
                    cell2.Tile = temp1;
                }
                else
                {
                    matrix.selectedCellStart.Tile.ChangeState(TileState.Idle);
                }
            }

            /// <summary> Changes the current state to <see cref="Matrix.DestroyState"/> or 
            /// <see cref="Matrix.IdleState"/> depending on the old state. </summary>
            private void NextState()
            {
                // We can do something here if needed when changing state.
                if (oldState is IdleState)
                {
                    matrix.ChangeState(new DestroyState(matrix, this));
                }
                else if(oldState is DestroyState)
                {
                    matrix.ChangeState(new IdleState(matrix));
                }
            }
        }
    }
}
