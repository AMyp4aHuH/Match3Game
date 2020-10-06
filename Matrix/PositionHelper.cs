using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Matrix
{
    /// <summary>
    /// Needed for convertation Cell in Vector and Vector in Cell. 
    /// </summary>
    public static class PositionHelper
    {
        public static Vector2 GetCoordinatesOnScreen(Cell cell)
        {
            Vector2 position = new Vector2(
                x: cell.C * Matrix.CellSize + Matrix.WidthIndent,
                y: cell.R * Matrix.CellSize + Matrix.HeightIndent
                );

            return position;
        }

        public static Vector2 GetCoordinatesOnScreen(int r, int c)
        {
            Cell cell = new Cell(r, c);
            return GetCoordinatesOnScreen(cell);
        }

        /// <summary>
        /// Only use when we search cell by position Tile.
        /// </summary>
        public static Cell GetCellByTilePosition(Vector2 position)
        {
            int r = (int)((position.Y - Matrix.HeightIndent + Matrix.CellSize / 2) / Matrix.CellSize) ;
            int c = (int)((position.X - Matrix.WidthIndent + Matrix.CellSize / 2 ) / Matrix.CellSize) ;

            return new Cell(r, c);
        }

        public static Cell GetCellByClickPosition(Vector2 position)
        {
            int r = (int)((position.Y - Matrix.HeightIndent) / Matrix.CellSize);
            int c = (int)((position.X - Matrix.WidthIndent) / Matrix.CellSize);

            return new Cell(r, c);
        }
    }
}
