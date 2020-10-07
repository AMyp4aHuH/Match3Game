using Match3Game.MatrixElements.Destroyers;
using Match3Game.Providers;
using System;

namespace Match3Game.MatrixElements
{
    public abstract class TileFactory
    {
        public Tile.TileDestroy tileDestroying;

        public abstract Tile CreateNextTile(Type type, Cell cell);

        public abstract Tile CreateRandomTile(Cell cell);

        public Tile CreateTile(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.Default;
            return tile;
        }

        public Tile CreateVerticalLineBonus(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.VerticalLine;
            tile.SetDetail(TextureProvider.VerticaDefaultlLineTexture);
            return tile;
        }

        public Tile CreateHorizontalLineBonus(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.HorizontalLine;
            tile.SetDetail(TextureProvider.HorizontalDefaultLineTexture);
            return tile;
        }

        public Tile CreateBomb(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.Bomb;
            tile.SetDetail(TextureProvider.BombDefaultTexture);
            return tile;
        }

        public Destroyer CreateDestoyer() => new Destroyer(); 
    }
}
