using Match3Game.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Match3Game.Scenes
{
    public abstract class Scene : IComponent
    {
        public static Point ScreenSize;

        protected List<IGameElement> gameElements = new List<IGameElement>();

        public abstract void Load(ContentManager content);

        public void AddChild(IGameElement element) => gameElements.Add(element);

        public void RemoveChild(IGameElement element) => gameElements.Remove(element);

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
