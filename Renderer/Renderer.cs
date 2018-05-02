using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Engine.Engine.Renderer
{
    internal sealed class Renderer : IRenderer
    {

        // The path to the resources
        private const string ForwardShaderPath = "Shader/Forward";

        private readonly ForwardRenderer mForwardRenderer;
        private readonly GraphicsDevice mGraphicsDevice;

        public Renderer(GraphicsDevice device, ContentManager content)
        {

            mGraphicsDevice = device;
            mForwardRenderer = new ForwardRenderer(device, content, ForwardShaderPath);

        }

        public void Render(RenderTarget target, Camera camera, Scene scene)
        {

            mGraphicsDevice.SetRenderTarget(target.mMainRenderTarget);

            mForwardRenderer.Render(target, camera, scene);

            mGraphicsDevice.SetRenderTarget(null);

        }

    }

}