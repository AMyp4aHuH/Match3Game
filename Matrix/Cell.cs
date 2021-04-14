using System;

namespace Match3Game.MatrixElements
{
    public class Cell : IEquatable<Cell>
    {
        public int R;
        public int C;
        public Tile Tile;

        public Cell(int R, int C)
        {
            this.R = R;
            this.C = C;
        }

        public Cell(int R, int C, Tile Tile): this(R, C)
        {
            this.Tile = Tile;
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
