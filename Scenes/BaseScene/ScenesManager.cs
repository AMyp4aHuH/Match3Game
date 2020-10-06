using Match3Game.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Scenes
{
    public static class ScenesManager
    {
        private static ContentManager content;
        public static ContentManager Content { set => content = value; }

        private static Scene currentScene;

        static ScenesManager()
        {
            currentScene = new StartScene();
            currentScene.Load(content);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            currentScene?.Draw(spriteBatch);
        }

        public static void Update(int milliseconds)
        {
            currentScene?.Update(milliseconds);
        }

        public static void ChangeScene<T>() where T : Scene, new()
        {
            currentScene = new T();
            currentScene.Load(content);
        }
    }
}
