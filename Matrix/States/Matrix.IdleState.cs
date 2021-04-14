using Match3Game.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Match3Game.MatrixElements
{
    public partial class Matrix
    {
        private class IdleState : MatrixState
        {
            public IdleState(Matrix matrix) : base(matrix)
            {
                matrix.selectedCellStart = null;
                matrix.selectedCellEnd = null;
            }

            public IdleState(Matrix matrix, MatrixState oldState) : base(matrix)
            {
                matrix.selectedCellStart = null;
                matrix.selectedCellEnd = null;
            }

            public override void Update(int elapsedTime)
            {
                base.Update(elapsedTime);
            }

            public void NextState()
            {
                matrix.ChangeState(new SwapState(matrix, this));
            }

            public override void StateStart()
            {
                SubscribeOnMouseEvents();
            }

            public override void StateEnd()
            {
                UnsubscribeOnMouseEvents();
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

            /// <returns> Return a cell with coordinates in matrix and Tile. </returns>
            private Cell GetSelectionCell(Vector2 position)
            {
                Cell cell = PositionConverter.GetCellByClickPosition(position);
                if (0 <= cell.C && cell.C < matrix.Columns)
                {
                    if (0 <= cell.R && cell.R < matrix.Rows)
                    {
                        if(matrix[cell] != null)
                        {
                            cell.Tile = matrix[cell];
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
                if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                {
                    Cell cell = GetSelectionCell(args.ClickPosition);
                    if(cell != null)
                    {
                        matrix[cell] = matrix.tileFactory.CreateNextTile(cell.Tile.GetType(), cell);
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
                                    matrix[cell] = matrix.tileFactory.CreateHorizontalLineBonus(cell.Tile.GetType(), cell);
                                    break;
                                }
                            case TileType.HorizontalLine:
                                {
                                    matrix[cell] = matrix.tileFactory.CreateVerticalLineBonus(cell.Tile.GetType(), cell);
                                    break;
                                }
                            case TileType.VerticalLine:
                                {
                                    matrix[cell] = matrix.tileFactory.CreateBomb(cell.Tile.GetType(), cell);
                                    break;
                                }
                            case TileType.Bomb:
                                {
                                    matrix[cell] = matrix.tileFactory.CreateTile(cell.Tile.GetType(), cell);
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
                if (matrix.selectedCellStart != null)
                {
                    switch (args.SwipeDirection)
                    {
                        case Direction.Left:
                            {
                                Swap(matrix.selectedCellStart.R, matrix.selectedCellStart.C - 1);
                                break;
                            }

                        case Direction.Up:
                            {
                                Swap(matrix.selectedCellStart.R - 1, matrix.selectedCellStart.C);
                                break;
                            }

                        case Direction.Right:
                            {
                                Swap(matrix.selectedCellStart.R, matrix.selectedCellStart.C + 1);
                                break;
                            }

                        case Direction.Down:
                            {
                                Swap(matrix.selectedCellStart.R + 1, matrix.selectedCellStart.C);
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

                void Swap(int row, int column)
                {
                    matrix.selectedCellEnd = new Cell(row, column);
                    matrix.selectedCellEnd.Tile = matrix[row, column];
                    NextState();
                }

                void DontSwap()
                {
                    matrix.selectedCellStart.Tile.ChangeState(TileState.Idle);
                    matrix.selectedCellStart = null;
                    matrix.selectedCellEnd = null;
                }
            }
            
        }
    }
}
