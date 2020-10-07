using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements
{
    /// <summary>
    /// Needed for convertation Cell in Vector and Vector in Cell. 
    /// </summary>
    public static class PositionConverter
    {
        public static Vector2 GetPositionsOnScreen(Cell cell)
        {
            Vector2 position = new Vector2(
                x: cell.C * Matrix.CellSize + Matrix.WidthIndent,
                y: cell.R * Matrix.CellSize + Matrix.HeightIndent
                );

            return position;
        }

        public static Vector2 GetPositionsOnScreen(int r, int c)
        {
            Cell cell = new Cell(r, c);
            return GetPositionsOnScreen(cell);
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
