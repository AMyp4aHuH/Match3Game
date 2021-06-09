using Match3Game.Providers;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Match3Game.Sprites;

namespace Match3Game.MatrixElements.Destroyers
{
    public class Destroyer : Tile
    {
        public DestroyerState State = DestroyerState.Empty;
        public Cell LastCellCollision;

        public override void Update(int milliseconds)
        {
            switch(State)
            {
                case DestroyerState.Create:
                    {
                        if (!animationManager.Play(AnimationState.Create ,milliseconds))
                        {
                            State = DestroyerState.Move;
                        }
                        break;
                    }
                case DestroyerState.Destroy:
                    {
                        if (!animationManager.Play(AnimationState.Destroy, milliseconds))
                        {
                            State = DestroyerState.Empty;
                            animationManager.Stop();
                        }
                        break;
                    }
                case DestroyerState.Move:
                    {
                        animationManager.Play(AnimationState.Idle, milliseconds);

                        if (movePositions.Count == 0)
                        {
                            State = DestroyerState.Destroy;
                        }
                        else
                        {
                            PositionOnScreen += movePositions.Dequeue();
                        }
                        break;
                    }
                case DestroyerState.Empty:
                    {
                        // To do nothing.
                        break;
                    }
            }
        }

        public void Move(Vector2 start, Vector2 end)
        {
            State = DestroyerState.Create;

            var windthIndentInsideCell = (Matrix.CellSize - animationManager.Size * Scale) / 2;
            var heightIndentInsideCell = (Matrix.CellSize - animationManager.Size * Scale) / 2;

            start += new Vector2(windthIndentInsideCell, heightIndentInsideCell);
            end += new Vector2(windthIndentInsideCell, heightIndentInsideCell);

            PositionOnScreen = start;

            float[] positions;
            Vector2 diffeneceVec = start - end;

            if(diffeneceVec != Vector2.Zero)
            {
                if ((Math.Abs(diffeneceVec.X) > Math.Abs(diffeneceVec.Y)))
                {
                    // Horizontal.
                    positions = FillMoveQueue(distance: (int)Math.Abs(diffeneceVec.X));

                    if (diffeneceVec.X > 0)
                    {
                        // Left.
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            movePositions.Enqueue(new Vector2(positions[i] * (-1), 0));
                        }
                    }
                    else
                    {
                        // Right.
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            movePositions.Enqueue(new Vector2(positions[i], 0));
                        }
                    }
                }
                else
                {
                    // Vertical
                    positions = FillMoveQueue(distance: (int)Math.Abs(diffeneceVec.Y));

                    if(diffeneceVec.Y > 0)
                    {
                        // Down.
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            movePositions.Enqueue(new Vector2(0, positions[i] * (-1)));
                        }
                    }
                    else
                    {
                        // Up.
                        for (int i = 0; i < positions.Count(); i++)
                        {
                            movePositions.Enqueue(new Vector2(0, positions[i]));
                        }
                    }
                }
            }
        }
    }
}
