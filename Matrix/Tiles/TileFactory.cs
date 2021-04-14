using Match3Game.MatrixElements.Destroyers;
using Match3Game.Providers;
using System;

namespace Match3Game.MatrixElements
{
    public abstract class TileFactory
    {
        public Tile.TileDestroy tileDestroying;

        public float TileScale = 1f;

        public abstract Tile CreateNextTile(Type type, Cell cell);

        public abstract Tile CreateRandomTile(Cell cell);

        public Tile CreateTile(Type type, Cell cell, TileType tileType = TileType.Default)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.Type = tileType;
            tile.Scale = TileScale;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            return tile;
        }

        public Tile CreateVerticalLineBonus(Type type, Cell cell)
        {
            Tile tile = CreateTile(type, cell);
            tile.Type = TileType.VerticalLine;
            tile.SetDetail(TextureProvider.VerticaDefaultlLineTexture);
            return tile;
        }

        public Tile CreateHorizontalLineBonus(Type type, Cell cell)
        {
            Tile tile = CreateTile(type, cell);
            tile.Type = TileType.HorizontalLine;
            tile.SetDetail(TextureProvider.HorizontalDefaultLineTexture);
            return tile;
        }

        public Tile CreateBomb(Type type, Cell cell)
        {
            Tile tile = CreateTile(type, cell);
            tile.Type = TileType.Bomb;
            tile.SetDetail(TextureProvider.BombDefaultTexture);
            return tile;
        }

        public Destroyer CreateDestoyer() 
        {
            var tile = new Destroyer();
            tile.Scale = TileScale;
            return tile;
        }
    }
}
