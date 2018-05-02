using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Engine.Engine
{
    class RenderTarget
    {

        public RenderTarget2D mMainRenderTarget;
        private Texture2D mShadowMap;

        public RenderTarget(GraphicsDevice device, int width, int height, int shadowMapResolution)
        {
            mMainRenderTarget = new RenderTarget2D(device, width, height, true, SurfaceFormat.HalfVector4, DepthFormat.Depth24);
        }

    }
}
