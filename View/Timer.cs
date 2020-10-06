using Match3Game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.View
{
    public class Timer : IGameElement
    {
        //public delegate bool TimerEvent();
        //public TimerEvent TimerStoped;

        public Action Action;

        private int delayTime;
        public int DelayTime => delayTime;

        private bool isDone = false;
        private bool isTimerStoped = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delayTime"> Time after which the Action will occur. In milliseconds. </param>
        public Timer(int delayTime)
        {
            this.delayTime = delayTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(int elapsedTime)
        {
            if(!isDone)
            {
                delayTime -= elapsedTime;
                if (delayTime <= 0)
                {
                    delayTime = 0;
                    Action?.Invoke();
                    isDone = true;
                }
            }
        }
    }
}
