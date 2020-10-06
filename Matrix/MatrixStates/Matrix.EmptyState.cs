using System;

namespace Match3Game.Matrix
{
    public partial class Matrix
    {
        public class EmptyState : State
        {
            public EmptyState(Matrix matrix) : base(matrix)
            {

            }

            public override void Update(int elapsedTime)
            {
                for (var i = 0; i < matrix.Rows; i++)
                {
                    for (var j = 0; j < matrix.Columns; j++)
                    {
                        matrix[i, j]?.Update(elapsedTime);
                    }
                }
            }
        }
    }
}
