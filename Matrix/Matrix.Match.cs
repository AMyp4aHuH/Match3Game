using System.Collections.Generic;
using System.Linq;

namespace Match3Game.MatrixElements
{
    public partial class Matrix
    {
        private class Match
        {
            private Matrix matrix;
            private List<Cell> specificTiles = new List<Cell>();

            public Match(Matrix matrix)
            {
                this.matrix = matrix;
            }

            /// <returns> List to destroy. </returns>
            public List<Cell> SearchMatchAfterSwap(Cell clickCellStart, Cell clickCellEnd)
            {
                var hMatch = new List<Cell>();
                var vMatch = new List<Cell>();

                // Search match with swap cells.
                SearchMatchesCell(clickCellStart, hMatch, vMatch);
                SearchMatchesCell(clickCellEnd, hMatch, vMatch);

                // Search intersect between H and V match for create specific tile "Bomb".
                var intersect = hMatch.Intersect(vMatch).ToList();

                if (intersect != null && intersect.Count > 0)
                {
                    for (int i = 0; i < intersect.Count; i++)
                    {
                        specificTiles.Add(
                        new Cell(
                            intersect[i].R,
                            intersect[i].C,
                            matrix.tileFactory.CreateBomb(
                                intersect[i].Tile.GetType(),
                                intersect[i]
                                )
                            )
                        );
                    }
                }
                var match = new List<Cell>();
                match.AddRange(hMatch);
                match.AddRange(vMatch);
                return match;
            }

            /// <returns> List to destroy. </returns>
            public List<Cell> SearchMatchAfterGenerate()
            {
                // Matches in all rows.
                var allHorizontalMatches = new List<Cell>();
                // Matches in all columns.
                var allVerticalMatches = new List<Cell>();

                // Search all matches (and scpecific tile)
                for (var i = 0; i < matrix.Rows; i++)
                {
                    var hMatch = new List<Cell>() { };
                    var vMatch = new List<Cell>() { };
                    var hBackTileType = matrix[i, 0].GetType();
                    var vBackTileType = matrix[0, i].GetType();

                    for (var j = 0; j < matrix.Columns; j++)
                    {
                        if (hBackTileType == matrix[i, j].GetType())
                        {
                            hMatch.Add(new Cell(i, j, matrix[i, j]));
                        }
                        else
                        {
                            hBackTileType = matrix[i, j].GetType();
                            if (hMatch.Count >= 3)
                            {
                                allHorizontalMatches.AddRange(hMatch);
                                SearchSpecificMatch(hMatch, hMatch.FirstOrDefault());
                            }
                            hMatch = new List<Cell>() { new Cell(i, j, matrix[i, j]) };
                        }

                        if (vBackTileType == matrix[j, i].GetType())
                        {
                            vMatch.Add(new Cell(j, i, matrix[j, i]));
                        }
                        else
                        {
                            vBackTileType = matrix[j, i].GetType();
                            if (vMatch.Count >= 3)
                            {
                                allVerticalMatches.AddRange(vMatch);
                                SearchSpecificMatch(vMatch, vMatch.FirstOrDefault());
                            }
                            vMatch = new List<Cell>() { new Cell(j, i, matrix[j, i]) };
                        }
                    }

                    // Occur when Tiles in the Column and Row end.
                    if (hMatch.Count >= 3)
                    {
                        allHorizontalMatches.AddRange(hMatch);
                        SearchSpecificMatch(hMatch, hMatch.FirstOrDefault());
                    }
                    if (vMatch.Count >= 3)
                    {
                        allVerticalMatches.AddRange(vMatch);
                        SearchSpecificMatch(vMatch, vMatch.FirstOrDefault());
                    }
                }

                // Search common tiles between H and V matches for create tile "Bomb"
                var intersect = allHorizontalMatches.Intersect(allVerticalMatches).ToList();

                if (intersect.Count > 0)
                {
                    foreach (var cell in intersect)
                    {
                        Tile tile = matrix.tileFactory.CreateBomb(cell.Tile.GetType(), cell);
                        specificTiles.Add(new Cell(cell.R, cell.C, tile));
                    }
                }

                var result = new List<Cell>();
                result.AddRange(allHorizontalMatches);
                result.AddRange(allVerticalMatches);

                return result;
            }

