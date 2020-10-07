using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Match3Game.Common
{
    public static class MouseClickDetector
    {
        public delegate void MouseClick(object sender, MouseClickEventArgs args);
        public delegate void MouseSwipe(object sender, MouseSwipeEventArgs args);

        public static event MouseClick LeftButtonDown;
        public static event MouseClick LeftButtonUp;
        public static event MouseSwipe LeftButtonSwipe;

        private static Vector2 downClickPosition = new Vector2(-1, -1);
        private const int distanceForDetectedSwipe = 20;
        private static bool mouseReleased = true;

        public static void Update()
        {
            MouseState mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed)
            {
                if (mouseReleased == true)
                {
                    // Left button down!
                    mouseReleased = false;
                    downClickPosition = new Vector2(mState.Position.X, mState.Position.Y);
                    LeftButtonDown?.Invoke(
                    null,
                    new MouseClickEventArgs(new Vector2(mState.Position.X, mState.Position.Y))
                );
                }
                // Swipe on the screen.
                else
                {
                    // Left button down and swipe!
                    if (downClickPosition.X > 0)
                    {
                        var distance = Vector2.Distance(downClickPosition, new Vector2(mState.Position.X, mState.Position.Y));
                        if (distance > distanceForDetectedSwipe)
                        {
                            Direction direction = GetSwipeDirection(downClickPosition, new Vector2(mState.Position.X, mState.Position.Y));
                            LeftButtonSwipe?.Invoke(
                                null, 
                                new MouseSwipeEventArgs(
                                    downClickPosition,
                                    new Vector2(mState.Position.X, mState.Position.Y),
                                    direction
                                    ) { }
                                );
                        }
                    }
                }
            }
            else if(mState.LeftButton == ButtonState.Released && mouseReleased == false)
            {
                // Left button up!
                mouseReleased = true;
                downClickPosition = new Vector2(-1, -1);
                LeftButtonUp?.Invoke(
                    null, 
                    new MouseClickEventArgs( new Vector2(mState.Position.X, mState.Position.Y) )
                );
            }
        }
        
        private static Direction GetSwipeDirection(Vector2 start, Vector2 end)
        {
            Direction result;
            var xDifference = start.X - end.X;
            var yDifference = start.Y - end.Y;

            if (Math.Abs(xDifference) == 1 || Math.Abs(yDifference) == 1)
            {

            }
            if (Math.Abs(xDifference) > Math.Abs(yDifference))
            {
                // Horizontal swipe.
                result = (xDifference > 0) ? Direction.Left : Direction.Right;
            }
            else
            {
                // Vertical swipe.
                result = (yDifference > 0) ? Direction.Up : Direction.Down;
            }

            return result;
        }

        /// <summary>
        /// Clear all subscribers.
        /// </summary>
        public static void Clear()
        {
            LeftButtonDown = null;
            LeftButtonUp = null;
            LeftButtonSwipe = null;
        }
    }

    public class MouseClickEventArgs
    {
        public MouseClickEventArgs(Vector2 clickPosition) => ClickPosition = clickPosition;
        
        public readonly Vector2 ClickPosition;
    }

    public class MouseSwipeEventArgs
    {
        public MouseSwipeEventArgs(Vector2 downClickPosition, Vector2 upClickPosition, Direction swipeDirection)
        {
            DownClickPosition = downClickPosition;
            UpClickPosition = upClickPosition;
            SwipeDirection = swipeDirection;
        }

        public readonly Vector2 DownClickPosition;
        public readonly Vector2 UpClickPosition;
        public readonly Direction SwipeDirection;
    }
}
