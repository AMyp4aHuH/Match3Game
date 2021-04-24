using Match3Game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.MatrixElements.Destroyers
{
    public class LineDestroyers
    {
        private Cell lineBonus;
        private Destroyer firstDestroyer;
        private Destroyer secondDestroyer;
        private Matrix matrix;
        private TileFactory tileFactory;

        /// <param name="cellLine"> Must contain Tile and its position in the matrix. </param>
        public LineDestroyers(Matrix matrix ,Cell lineBonus, TileFactory tileFactory)
        {
            this.matrix = matrix;
            this.lineBonus = lineBonus;
            this.tileFactory = tileFactory;

            switch(this.lineBonus.Tile.Type)
            {
                case TileType.HorizontalLine:
                    {
                        if (this.lineBonus.C != 0 && this.lineBonus.C != matrix.Columns - 1)
                        {
                            GoLeft();
                            GoRight();
                        }
                        else
                        {
                            if(this.lineBonus.C == 0)
                            {
                                GoRight();
                            }
                            else
                            {
                                GoLeft();
                            }
                        }

                        break;
                    }

                case TileType.VerticalLine:
                    {
                        if (this.lineBonus.R != 0 && this.lineBonus.R != matrix.Rows - 1)
                        {
                            GoUp();
                            GoDown();
                        }
                        else
                        {
                            if (this.lineBonus.R == 0)
                            {
                                GoDown();
                            }
                            else
                            {
                                GoUp();
                            }
                        }
                        break;
                    }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            firstDestroyer?.Draw(spriteBatch);
            secondDestroyer?.Draw(spriteBatch);
        }

        public void Update(int elapsedTime)
        {
            UpdateDestroyer(firstDestroyer, elapsedTime);
            UpdateDestroyer(secondDestroyer, elapsedTime);
        }

        public void UpdateDestroyer(Destroyer destroyer, int elapsedTime)
        {
            if (destroyer != null)
            {
                destroyer.Update(elapsedTime);
                var cell = PositionConverter.GetCellByTilePosition(destroyer.PositionOnScreen);
                var tile = matrix[cell];
                if (tile != null && (tile.State == TileState.Idle || tile.State == TileState.WaitDestroy))
                {
                    tile.ChangeState(TileState.Destroy);
                    SearchNotDestryedTile(cell, destroyer.LastCellCollision);
                }
                destroyer.LastCellCollision = cell;
            }
        }

        public void SearchNotDestryedTile(Cell current, Cell last)
        {
            if (last != null)
            {
                if (!current.Equals(last))
                {
                    int r = current.R - last.R;
                    int c = current.C - last.C;

                    if (r == 0 && Math.Abs(c) > 1)
                    {
                        // Horizontal.
                        if (c > 0)
                        {
                            // Right.
                            for (int i = 1; i < Math.Abs(c); i++)
                            {
                                matrix[last.R, last.C + i]?.ChangeState(TileState.Destroy);
                            }
                        }
                        else
                        {
                            // Left.
                            for (int i = 1; i < Math.Abs(r); i++)
                            {
                                matrix[last.R, last.C - i]?.ChangeState(TileState.Destroy);
                            }
                        }
                    }
                    else if (c == 0 && Math.Abs(r) > 1)
                    {
                        // Vertical.
                        if (r > 0)
                        {
                            // Down.
                            for (int i = 1; i < Math.Abs(r); i++)
                            {
                                matrix[last.R + i, last.C]?.ChangeState(TileState.Destroy);
                            }
                        }
                        else
                        {
                            // Up.
                            for (int i = 1; i < Math.Abs(r); i++)
                            {
                                matrix[last.R - i, last.C]?.ChangeState(TileState.Destroy);
                            }
                        }
                    }
                }
            }
        }

        private void GoLeft()
        {
            firstDestroyer = tileFactory.CreateDestoyer();
            firstDestroyer.Move(
                PositionConverter.GetPositionsOnScreen(r: lineBonus.R, c: lineBonus.C - 1),
                PositionConverter.GetPositionsOnScreen(r: lineBonus.R, c: 0)
                );
        }

        private void GoRight()
        {
            secondDestroyer = tileFactory.CreateDestoyer();
            secondDestroyer.Move(
                PositionConverter.GetPositionsOnScreen(r: lineBonus.R, c: lineBonus.C + 1),
                PositionConverter.GetPositionsOnScreen(r: lineBonus.R, c: matrix.Columns - 1)
                );
        }

        private void GoUp()
        {
            firstDestroyer = tileFactory.CreateDestoyer();
            firstDestroyer.Move(
                PositionConverter.GetPositionsOnScreen(r: lineBonus.R - 1, c: lineBonus.C),
                PositionConverter.GetPositionsOnScreen(r: 0, c: lineBonus.C)
                );
        }

        private void GoDown()
        {
            secondDestroyer = new Destroyer();
            secondDestroyer.Move(
                PositionConverter.GetPositionsOnScreen(r: lineBonus.R + 1, c: lineBonus.C),

                PositionConverter.GetPositionsOnScreen(r: matrix.Rows - 1, c: lineBonus.C)

                );
        }

        public bool IsAlive()
        {
            return IsFirstDestroyerAlive() && IsSecondDestroyerAlive();
        }

        private bool IsFirstDestroyerAlive()
        {
            if(firstDestroyer is null)
            {
                return true;
            }
            else 
            {
                return firstDestroyer.State == DestroyerState.Empty;
            }
        }
       
        private bool IsSecondDestroyerAlive()
        {
            if (secondDestroyer is null)
            {
                return true;
            }
            else
            {
                return secondDestroyer.State == DestroyerState.Empty;
            }
        }
    }
}
