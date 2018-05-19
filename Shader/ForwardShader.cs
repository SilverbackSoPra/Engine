using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor.Engine.Shader
{

    internal sealed class ForwardShader : Shader
    {
        private readonly EffectParameter mAlbedoMapParameter;
        private readonly EffectParameter mModelMatrixParameter;
        private readonly EffectParameter mViewMatrixParameter;
        private readonly EffectParameter mProjectionMatrixParameter;
        private readonly EffectParameter mGlobalLightLocationParameter;
        private readonly EffectParameter mGlobalLightColorParameter;
        private readonly EffectParameter mGlobalLightAmbientParameter;
        private readonly EffectParameter mLightSpaceMatrixParameter;
        private readonly EffectParameter mShadowMapParameter;
        private readonly EffectParameter mShadowBiasParameter;
        private readonly EffectParameter mShadowNumSamplesParameter;
        private readonly EffectParameter mShadowSampleRangeParameter;
        private readonly EffectParameter mShadowDistanceParameter;

        public Matrix mViewMatrix;
        public Matrix mProjectionMatrix;

        public Vector3 mGlobalLightLocation;
        public Vector3 mGlobalLightColor;
        public float mGlobalLightAmbient;

        public Matrix mLightSpaceMatrix;
        public Texture2D mShadowMap;
        public float mShadowBias;
        public int mShadowNumSamples;
        public float mShadowSampleRange;
        public float mShadowDistance;

        public ForwardShader(ContentManager content, string shaderPath) : base(content, shaderPath)
        { 

            mModelMatrixParameter = mShader.Parameters["modelMatrix"];
            mViewMatrixParameter = mShader.Parameters["viewMatrix"];
            mProjectionMatrixParameter = mShader.Parameters["projectionMatrix"];

            mAlbedoMapParameter = mShader.Parameters["albedoMap"];
            mGlobalLightLocationParameter = mShader.Parameters["lightLocation"];
            mGlobalLightColorParameter = mShader.Parameters["lightColor"];
            mGlobalLightAmbientParameter = mShader.Parameters["lightAmbient"];

            mLightSpaceMatrixParameter = mShader.Parameters["lightSpaceMatrix"];
            mShadowMapParameter = mShader.Parameters["shadowMap"];
            mShadowBiasParameter = mShader.Parameters["shadowBias"];
            mShadowNumSamplesParameter = mShader.Parameters["shadowNumSamples"];
            mShadowSampleRangeParameter = mShader.Parameters["shadowSampleRange"];
            mShadowDistanceParameter = mShader.Parameters["shadowDistance"];

        }

        
        public override void Apply()
        {

            // TODO: We should check whether the matrices are not null
            mViewMatrixParameter.SetValue(mViewMatrix);
            mProjectionMatrixParameter.SetValue(mProjectionMatrix);

            mGlobalLightLocationParameter.SetValue(mGlobalLightLocation);
            mGlobalLightColorParameter.SetValue(mGlobalLightColor);
            mGlobalLightAmbientParameter.SetValue(mGlobalLightAmbient);

            mLightSpaceMatrixParameter.SetValue(mLightSpaceMatrix);
            mShadowMapParameter.SetValue(mShadowMap);
            mShadowBiasParameter.SetValue(mShadowBias);
            mShadowNumSamplesParameter.SetValue(mShadowNumSamples);
            mShadowSampleRangeParameter.SetValue(mShadowSampleRange);
            mShadowDistanceParameter.SetValue(mShadowDistance);

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