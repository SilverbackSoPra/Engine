using Microsoft.Xna.Framework.Graphics;


namespace LevelEditor.Engine
{
    /// <summary>
    /// A render target contains render buffers for every render pass of the engine.
    /// </summary>
    class RenderTarget
    {

        private readonly GraphicsDevice mGraphicsDevice;

        public RenderTarget2D mMainRenderTarget;
        public RenderTarget2D mPostProcessRenderTarget;
        public RenderTarget2D mShadowRenderTarget;

        /// <summary>
        /// Constructs a <see cref="RenderTarget"/>.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="shadowMapResolution"></param>
        public RenderTarget(GraphicsDevice device, int width, int height, int shadowMapResolution)
        {

            mGraphicsDevice = device;

            mMainRenderTarget = new RenderTarget2D(mGraphicsDevice, width, height, false, SurfaceFormat.HalfVector4, DepthFormat.Depth16);
            mPostProcessRenderTarget = new RenderTarget2D(mGraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);
            mShadowRenderTarget = new RenderTarget2D(mGraphicsDevice, shadowMapResolution, shadowMapResolution, false, SurfaceFormat.Single, DepthFormat.Depth16);

        }

        /// <summary>
        /// Resizes the normal render buffers for scene and postprocessing
        /// </summary>
        /// <param name="width">The width of the resolution</param>
        /// <param name="height">The height of the resolution</param>
        public void Resize(int width, int height)
        {
            mMainRenderTarget.Dispose();
            mPostProcessRenderTarget.Dispose();

            mMainRenderTarget = new RenderTarget2D(mGraphicsDevice, width, height, false, SurfaceFormat.HalfVector4, DepthFormat.Depth16);
            mPostProcessRenderTarget = new RenderTarget2D(mGraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);
        }

        /// <summary>
        /// Resizes the shadow map render buffer.
        /// </summary>
        /// <param name="shadowMapResolution">The resolution of the shadow map</param>
        public void Resize(int shadowMapResolution)
        {
            mShadowRenderTarget.Dispose();

            mShadowRenderTarget = new RenderTarget2D(mGraphicsDevice, shadowMapResolution, shadowMapResolution, false, SurfaceFormat.Single, DepthFormat.Depth16);
        }

    }
}
