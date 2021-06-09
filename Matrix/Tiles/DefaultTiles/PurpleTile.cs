using Match3Game.Providers;
using Match3Game.Sprites;
using Microsoft.Xna.Framework;

namespace Match3Game.MatrixElements.DefaultTiles
{
    public class PurpleTile : Tile
    {
        public PurpleTile(TileType type) : base()
        {
            Type = type;
            LoadTextures(type);
        }

        private void LoadTextures(TileType type)
        {
            animationManager = new AnimationManager<AnimationState>();

            Animation idleAnim = new Animation(TextureProvider.PurpleTile.Textures.Idle, AnimationConfig.IdlePeriod, true);
            Animation destroyAnim = new Animation(TextureProvider.PurpleTile.Textures.Destroy, AnimationConfig.DestroyPeriod);
            Animation selectAnim = new Animation(TextureProvider.PurpleTile.Textures.Destroy, AnimationConfig.SelectPeriod, true);
            Animation waitDestroy = new Animation(TextureProvider.PurpleTile.Textures.Idle, AnimationConfig.WhaitDestroy);

            switch (type)
            {
                case TileType.Bomb:
                    {
                        idleAnim.SetDetailTexture(TextureProvider.BombTile.Textures.Idle);
                        destroyAnim.SetDetailTexture(TextureProvider.BombTile.Textures.Destroy);
                        selectAnim.SetDetailTexture(TextureProvider.BombTile.Textures.Destroy);
                        waitDestroy.SetDetailTexture(TextureProvider.BombTile.Textures.Idle);
                        break;
                    }
                case TileType.HorizontalLine:
                    {
                        idleAnim.SetDetailTexture(TextureProvider.HorizontalLineTile.Textures.Idle);
                        destroyAnim.SetDetailTexture(TextureProvider.HorizontalLineTile.Textures.Destroy);
                        selectAnim.SetDetailTexture(TextureProvider.HorizontalLineTile.Textures.Destroy);
                        waitDestroy.SetDetailTexture(TextureProvider.HorizontalLineTile.Textures.Idle);
                        break;
                    }
                case TileType.VerticalLine:
                    {
                        idleAnim.SetDetailTexture(TextureProvider.VerticalLineTile.Textures.Idle);
                        destroyAnim.SetDetailTexture(TextureProvider.VerticalLineTile.Textures.Destroy);
                        selectAnim.SetDetailTexture(TextureProvider.VerticalLineTile.Textures.Destroy);
                        waitDestroy.SetDetailTexture(TextureProvider.VerticalLineTile.Textures.Idle);
                        break;
                    }
                default:
                    break;
            }

            animationManager.AddAnimation(AnimationState.Idle, idleAnim, setCurrentAnimation: true);
            animationManager.AddAnimation(AnimationState.Destroy, destroyAnim);
            animationManager.AddAnimation(AnimationState.Select, selectAnim);
            animationManager.AddAnimation(AnimationState.WaitDestroy, waitDestroy);
        }
    }
}
