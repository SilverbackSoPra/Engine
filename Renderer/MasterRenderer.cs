using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Engine.Engine;
using Monogame_Engine.Engine.Renderer;

namespace Monogame_Engine.Engine.Renderer
{
    /// <summary>
    /// A new type of Vertex structure which is used to just transfer texture coordinates.
    /// </summary>
    public struct VertexTexture
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

    public struct Quad
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

    internal sealed class MasterRenderer : IRenderer
    {

        // The path to the resources
        private const string ForwardShaderPath = "Shader/Forward";
        private const string PostProcessShaderPath = "Shader/PostProcess";

        private readonly ForwardRenderer mForwardRenderer;
        private readonly PostProcessRenderer mPostProcessRenderer;

        private readonly GraphicsDevice mGraphicsDevice;

        private Quad mQuad;

        public MasterRenderer(GraphicsDevice device, ContentManager content)
        {

            mGraphicsDevice = device;

            mQuad = new Quad(device);

            mForwardRenderer = new ForwardRenderer(device, content, ForwardShaderPath);
            mPostProcessRenderer = new PostProcessRenderer(device, content, PostProcessShaderPath);

        }


        public void Render(RenderTarget target, Camera camera, Scene scene)
        {
            // Flat rendering: http://community.monogame.net/t/flat-shading-low-poly/8668

            mGraphicsDevice.BlendState = BlendState.Opaque;
            mGraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            mGraphicsDevice.SetRenderTarget(target.mMainRenderTarget);

            mGraphicsDevice.Clear(Color.White);

            mForwardRenderer.Render(target, camera, scene);

            mGraphicsDevice.SetRenderTarget(null);

            mGraphicsDevice.SetVertexBuffer(mQuad.mBuffer);

            mPostProcessRenderer.Render(target, camera, scene);

        }

    }

}