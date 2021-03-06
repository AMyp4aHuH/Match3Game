﻿using Match3Game.Controls;
using Match3Game.MatrixElements;
using Match3Game.MatrixElements.DefaultTiles;
using Match3Game.Providers;
using Match3Game.Sprites;
using Microsoft.Xna.Framework.Content;

namespace Match3Game.Scenes
{
    public class StartScene : Scene
    { 
        public override void Load(ContentManager content)
        {
            Sprite background = new Sprite(TextureProvider.Common.Background);
            background.DisplayOnCenterScreen();
            AddChild(background);

            Button buttonPlay = new Button(TextureProvider.Common.ButtonBackground, FontProvider.DefaultFont, "Play");
            buttonPlay.Scale = 0.5f;
            buttonPlay.DisplayOnCenterScreen();
            buttonPlay.Click += ButtonClickPlay;
            AddChild(buttonPlay);
        }

        public void ButtonClickPlay()
        {
            ScenesManager.ChangeScene<MainScene>();
        }
    }
}
