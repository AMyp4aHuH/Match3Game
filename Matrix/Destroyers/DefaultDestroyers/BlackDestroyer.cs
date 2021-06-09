using Match3Game.MatrixElements.Destroyers;
using Match3Game.Providers;
using Match3Game.Sprites;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements
{
    public class BlackDestroyer : Destroyer
    {
        public BlackDestroyer() : base()
        {
            Load();
        }

        private void Load()
        {
            animationManager = new AnimationManager<AnimationState>();

            Animation createAnim = new Animation(TextureProvider.DarkTile.Textures.Create, AnimationConfig.CreatePeriod);
            Animation idleAnim = new Animation(TextureProvider.DarkTile.Textures.Idle, AnimationConfig.IdlePeriod, true);
            Animation destroyAnim = new Animation(TextureProvider.DarkTile.Textures.Destroy, AnimationConfig.DestroyPeriod);

            animationManager.AddAnimation(AnimationState.Create, createAnim, setCurrentAnimation: true);
            animationManager.AddAnimation(AnimationState.Idle, idleAnim);
            animationManager.AddAnimation(AnimationState.Destroy, destroyAnim);
        }
    }
}
