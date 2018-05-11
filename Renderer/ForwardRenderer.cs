using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Engine.Engine.Shader;

namespace Monogame_Engine.Engine.Renderer
{
    internal sealed class ForwardRenderer : IRenderer
    {

        private readonly ForwardShader mShader;
        private readonly GraphicsDevice mGraphicsDevice;

        public ForwardRenderer(GraphicsDevice device, ContentManager content, string shaderPath)
        {

            mShader = new ForwardShader(content, shaderPath);

            mGraphicsDevice = device;

        }

        public void Render(RenderTarget target, Camera camera, Scene scene)
        {

            Light globalLight = scene.mLights[0];

            mShader.mViewMatrix = camera.mViewMatrix;
            mShader.mProjectionMatrix = camera.mProjectionMatrix;

            mShader.mGlobalLightLocation = globalLight.mLocation;
            mShader.mGlobalLightColor = globalLight.mColor;
            mShader.mGlobalLightAmbient = globalLight.mAmbient;

            mShader.Apply();

            // Now render our actor in batched mode
            foreach (var actorBatch in scene.mActorBatches)
            {
                foreach (var meshPart in actorBatch.mMesh.mMeshParts)
                {

                    // We only want to bind the materials once
                    // https://classes.soe.ucsc.edu/cmps020/Winter10/slides/Feb23.pdf last page
                    mGraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer);
                    mGraphicsDevice.Indices = (meshPart.IndexBuffer);

                    var primitiveCount = meshPart.PrimitiveCount;
                    var vertexOffset = meshPart.VertexOffset;
                    var startIndex = meshPart.StartIndex;

                    /*
                     This might not be the best solution in long term
                     We have to test whether this works on all models or not
                     Also we should change the way we draw models to prevent
                     this kind of programming. One way would be to process the
                     models which are loaded with Monogame into the Mesh Class.
                    */
                    var effect = (BasicEffect)meshPart.Effect;

                    mShader.ApplyMaterial(effect.Texture);

                    foreach (var actor in actorBatch.mActors)
                    {

                        if (actor.mRender)
                        {
                            mShader.ApplyModelMatrix(actor.mModelMatrix);

                            mGraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                                vertexOffset,
                                startIndex,
                                primitiveCount);
                        }

                    }
                }
            }

        }

    }
}
