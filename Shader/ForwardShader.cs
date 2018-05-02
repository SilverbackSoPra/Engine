using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Engine.Engine.Shader
{
    internal sealed class ForwardShader : Shader
    {
        private readonly EffectParameter mAlbedoMapParameter;
        private readonly EffectParameter mModelMatrixParameter;
        private readonly EffectParameter mViewMatrixParameter;
        private readonly EffectParameter mProjectionMatrixParameter;

        public Matrix mViewMatrix;
        public Matrix mProjectionMatrix;

        public ForwardShader(ContentManager content, string shaderPath) : base(content, shaderPath)
        { 

            mModelMatrixParameter = mShader.Parameters["modelMatrix"];
            mViewMatrixParameter = mShader.Parameters["viewMatrix"];
            mProjectionMatrixParameter = mShader.Parameters["projectionMatrix"];

            mAlbedoMapParameter = mShader.Parameters["albedoMap"];

        }

        
        public override void Apply()
        {

            // TODO: We should check whether the matrices are not null
            mViewMatrixParameter.SetValue(mViewMatrix);
            mProjectionMatrixParameter.SetValue(mProjectionMatrix);

            base.Apply();
        }
        
        public void ApplyMaterial(Texture2D albedoMap)
        {
            mAlbedoMapParameter.SetValue(albedoMap);
            base.Apply();
        }

        public void ApplyModelMatrix(Matrix modelMatrix)
        {
            mModelMatrixParameter.SetValue(modelMatrix);
            base.Apply();
        }

    }
}