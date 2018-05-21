using LevelEditor.Engine.Shader;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor.Engine.Renderer
{
    internal sealed class PostProcessRenderer : IRenderer
    {

        private readonly PostProcessShader mShader;

        private readonly GraphicsDevice mDevice;

        public PostProcessRenderer(GraphicsDevice device, ContentManager content, string shaderPath)
        {
            mDevice = device;

            mShader = new PostProcessShader(content, shaderPath);

        }

        public void Render(RenderTarget target, Camera camera, Scene scene)
        {

            mShader.mAlbedoMap = target.mMainRenderTarget;
            mShader.mSaturation = scene.mPostProcessing.mSaturation;

            mShader.Apply();

            mDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 4);

        }
    }
}
