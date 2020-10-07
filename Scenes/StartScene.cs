using Match3Game.Scenes.Adapters;
using Match3Game.View;
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
    public class StartScene : Scene
    { 
        public override void Load(ContentManager content)
        {
            Sprite background = new Sprite(TextureManager.Background);
            background.DisplayOnCenterScreen();
            AddGameElement(background);

            Button buttonPlay = new Button(TextureManager.ButtonBackground, FontManager.DefaultFont, "Play");
            buttonPlay.Scale = 0.5f;
            buttonPlay.DisplayOnCenterScreen();
            buttonPlay.Click += ButtonClickPlay;
            AddGameElement(buttonPlay);
        }

        public void ButtonClickPlay()
        {
            ScenesManager.ChangeScene<MainScene>();
        }
    }
}
