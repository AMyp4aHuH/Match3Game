using System.Collections.Generic;

namespace Match3Game.MatrixElements
{
    public partial class Matrix
    {
        public class GenerateState : MatrixState
        {
            public GenerateState(Matrix matrix) : base(matrix)
            {
                // This will work when creating the matrix.
                Generate();
            }

            public GenerateState(Matrix matrix, MatrixState oldState) : base(matrix)
            {
                Generate();
            }

            public override void Update(int elapsedTime)
            {
                bool IsGenerate = false;

                for (var i = 0; i < matrix.Rows; i++)
                {
                    for (var j = 0; j < matrix.Columns; j++)
                    {
                        if(matrix[i, j] != null)
                        {
                            matrix[i, j].Update(elapsedTime);
                            if (matrix[i, j].IsMove())
                            {
                                IsGenerate = true;
                            }
                        } 
                    }
                }

                if (!IsGenerate)
                {
                    NextState();
                }
            }

            public void Generate()
            {
                Queue<Cell> NullTiles = new Queue<Cell>();
                
                for (var j = 0; j < matrix.Columns; j++)
                {
                    for (var i = matrix.Rows - 1; i >= 0; i--)
                    {
                        if(matrix[i,j] == null)
                        {
                            NullTiles.Enqueue(new Cell(i, j));
                        }
                        else
                        {
                            if (NullTiles.Count > 0)
                            {
                                Cell cell = NullTiles.Dequeue();
                                matrix[cell] = matrix[i, j];
                                matrix[cell].SetPosition(cell);
                                matrix[cell].MoveExistingTileDuringGeneration(i, cell.R);
                                matrix[i, j] = null;
                                NullTiles.Enqueue(new Cell(i, j));
                            }
                        }
                    }

                    int verticalOffest = 1;
                    while (NullTiles.Count > 0)
                    {
                        Cell cell = NullTiles.Dequeue();
                        matrix[cell] = matrix.tileFactory.CreateRandomTile(cell);
                        matrix[cell].MoveNewTileDuringGeneration(cell.R + verticalOffest);
                        verticalOffest++;
                    }
                }
            }

            /// <summary> Changes the current state to <see cref="Matrix.DestroyState"/> </summary>
            private void NextState()
            {
                // We can do something here if needed when changing state.
                matrix.ChangeState(new DestroyState(matrix, this));
            }
        } 
    }
}
