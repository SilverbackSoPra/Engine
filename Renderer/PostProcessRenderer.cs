using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Engine.Engine.Shader;

namespace Monogame_Engine.Engine.Renderer
{
    internal sealed class PostProcessRenderer : IRenderer
    {

        private readonly PostProcessShader mShader;

        private readonly SpriteBatch mSpriteBatch;

        public PostProcessRenderer(ContentManager content, string shaderPath, SpriteBatch spriteBatch)
        {
            mSpriteBatch = spriteBatch;

            // mShader = new PostProcessShader(content, shaderPath);

        }

        public void Render(RenderTarget target, Camera camera, Scene scene)
        {

            /*
            We don't want use the SpriteBatch class and should change to our own vertex structure
            To improve this: http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series1/Terrain_lighting.php
            Some further resources: https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb199731%28v%3dxnagamestudio.35%29
            */

            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
                SamplerState.LinearClamp, DepthStencilState.Default,
                RasterizerState.CullNone);

            mSpriteBatch.Draw(target.mMainRenderTarget, new Rectangle(0, 0, 1280, 720), Color.White);

            mSpriteBatch.End();

        }
    }
}
