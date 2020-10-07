
namespace Match3Game.MatrixElements
{
    public abstract class MatrixState
    {
        protected Matrix matrix;

        /// <summary> Need to change value to true when Timer expires to game over. </summary>
        public static bool GameOver = false;

        public MatrixState(Matrix matrix)
        {
            this.matrix = matrix;
        }

        public virtual void Update(int elapsedTime)
        {
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        if (matrix[i, j] != null)
                        {
                            matrix[i, j]?.Update(elapsedTime);
                        }
                    }
                }
            }
        }

    }
}
