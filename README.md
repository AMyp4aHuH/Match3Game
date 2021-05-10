# Match3Game
This game is written in the Match3 genre and using the .NET MonoGame library.

The player's task is to create a sequence of three or more tiles of the same color by swiping or clicking on two adjacent tiles.

## Matrix
The main element of the game is a Matrix of Tiles.

Example:
```csharp
Matrix matrix = new Matrix(
  8,                          // Matrix size 8x8.
  0.7f,                       // Matrix scale relative to the smallest side of the game screen.
  0.9f,                       // Tile scale relative to the matrix cell.
  new Point(800, 480),        // Game window size.
  new DefaultTileFactory()    // Tile factory.
);

```

There are several types of tiles in the game, namely:
- Default tiles;
- Line bonus(vertical or horizontal) that releases black destroyers to the sides;
- Bomb bonus which after its destruction, destroys everything around in a 3x3 radius with itself in the center.

Example matrix with bonuses vertical line, horizontal line and bomb:

![Image alt](https://github.com/AMyp4aHuH/Match3Game/blob/master/Matrix.gif)

## Tiles
To use your own tiles, it is enough:
- Add a texture ("is a mesh" of square frames) to the [TextureProvider](../master/Providers/TextureProvider.cs) class;
- Create new types of [Tiles](../master/Matrix/Tiles/Tile.cs) by passing the texture to the constructor (and overriding the methods for changing frames);
- Create [TileFactory](../master/Matrix/Tiles/TileFactory.cs) and pass it to the constructor when creating the Matrix.

Remark: for bonuses, one more texture should be added - a detail that distinguishes the tile from the usual ones. For example, on the gif, you can see white vertical and horizontal lines.
