
namespace Match3Game.Matrix
{
    public abstract class State
    {
        protected Matrix matrix;

        /// <summary> Need to change true when Timer expires to game over. </summary>
        public static bool GameOver = false;

        public State(Matrix matrix)
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
