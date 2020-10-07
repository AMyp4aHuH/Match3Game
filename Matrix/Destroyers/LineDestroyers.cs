using Match3Game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Matrix
{
    public class LineDestroyers
    {
        public Cell LineBonus;
        private Destroyer destroyerOne;
        private Destroyer destroyerTwo;
        private Matrix matrix;
        private TileFactory tileFactory;

        /// <param name="cellLine"> Must contain Tile and its position in the matrix. </param>
        public LineDestroyers(Matrix matrix ,Cell cellLine, TileFactory tileFactory)
        {
            this.matrix = matrix;
            LineBonus = cellLine;
            this.tileFactory = tileFactory;

            switch(LineBonus.Tile.Type)
            {
                case TileType.HorizontalLine:
                    {
                        if (LineBonus.C != 0 && LineBonus.C != matrix.Columns - 1)
                        {
                            GoLeft();
                            GoRight();
                        }
                        else
                        {
                            if(LineBonus.C == 0)
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
                        if (LineBonus.R != 0 && LineBonus.R != matrix.Rows - 1)
                        {
                            GoUp();
                            GoDown();
                        }
                        else
                        {
                            if (LineBonus.R == 0)
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
            destroyerOne?.Draw(spriteBatch);
            destroyerTwo?.Draw(spriteBatch);
        }

        public void Update(int elapsedTime)
        {
            UpdateDestroyer(destroyerOne, elapsedTime);
            UpdateDestroyer(destroyerTwo, elapsedTime);
        }

        public void UpdateDestroyer(Destroyer destroyer, int elapsedTime)
        {
            if (destroyer != null)
            {
                destroyer.Update(elapsedTime);
                Cell cell = PositionHelper.GetCellByTilePosition(destroyer.PositionOnScreen);
                Tile tile = matrix[cell];
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
            destroyerOne = tileFactory.CreateDestoyer();
            destroyerOne.Move(
                PositionHelper.GetCoordinatesOnScreen(r: LineBonus.R, c: LineBonus.C - 1),
                PositionHelper.GetCoordinatesOnScreen(r: LineBonus.R, c: 0)
                );
        }

        private void GoRight()
        {
            destroyerTwo = new Destroyer();
            destroyerTwo.Move(
                PositionHelper.GetCoordinatesOnScreen(r: LineBonus.R, c: LineBonus.C + 1),
                PositionHelper.GetCoordinatesOnScreen(r: LineBonus.R, c: matrix.Columns - 1)
                );
        }

        private void GoUp()
        {
            destroyerOne = new Destroyer();
            destroyerOne.Move(
                PositionHelper.GetCoordinatesOnScreen(r: LineBonus.R - 1, c: LineBonus.C),
                PositionHelper.GetCoordinatesOnScreen(r: 0, c: LineBonus.C)
                );
        }

        private void GoDown()
        {
            destroyerTwo = new Destroyer();
            destroyerTwo.Move(
                PositionHelper.GetCoordinatesOnScreen(r: LineBonus.R + 1, c: LineBonus.C),

                PositionHelper.GetCoordinatesOnScreen(r: matrix.Rows - 1, c: LineBonus.C)

                );
        }

        public bool IsEnd()
        {
            return IsDestroyerOneEnd() && IsDestroyerTwoEnd();
        }

        private bool IsDestroyerOneEnd()
        {
            if(destroyerOne is null)
            {
                return true;
            }
            else 
            {
                return destroyerOne.State == DestroyerState.Empty;
            }
        }
       
        private bool IsDestroyerTwoEnd()
        {
            if (destroyerTwo is null)
            {
                return true;
            }
            else
            {
                return destroyerTwo.State == DestroyerState.Empty;
            }
        }
    }
}
