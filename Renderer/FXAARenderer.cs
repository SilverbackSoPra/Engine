using LevelEditor.Engine.Shader;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LevelEditor.Engine.Renderer
{
    internal sealed class FxaaRenderer : IRenderer
    {

        private readonly FxaaShader mShader;

        private readonly GraphicsDevice mDevice;

        public FxaaRenderer(GraphicsDevice device, ContentManager content, string shaderPath)
        {

            mDevice = device;

            mShader = new FxaaShader(content, shaderPath);

        }

        public void Render(RenderTarget target, Camera camera, Scene scene)
        {

            mShader.mAlbedoMap = target.mPostProcessRenderTarget;
            mShader.mLumaThreshold = scene.mPostProcessing.mFxaa.mLumaThreshold;
            mShader.mLumaThresholdMin = scene.mPostProcessing.mFxaa.mLumaThresholdMin;
            mShader.mDebug = scene.mPostProcessing.mFxaa.mDebugMode;
            mShader.mFramebufferResolution = new Vector2(target.mPostProcessRenderTarget.Width, target.mPostProcessRenderTarget.Height);

            mShader.Apply();

            mDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 4);

        }

    }

}
