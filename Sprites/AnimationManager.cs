using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game
{
    public class AnimationManager<T>
    {
        private Dictionary<T, Animation> animations = new Dictionary<T, Animation>();
        private Animation currentAnimation;
        public Texture2D Texture => currentAnimation?.Texture;
        public Texture2D DetailTexture => currentAnimation?.DetailTexture;

        public int Size
        {
            get 
            { 
                if(currentAnimation is null)
                {
                    return animations.Count > 0 ? animations.FirstOrDefault().Value.FrameHeight : 0;
                }

                return currentAnimation.FrameHeight; 
            }
        }
        
        public Rectangle Frame => currentAnimation.CurrentFrame;
        
        public bool Play(T animantionName, float elapsedTime)
        {
            if(animations.TryGetValue(animantionName, out Animation animation))
            {
                if (currentAnimation != animation)
                {
                    var oldAnim = currentAnimation;
                    currentAnimation = animation;
                    oldAnim?.Reset();
                }
                else
                {
                    return animation.NextFrame(elapsedTime);
                }
            }
            else
            {
                throw new Exception($"Animation {typeof(T)}:{animantionName} not exist.");
            }

            return true;
        }

        public void Stop()
        {
            //NRE

            currentAnimation = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="animation"></param>
        /// <param name="setAnimationNow"> Set this animation on current animation. </param>
        public void AddAnimation(T key, Animation animation, bool setCurrentAnimation = false)
        {
            if (setCurrentAnimation)
            {
                currentAnimation = animation;
            }

            animations.Add(key, animation);
        }
    }
}
