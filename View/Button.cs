using Match3Game.Common;
using Match3Game.Interfaces;
using Match3Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.View
{
    public class Button : Sprite, IGameElement
    {
        public Action Click;

        public Button(Texture2D texture) : base (texture)
        {
            MouseClickDetector.LeftButtonDown += Notify;
        }

        public void Notify(object sender, MouseClickEventArgs args)
        {
            if(Rectangle.Intersects(new Rectangle((int)args.ClickPosition.X, (int)args.ClickPosition.Y, 1, 1)))
            {
                // TODO: Make a button for many clicks.
                MouseClickDetector.LeftButtonDown -= Notify;
                Click?.Invoke();
            }
        }
    }
}
