using Match3Game.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Match3Game.Matrix
{
    public partial class Matrix
    {
        public class IdleState : State
        {
            public IdleState(Matrix matrix) : base(matrix)
            {
                matrix.selectedCellStart = null;
                matrix.selectedCellEnd = null;
                SubscribeOnMouseEvents();
            }

            public IdleState(Matrix matrix, State oldState) : base(matrix)
            {
                matrix.selectedCellStart = null;
                matrix.selectedCellEnd = null;
                SubscribeOnMouseEvents();
            }

            public override void Update(int elapsedTime)
            {
                base.Update(elapsedTime);
            }

            public void NextState()
            {
                UnsubscribeOnMouseEvents();
                matrix.ChangeState(new SwapState(matrix, this));
            }

            public void SetEndCellAndChangeState(int row, int column)
            {
                matrix.selectedCellEnd = new Cell(row, column);
                matrix.selectedCellEnd.Tile = matrix[row, column];
                NextState();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"> Click coordinates along the X-axis. (On the screen) </param>
            /// <param name="y"> Click coordinates along the Y-axis. (On the screen) </param>
            /// <returns> Return a cell with coordinates in matrix and tile. </returns>
            private Cell GetSelectionCell(Vector2 position)
            {
                // True work!!!
                position.X -= WidthIndent;
                position.Y -= HeightIndent;

                var xx = Math.Floor(Convert.ToDecimal((decimal)position.X / (decimal)CellSize));
                var yy = Math.Floor(Convert.ToDecimal((decimal)position.Y / (decimal)CellSize));

                if (0 <= xx && xx < matrix.Columns)
                {
                    if (0 <= yy && yy < matrix.Rows)
                    {
                        Cell cell = new Cell((int)yy, (int)xx);
                        if(matrix[cell.R, cell.C] != null)
                        {
                            cell.Tile = matrix[cell.R, cell.C];
                        }
                        else
                        {
                            cell = null;
                        }
                        
                        return cell;
                    }
                }

                return null;
            }

            public void OnSelectTile(object sender, MouseClickEventArgs args)
            {
                if(GameOver)
                {
                    UnsubscribeOnMouseEvents();
                    matrix.ChangeState(new EmptyState(matrix));
                    return;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                {
                    Cell cell = GetSelectionCell(args.ClickPosition);
                    if(cell != null)
                    {
                        matrix[cell] = matrix.tileFactory.GetNextTile(cell.Tile.GetType(), cell);
                    }
                }
                else if(Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                {
                    Cell cell = GetSelectionCell(args.ClickPosition);

                    if(cell != null)
                    {
                        switch (cell.Tile.Type)
                        {
                            case TileType.Default:
                                {
                                    matrix[cell] = matrix.tileFactory.GetHorizontalLineBonus(cell.Tile.GetType(), cell);
                                    break;
                                }
                            case TileType.HorizontalLine:
                                {
                                    matrix[cell] = matrix.tileFactory.GetVerticalLineBonus(cell.Tile.GetType(), cell);
                                    break;
                                }
                            case TileType.VerticalLine:
                                {
                                    matrix[cell] = matrix.tileFactory.GetBomb(cell.Tile.GetType(), cell);
                                    break;
                                }
                            case TileType.Bomb:
                                {
                                    matrix[cell] = matrix.tileFactory.GetTile(cell.Tile.GetType(), cell);
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    if (matrix.selectedCellStart is null)
                    {
                        matrix.selectedCellStart = GetSelectionCell(args.ClickPosition);
                        if (matrix.selectedCellStart != null)
                        {
                            matrix.selectedCellStart.Tile = matrix[matrix.selectedCellStart.R, matrix.selectedCellStart.C];
                            matrix.selectedCellStart.Tile.ChangeState(TileState.Select);
                        }
                    }
                    else
                    {
                        matrix.selectedCellEnd = GetSelectionCell(args.ClickPosition);
                        if (matrix.selectedCellEnd != null)
                        {
                            matrix.selectedCellEnd.Tile = matrix[matrix.selectedCellEnd.R, matrix.selectedCellEnd.C];
                            UnsubscribeOnMouseEvents();
                            matrix.ChangeState(new SwapState(matrix, this));
                        }
                        else
                        {
                            matrix.selectedCellStart.Tile.ChangeState(TileState.Idle);
                            matrix.selectedCellStart = null;
                        }
                    }
                }
            }

            public void OnSwapTiles(object sender, MouseSwipeEventArgs args)
            {
                if (GameOver)
                {
                    UnsubscribeOnMouseEvents();
                    matrix.ChangeState(new EmptyState(matrix));
                    return;
                }

                if (matrix.selectedCellStart != null)
                {
                    switch (args.SwipeDirection)
                    {
                        case Direction.Left:
                            {
                                if (0 < matrix.selectedCellStart.C)
                                {
                                    SetEndCellAndChangeState(matrix.selectedCellStart.R, matrix.selectedCellStart.C - 1);
                                }
                                else
                                {
                                    DontSwap();
                                }
                                break;
                            }

                        case Direction.Up:
                            {
                                if (0 < matrix.selectedCellStart.R)
                                {
                                    SetEndCellAndChangeState(matrix.selectedCellStart.R - 1, matrix.selectedCellStart.C);
                                }
                                else
                                {
                                    DontSwap();
                                }
                                break;
                            }

                        case Direction.Right:
                            {
                                if (matrix.selectedCellStart.C < matrix.Columns - 1)
                                {
                                    SetEndCellAndChangeState(matrix.selectedCellStart.R, matrix.selectedCellStart.C + 1);
                                }
                                else
                                {
                                    DontSwap();
                                }
                                break;
                            }

                        case Direction.Down:
                            {
                                if (matrix.selectedCellStart.R < matrix.Rows - 1)
                                {
                                    SetEndCellAndChangeState(matrix.selectedCellStart.R + 1, matrix.selectedCellStart.C);
                                }
                                else
                                {
                                    DontSwap();
                                }
                                break;
                            }

                        case Direction.None:
                            {
                                DontSwap();
                                break;
                            }

                        default:
                            break;
                    }
                }

                void DontSwap()
                {
                    matrix.selectedCellStart.Tile.ChangeState(TileState.Idle);
                    matrix.selectedCellStart = null;
                    matrix.selectedCellEnd = null;
                }
            }
        
            public void SubscribeOnMouseEvents()
            {
                MouseClickDetector.LeftButtonDown += OnSelectTile;
                MouseClickDetector.LeftButtonSwipe += OnSwapTiles;
            }

            public void UnsubscribeOnMouseEvents()
            {
                MouseClickDetector.LeftButtonDown -= OnSelectTile;
                MouseClickDetector.LeftButtonSwipe -= OnSwapTiles;
            }
        }
    }
}