            public List<Cell> GetSpecificTiles()
            {
                return specificTiles;
            }

            private void SearchMatchesCell(Cell cell, List<Cell> horizontalMatches, List<Cell> verticalMatches)
            {
                horizontalMatches.AddRange(
                    SearchHorizontalMatch());

                verticalMatches.AddRange(
                    SearchVerticalMatch());

                List<Cell> SearchHorizontalMatch()
                {
                    List<Cell> result = new List<Cell>() { cell };
                    // Horizontal offset.
                    int offset = 1;

                    while (cell.C + offset < matrix.Columns)
                    {
                        if (matrix[cell.R, cell.C].GetType() == matrix[cell.R, cell.C + offset].GetType())
                        {
                            result.Add(new Cell(cell.R, cell.C + offset, matrix[cell.R, cell.C + offset]));
                            offset++;
                        }
                        else
                        {
                            offset = 1;
                            break;
                        }
                    }
                    offset = 1;
                    while (cell.C - offset >= 0)
                    {
                        if (matrix[cell.R, cell.C].GetType() == matrix[cell.R, cell.C - offset].GetType())
                        {
                            result.Add(new Cell(cell.R, cell.C - offset, matrix[cell.R, cell.C - offset]));
                            offset++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (result.Count < 3)
                    {
                        result = new List<Cell>();
                    }
                    else
                    {
                        SearchSpecificMatch(result, cell);
                    }

                    return result;
                }
                List<Cell> SearchVerticalMatch()
                {
                    List<Cell> result = new List<Cell>() { cell };
                    // Vertical offset.
                    int offset = 1;

                    while (cell.R + offset < matrix.Rows)
                    {
                        if (matrix[cell.R, cell.C] != null && matrix[cell.R + offset, cell.C] != null)
                        {
                            if (matrix[cell.R, cell.C].GetType() == matrix[cell.R + offset, cell.C].GetType())
                            {
                                result.Add(new Cell(cell.R + offset, cell.C, matrix[cell.R + offset, cell.C]));
                                offset++;
                            }
                            else
                            {
                                offset = 1;
                                break;
                            }
                        }
                    }
                    offset = 1;
                    while (cell.R - offset >= 0)
                    {
                        if (matrix[cell.R, cell.C] != null &&
                           matrix[cell.R - offset, cell.C] != null)
                        {
                            if (matrix[cell.R, cell.C].GetType() == matrix[cell.R - offset, cell.C].GetType())
                            {
                                result.Add(new Cell(cell.R - offset, cell.C, matrix[cell.R - offset, cell.C]));
                                offset++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    if (result.Count < 3)
                    {
                        result = new List<Cell>();
                    }
                    else
                    {
                        SearchSpecificMatch(result, cell);
                    }
                    return result;
                }
            }

            /// <summary> Search combination for create specific Tile "Line" or "Bomb". </summary>
            /// <param name="cell"> Сell where the Tile will be placed. </param>
            private void SearchSpecificMatch(List<Cell> matchList, Cell cell)
            {
                if (matchList.Count == 4)
                {
                    var firstCell = matchList.FirstOrDefault();
                    var lastCell = matchList.LastOrDefault();

                    if (firstCell.R == lastCell.R)
                    {
                        specificTiles.Add(
                            new Cell(
                                cell.R,
                                cell.C,
                                matrix.tileFactory.CreateVerticalLineBonus(
                                    lastCell.Tile.GetType(), 
                                    new Cell(cell.R, cell.C)
                                    )
                                ));
                    }
                    else
                    {
                        specificTiles.Add(
                            new Cell(
                                cell.R,
                                cell.C,
                                matrix.tileFactory.CreateHorizontalLineBonus(
                                    lastCell.Tile.GetType(), 
                                    new Cell(cell.R, cell.C)
                                    )
                                ));
                    }
                }
                else if (matchList.Count >= 5)
                {
                    specificTiles.Add(
                        new Cell(
                            cell.R,
                            cell.C,
                            matrix.tileFactory.CreateBomb(
                                cell.Tile.GetType(),
                                new Cell(cell.R, cell.C)
                                )
                            ));
                }
            }
        }

    }
}
