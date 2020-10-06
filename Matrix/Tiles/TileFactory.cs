using System;

namespace Match3Game.Matrix
{
    public abstract class TileFactory
    {
        public Tile.TileDestroy tileDestroying;

        public abstract Tile GetNextTile(Type type, Cell cell);

        public abstract Tile GetRandomTile(Cell cell);

        public Tile GetTile(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.Default;
            return tile;
        }

        public Tile GetVerticalLineBonus(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.VerticalLine;
            tile.SetDetail(TextureManager.VerticaDefaultlLineTexture);
            return tile;
        }

        public Tile GetHorizontalLineBonus(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.HorizontalLine;
            tile.SetDetail(TextureManager.HorizontalDefaultLineTexture);
            return tile;
        }

        public Tile GetBomb(Type type, Cell cell)
        {
            Tile tile = Activator.CreateInstance(type) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            tile.Type = TileType.Bomb;
            tile.SetDetail(TextureManager.BombDefaultTexture);
            return tile;
        }

        public Destroyer CreateDestoyer() => new Destroyer(); 
    }
}
