using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Interfaces
{
    public interface IGameElement
    {
        void Draw(SpriteBatch spriteBatch);

        void Update(int milliseconds);
    }
}
