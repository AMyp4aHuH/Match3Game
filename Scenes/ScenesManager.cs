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
        public static ContentManager Content { private get; set; }

        private static Scene currentScene;

        static ScenesManager()
        {
            currentScene = new StartScene();
            currentScene.Load(Content);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            currentScene?.Draw(spriteBatch);
        }

        public static void Update(int elapsedTime)
        {
            currentScene?.Update(elapsedTime);
        }

        public static void ChangeScene<T>() where T : Scene, new()
        {
            currentScene = new T();
            currentScene.Load(Content);
        }
    }
}
