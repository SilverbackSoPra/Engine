using Microsoft.Xna.Framework.Content;

namespace Monogame_Engine.Engine.Shader
{
    internal sealed class PostProcessShader : Shader
    {
        public PostProcessShader(ContentManager content, string shaderPath) : base(content, shaderPath)
        {



        }

        public override void Apply()
        {
            base.Apply();
        }
    }
}
