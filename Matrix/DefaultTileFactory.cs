using Match3Game.MatrixElements.DefaultTiles;
using Match3Game.MatrixElements.Destroyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.MatrixElements
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

        public override Tile CreateNextTile(Type type, Cell cell)
        {
            var index = Array.IndexOf(TileTypes, TileTypes.Where(t => t == type).FirstOrDefault());
            if (index < TileTypes.Length - 1)
            {
                return CreateTile(index + 1, cell);
            }
            else
            {
                return CreateTile(0, cell);
            }
        }

        private Tile CreateTile(int index, Cell cell)
        {
            Tile tile = CreateTile(TileTypes[index], cell);
            tile.Type = TileType.Default;
            return tile;
        }

        public override Tile CreateRandomTile(Cell cell)
        {
            Tile tile = CreateTile(TileTypes[rnd.Next(0, TileTypes.Length)], cell);
            return tile;
        }

        public override Destroyer CreateDestoyer()
        {
            var tile = new BlackDestroyer();
            tile.Scale = TileScale;
            return tile;
        }
    }
}
