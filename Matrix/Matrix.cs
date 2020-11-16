using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Match3Game.Interfaces;
using Match3Game.Common;
using System.Collections.Generic;
using Match3Game.MatrixElements.Destroyers;

namespace Match3Game.MatrixElements
{
    public partial class Matrix : IGameElement
    {
        private Tile[,] data;

        public Tile this[int r, int c]
        {
            get
            {
                return this.data[r, c];
            }
            set
            {
                this.data[r, c] = value;
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
        /// <param name="cellSize"> Pixels count on Hight or Width (because we have a square <see cref="Tile"/>). </param>
        /// <param name="screenSize"> Heiht and width screen. Needed for display matrix on centre. </param>
        /// <param name="tileFactory"> Factory for creates tiles. </param>
        public Matrix(int size, int cellSize, Point screenSize, TileFactory tileFactory)
        {
            CellSize = cellSize;
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
