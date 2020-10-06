using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Matrix
{
    public partial class Matrix
    {
        public class Match
        {
            private Matrix matrix;
            private List<Cell> specificTiles = new List<Cell>();

            public Match(Matrix matrix)
            {
                this.matrix = matrix;
            }

            /// <returns> Destroy list. </returns>
            public List<Cell> SearchMatchAfterSwap(Cell clickCellStart, Cell clickCellEnd)
            {
                var hMatch = new List<Cell>();
                var vMatch = new List<Cell>();

                // Search match with swap cells.
                SearchMatchCell(clickCellStart, hMatch, vMatch);
                SearchMatchCell(clickCellEnd, hMatch, vMatch);

                // Search intersect between H and V match for create specific tile "Bomb".
                var intersect = hMatch.Intersect(vMatch).FirstOrDefault();

                if (intersect != null)
                {
                    specificTiles.Add(
                        new Cell(
                            intersect.R, 
                            intersect.C, 
                            matrix.tileFactory.GetBomb(
                                intersect.Tile.GetType(), 
                                intersect
                                )
                            )
                        );
                }
                var match = new List<Cell>();
                match.AddRange(hMatch);
                match.AddRange(vMatch);
                return match;
            }

            /// <returns> Destroy list. </returns>
            public List<Cell> SearchMatchAfterGenerate()
            {
                // Matches in all rows.
                var allHorizontalMatch = new List<Cell>();
                // Matches in all columns.
                var allVerticalMatch = new List<Cell>();

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
                                allHorizontalMatch.AddRange(hMatch);
                                // Search combination for create specific tile "Line" and "Bomb".
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
                                allVerticalMatch.AddRange(vMatch);
                                // Search combination for create specific tile "Line" and "Bomb".
                                SearchSpecificMatch(vMatch, vMatch.FirstOrDefault());
                            }
                            vMatch = new List<Cell>() { new Cell(j, i, matrix[j, i]) };
                        }
                    }

                    if (hMatch.Count >= 3)
                    {
                        allHorizontalMatch.AddRange(hMatch);
                        SearchSpecificMatch(hMatch, hMatch.FirstOrDefault());
                    }
                    if (vMatch.Count >= 3)
                    {
                        allVerticalMatch.AddRange(vMatch);
                        SearchSpecificMatch(vMatch, vMatch.FirstOrDefault());
                    }
                }

                // Search common tiles between H and V matches for create tile "Bomb"
                var intersect = allHorizontalMatch.Intersect(allVerticalMatch).ToList();

                if (intersect.Count > 0)
                {
                    foreach (var cell in intersect)
                    {
                        Tile tile = matrix.tileFactory.GetBomb(cell.Tile.GetType(), cell);
                        specificTiles.Add(new Cell(cell.R, cell.C, tile));
                    }
                }

                var result = new List<Cell>();
                result.AddRange(allHorizontalMatch);
                result.AddRange(allVerticalMatch);

                return result;
                // return new List<Cell>();
            }

            public List<Cell> GetSpecificTiles()
            {
                return specificTiles;
            }

            private void SearchMatchCell(Cell cell, List<Cell> horizontalMatch, List<Cell> verticalMatch)
            {
                // Check match StartCell.
                horizontalMatch.AddRange(
                    SearchHorizontalMatch());

                verticalMatch.AddRange(
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
                                matrix.tileFactory.GetVerticalLineBonus(lastCell.Tile.GetType(), new Cell(cell.R, cell.C))
                                ));
                    }
                    else
                    {
                        specificTiles.Add(
                            new Cell(
                                cell.R,
                                cell.C,
                                matrix.tileFactory.GetHorizontalLineBonus(lastCell.Tile.GetType(), new Cell(cell.R, cell.C))
                                ));
                    }
                }
                else if (matchList.Count >= 5)
                {
                    specificTiles.Add(
                        new Cell(
                            cell.R,
                            cell.C,
                            matrix.tileFactory.GetBomb(cell.Tile.GetType(),new Cell(cell.R, cell.C))
                            ));
                }
            }
        }

    }
}
