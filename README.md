# Match3Game
This game is written in the Match3 genre.
The Matrix is used as a playing field.
  
```csharp
Matrix matrix = new Matrix(
  8,                          // Matrix size 8x8.
  30,                         // Cell size 30x30.
  new Point(800, 480),        // Game window size.
  new DefaultTileFactory()    // Tile factory.
);

```

There are several types of tiles in the game, namely:
- Default tiles;
- Line bonus(vertical or horizontal) that releases black destroyers to the sides;
- Bomb bonus which after its destruction, destroys everything around in a 3x3 radius with itself in the center.

Example matrix with bonuses vertical line, horizontal line and bomb.

![Image alt](https://github.com/AMyp4aHuH/Match3Game/blob/master/Matrix.JPG)
