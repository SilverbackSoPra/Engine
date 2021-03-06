﻿using LevelEditor.Engine.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor.Engine.Renderer
{
    /// <summary>
    /// A new type of Vertex structure which is used to just transfer texture coordinates.
    /// </summary>
    internal struct VertexTexture
    {
        private Vector2 mTextureCoordinate;

        internal static readonly VertexDeclaration sVertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
        );

        internal VertexTexture(float x, float y)
        {
           mTextureCoordinate = new Vector2(x, y);
        }

    }

    /// <summary>
    /// Used to render a full screen texture as a triangle strip.
    /// </summary>
    internal struct Quad
    {
        private static readonly VertexTexture[] sVertices = new VertexTexture[] { new VertexTexture(-1.0f, -1.0f),
            new VertexTexture(-1.0f, 1.0f), new VertexTexture(1.0f, -1.0f), new VertexTexture(1.0f, 1.0f) };

        public readonly VertexBuffer mBuffer;

        public Quad(GraphicsDevice device)
        {
            mBuffer = new VertexBuffer(device, VertexTexture.sVertexDeclaration, 4, BufferUsage.WriteOnly);
            mBuffer.SetData(sVertices);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal sealed class MasterRenderer
    {

        // The path to the resources
        private const string ForwardShaderPath = "Shader/Forward";
        private const string PostProcessShaderPath = "Shader/PostProcess";
        private const string ShadowShaderPath = "Shader/Shadow";
        private const string FxaaShaderPath = "Shader/FXAA";

        private readonly ForwardRenderer mForwardRenderer;
        private readonly PostProcessRenderer mPostProcessRenderer;
        private readonly ShadowRenderer mShadowRenderer;
        private readonly FxaaRenderer mFxaaRenderer;

        private readonly GraphicsDevice mGraphicsDevice;

        private readonly Quad mQuad;

        /// <summary>
        /// Constructs a <see cref="MasterRenderer"/>.
        /// </summary>
        /// <param name="device">The graphics device which should already be initialized.</param>
        /// <param name="content">The content manager which should already be initialized.</param>
        public MasterRenderer(GraphicsDevice device, ContentManager content)
        {

            mGraphicsDevice = device;

            var depthStencilState = new DepthStencilState();
            depthStencilState.StencilEnable = false;

            mGraphicsDevice.DepthStencilState = depthStencilState;

            mQuad = new Quad(device);

            mForwardRenderer = new ForwardRenderer(device, content, ForwardShaderPath);
            mPostProcessRenderer = new PostProcessRenderer(device, content, PostProcessShaderPath);
            mShadowRenderer = new ShadowRenderer(device, content, ShadowShaderPath);
            mFxaaRenderer = new FxaaRenderer(device, content, FxaaShaderPath);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewport"></param>
        /// <param name="target"></param>
        /// <param name="camera"></param>
        /// <param name="scene"></param>
        public void Render(Viewport viewport, RenderTarget target, Camera camera, Scene scene)
        {
            // Flat rendering: http://community.monogame.net/t/flat-shading-low-poly/8668
            if (scene.mLights.Count <= 0)
            {
                throw new EngineInvalidParameterException("Scene doesn't contain any light sources");
            }
            else
            {
                mGraphicsDevice.BlendState = BlendState.Opaque;
                mGraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

                var globaLight = scene.mLights[0];

                if (globaLight.mShadow.mActivated)
                {
                    mGraphicsDevice.SetRenderTarget(target.mShadowRenderTarget);

                    mGraphicsDevice.Clear(Color.White);

                    mShadowRenderer.Render(target, camera, scene);
                }

                mGraphicsDevice.SetRenderTarget(target.mMainRenderTarget);

                mGraphicsDevice.Clear(new Color(41, 122, 255));

                mForwardRenderer.Render(target, camera, scene);

                // Check which framebuffer we need to use.
                // If we want to have Anti-Aliasing we need to bind the post-process render target
                if (scene.mPostProcessing.mFxaa.mActivated)
                {
                    mGraphicsDevice.SetRenderTarget(target.mPostProcessRenderTarget);
                }
                else
                {
                    mGraphicsDevice.SetRenderTarget(null);
                }

                mGraphicsDevice.SetVertexBuffer(mQuad.mBuffer);

                var defaultViewport = mGraphicsDevice.Viewport;
                mGraphicsDevice.Viewport = viewport;

                mPostProcessRenderer.Render(target, camera, scene);

                if (scene.mPostProcessing.mFxaa.mActivated)
                {
                    mGraphicsDevice.SetRenderTarget(null);

                    mFxaaRenderer.Render(target, camera, scene);
                }

                mGraphicsDevice.Viewport = defaultViewport;

            }

        }

    }

}