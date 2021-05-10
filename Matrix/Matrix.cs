using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Match3Game.Interfaces;
using Match3Game.Common;
using System.Collections.Generic;
using Match3Game.MatrixElements.Destroyers;
using System;

namespace Match3Game.MatrixElements
{
    public partial class Matrix : IGameElement
    {
        private Tile[,] data;

        public Tile this[int r, int c]
        {
            get
            {
                return data[r, c];
            }
            set
            {
                data[r, c] = value;
            }
        }

        public Tile this[Cell cell]
        {
            get
            {
                return this.data[cell.R, cell.C];
            }
            set
            {
                this.data[cell.R, cell.C] = value;
            }

        }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public static int CellSize { get; private set; }

        public MatrixState State { get; private set; }

        public static int HeightIndent;
        public static int WidthIndent;
        private Cell selectedCellStart;
        private Cell selectedCellEnd;
        private TileFactory tileFactory;
        private List<LineDestroyers> destroyers = new List<LineDestroyers>();

        /// <param name="size"> Rows or columns count. Matrix is always a cube. </param>
        /// <param name="matrixScale"> 
        /// Scale relative to the smallest side of the game screen. 
        /// For example, if Scale = 1 then the side of the matrix = the smallest side of the screen. 
        /// If you need to indent 5% (relative to the smallest side) from the near side of the screen, then set Scalee to 0.9f
        /// </param>
        /// <param name="tileScale">
        /// Tile scale relative to the matrix cell. For example, if scale = 1, then the tile will occupy the entire cell. 
        /// </param>
        /// <param name="screenSize"> Height and width screen. Needed for display matrix on center and matrix resize. </param>
        /// <param name="tileFactory"> Factory for creates tiles. </param>
        public Matrix(int size, float matrixScale, float tileScale, Point screenSize, TileFactory tileFactory)
        {
            ConfiguringMatrix(size, matrixScale, tileScale, screenSize, tileFactory);
        }

        public void ConfiguringMatrix(int size, float matrixScale, float tileScale, Point screenSize, TileFactory tileFactory)
        {
            if (matrixScale > 1 || matrixScale < 0.5)
            {
                throw new System.ArgumentException("Margin must be between 1 and 0,5.");
            }

            int minScreenSide = Math.Min(screenSize.X, screenSize.Y);

            CellSize = Convert.ToInt32((minScreenSide * matrixScale) / size);

            // We need tile for get his size.
            var defaultTile = tileFactory.CreateRandomTile(new Cell(0, 0));

            // Calculate and set scale for tiles.
            tileFactory.TileScale = ((float)CellSize / (float)defaultTile.Size) * (tileScale);

            this.tileFactory = tileFactory;
            this.tileFactory.tileDestroying += OnTileDestroying;

            Rows = size;
            Columns = size;

            data = new Tile[Rows, Columns];

            HeightIndent = (screenSize.Y - Rows * CellSize) / 2;
            WidthIndent = (screenSize.X - Columns * CellSize) / 2;

            State = new GenerateState(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (this[i, j] != null)
                    {
                        this[i, j].Draw(spriteBatch);
                    }
                }
            }

            if (destroyers.Count > 0)
            {
                for (int i = 0; i < destroyers.Count; i++)
                {
                    destroyers[i].Draw(spriteBatch);
                }
            }
        }
       
        public void Update(int elapsedTime)
        {
            State.Update(elapsedTime);
        }

        public void ChangeState(MatrixState state)
        {
            if(!(State is EmptyState))
            {
                State.StateEnd();
                State = state;
                State.StateStart();
            }
        }

        private void OnTileDestroying(object sender, TileEventArgs e)
        {
            if (e.State != TileState.Destroy && e.State != TileState.WaitDestroy)
            {
                GameAnalytics.Instance.AddScore(1);
                Cell positionInMatrix = e.PositionOnMatrix;
                switch (e.TileType)
                {
                    case TileType.HorizontalLine:
                        {
                            LineDestroyers lineDestroyer = 
                                new LineDestroyers(
                                    this, 
                                    new Cell(positionInMatrix.R, positionInMatrix.C, (Tile)sender),
                                    tileFactory
                                    );
                            destroyers.Add(lineDestroyer);
                            break;
                        }

                    case TileType.VerticalLine:
                        {
                            LineDestroyers lineDestroyer = 
                                new LineDestroyers(
                                    this, 
                                    new Cell(positionInMatrix.R, positionInMatrix.C, (Tile)sender),
                                    tileFactory
                                    );
                            destroyers.Add(lineDestroyer);
                            break;
                        }

                    case TileType.Bomb:
                        {
                            for (int i = positionInMatrix.R - 1; i <= positionInMatrix.R + 1; i++)
                            {
                                for (int j = positionInMatrix.C - 1; j <= positionInMatrix.C + 1; j++)
                                {
                                    if (i >= 0 &&
                                        j >= 0 &&
                                        i < Rows &&
                                        j < Columns)
                                    {
                                        if ( 
                                            this[i,j] != null && 
                                            this[i,j].State == TileState.Idle
                                            )
                                        {
                                            this[i, j].ChangeState(TileState.WaitDestroy);
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Execute when necessary to game over. Matrix change state on <see cref="EmptyState"/>
        /// </summary>
        public void GameOver()
        {
            ChangeState(new EmptyState(this));
        }
    }
}
