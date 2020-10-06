
using Microsoft.Xna.Framework;

namespace Match3Game.Matrix
{
    public class TileEventArgs
    {
        public Vector2 PositionOnScreen { get; }

        public TileType TileType { get; }

        public TileState State { get; }

        public Cell PositionOnMatrix { get; }

        public TileEventArgs(Vector2 positionOnScreen, TileType tileType, TileState state, Cell positionOnMatrix)
        {
            PositionOnScreen = positionOnScreen;
            TileType = tileType;
            State = state;
            PositionOnMatrix = positionOnMatrix;
        }
    }
}
