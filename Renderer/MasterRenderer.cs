using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Engine.Engine.Renderer
{
    internal sealed class MasterRenderer : IRenderer
    {

        // The path to the resources
        private const string ForwardShaderPath = "Shader/Forward";
        private const string PostProcessShaderPath = "Shader/PostProcess";

        private readonly ForwardRenderer mForwardRenderer;
        private readonly PostProcessRenderer mPostProcessRenderer;

        private readonly GraphicsDevice mGraphicsDevice;


        public MasterRenderer(GraphicsDevice device, ContentManager content)
        {

            mGraphicsDevice = device;

            var spriteBatch = new SpriteBatch(device);

            mForwardRenderer = new ForwardRenderer(device, content, ForwardShaderPath);
            mPostProcessRenderer = new PostProcessRenderer(content, PostProcessShaderPath, spriteBatch);

        }


        public void Render(RenderTarget target, Camera camera, Scene scene)
        {

            mGraphicsDevice.BlendState = BlendState.Opaque;
            mGraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            mGraphicsDevice.SetRenderTarget(target.mMainRenderTarget);

            mGraphicsDevice.Clear(Color.White);

            mForwardRenderer.Render(target, camera, scene);

            mGraphicsDevice.SetRenderTarget(null);

            mPostProcessRenderer.Render(target, camera, scene);

        }

    }

}