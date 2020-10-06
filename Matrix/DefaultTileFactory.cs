using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Matrix
{
    public class DefaultTileFactory : TileFactory
    {
        private Random rnd = new Random();

        private readonly Type[] TileTypes = {
            typeof(YellowTile),
            typeof(GreenTile),
            typeof(PinkTile),
            typeof(PurpleTile),
            typeof(BlueTile)
        };

        public override Tile GetNextTile(Type type, Cell cell)
        {
            var index = Array.IndexOf(TileTypes, TileTypes.Where(t => t == type).FirstOrDefault());

            Tile tile;
            if (index < TileTypes.Length - 1)
            {
                tile = Activator.CreateInstance(TileTypes[index + 1]) as Tile;
                tile.SetPosition(cell);
                tile.Destroying += tileDestroying;
                tile.Type = TileType.Default;
                return tile;
            }
            else
            {
                tile = Activator.CreateInstance(TileTypes[0]) as Tile;
                tile.SetPosition(cell);
                tile.Destroying += tileDestroying;
                tile.Type = TileType.Default;
                return tile;
            }
        }

        public override Tile GetRandomTile(Cell cell)
        {
            Tile tile = Activator.CreateInstance(TileTypes[rnd.Next(0, TileTypes.Length)]) as Tile;
            tile.SetPosition(cell);
            tile.Destroying += tileDestroying;
            return tile;
        }
    }
}
