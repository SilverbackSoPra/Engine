﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Engine.Engine.Renderer
{
    internal sealed class Renderer : IRenderer
    {

        // The path to the resources
        private const string ForwardShaderPath = "Shader/Forward";

        private readonly ForwardRenderer mForwardRenderer;

        public Renderer(GraphicsDevice device, ContentManager content)
        {

            mForwardRenderer = new ForwardRenderer(device, content, ForwardShaderPath);

        }

        public void Render(RenderTarget target, Camera camera, Scene scene)
        {

            mForwardRenderer.Render(target, camera, scene);

        }

    }

}