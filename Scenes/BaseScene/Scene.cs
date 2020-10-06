using Match3Game.Common;
using Match3Game.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Scenes
{
    public abstract class Scene : IGameElement
    {
        public static Point ScreenSize;
        public static GameAnalytics GameAnalytics;
        protected List<IGameElement> gameElements = new List<IGameElement>();

        public abstract void Load(ContentManager content);

        public void AddGameElement(IGameElement element) => gameElements.Add(element);

        public void RemoveGameElement(IGameElement element) => gameElements.Remove(element);

        public void Clear() => gameElements.Clear();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < gameElements.Count; i++)
            {
                gameElements[i].Draw(spriteBatch);
            }
        }

        public virtual void Update(int milliseconds)
        {
            for (var i = 0; i < gameElements.Count; i++)
            {
                gameElements[i].Update(milliseconds);
            }
        }
    }
}
