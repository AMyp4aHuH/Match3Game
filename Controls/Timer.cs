using Match3Game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3Game.Controls
{
    public class Timer : IGameElement
    {
        public Action Action;

        public int DelayTime { get; private set; }

        private bool isDone = false;

        /// <param name="delayTime"> Time after which the Action will occur. In milliseconds. </param>
        public Timer(int delayTime)
        {
            this.DelayTime = delayTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(int elapsedTime)
        {
            if(!isDone)
            {
                DelayTime -= elapsedTime;
                if (DelayTime <= 0)
                {
                    DelayTime = 0;
                    isDone = true;
                    Action?.Invoke();
                }
            }
        }
    }
}
