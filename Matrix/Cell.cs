using System;

namespace Match3Game.MatrixElements
{
    public class Cell : IEquatable<Cell>
    {
        public int R { get; private set; }
        public int C { get; private set; }
        public Tile Tile;

        public Cell(int r, int c)
        {
            R = r;
            C = c;
        }

        public Cell(int r, int c, Tile tile): this(r, c)
        {
            Tile = tile;
        }

        public bool Equals(Cell other)
        {
            if (other is null)
                return false;

            return R == other.R && C == other.C;
        }

        public override bool Equals(object obj) => Equals(obj as Cell);
        public override int GetHashCode() => (R, C).GetHashCode();
    }
}
