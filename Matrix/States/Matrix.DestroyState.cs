
namespace Match3Game.MatrixElements
{
    public partial class Matrix
    {
        public class DestroyState : MatrixState
        {
            private MatrixState oldState;
            private bool IsMatchSwap = false;
            private bool IsMatchAllMatrix = false;

            public DestroyState(Matrix matrix, MatrixState oldState) : base(matrix)
            {
                this.oldState = oldState;
                Match match = new Match(matrix);

                if(oldState is GenerateState) 
                {
                    var matches = match.SearchMatchAfterGenerate();

                    if (matches.Count > 0) 
                    {
                        IsMatchAllMatrix = true;
                        foreach (var cell in matches) 
                        {
                            cell.Tile.ChangeState(TileState.Destroy);
                        }
                        var specificTiles = match.GetSpecificTiles();
                        if(specificTiles.Count > 0) 
                        {
                            foreach (var cell in specificTiles)
                            {
                                matrix[cell] = cell.Tile;
                            }
                        }
                    }
                }
                else if(oldState is SwapState)
                {
                    var matches = match.SearchMatchAfterSwap(matrix.selectedCellStart, matrix.selectedCellEnd);

                    if (matches.Count > 0)
                    {
                        IsMatchSwap = true;
                        foreach (var cell in matches)
                        {
                            cell.Tile.ChangeState(TileState.Destroy);
                        }
                    }
                    var specificTiles = match.GetSpecificTiles();
                    if (specificTiles.Count > 0)
                    {
                        foreach (var cell in specificTiles)
                        {
                            matrix[cell] = cell.Tile;
                        }
                    }
                }
            }

            public override void Update(int elapsedTime)
            {
                bool IsNextState = true;
                for (var i = 0; i < matrix.Rows; i++)
                {
                    for (var j = 0; j < matrix.Columns; j++)
                    {
                        if (matrix[i, j] != null)
                        {
                            matrix[i, j].Update(elapsedTime);
                            IsNextState = (matrix[i, j].IsDestroy() || matrix[i, j].IsWaitDestroy() ) ? false : IsNextState;
                            if(matrix[i, j].IsEmpty())
                            {
                                matrix[i, j] = null;
                            }  
                        }
                    }
                }

                bool IsDestroyersEnd = true;
                if (matrix.destroyers.Count > 0)
                {
                    for (int i = 0; i < matrix.destroyers.Count; i++)
                    {
                        matrix.destroyers[i].Update(elapsedTime);
                        IsDestroyersEnd = matrix.destroyers[i].IsEnd();
                    }
                }

                if(IsNextState && IsDestroyersEnd)
                {
                    matrix.destroyers.Clear();
                    NextState();
                }
            }

            private void NextState()
            {
                if(matrix.State is EmptyState)
                {
                    // To do nothing. Game over.
                }
                else if (oldState is SwapState && IsMatchSwap)
                {
                    matrix.ChangeState(new GenerateState(matrix, this));
                }
                else if (oldState is SwapState && IsMatchSwap == false)
                {
                    matrix.ChangeState(new SwapState(matrix, this));
                }
                else if(oldState is GenerateState && IsMatchAllMatrix)
                {
                    matrix.ChangeState(new GenerateState(matrix, this));
                }
                else if(oldState is GenerateState && IsMatchAllMatrix == false)
                {
                    matrix.ChangeState(new IdleState(matrix));
                }
            }
        }
    }
}
