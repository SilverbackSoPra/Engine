using Microsoft.Xna.Framework;

namespace Monogame_Engine.Engine.Mesh
{
    /// <summary>
    /// An actor represents an instance of a mesh in a scene.
    /// </summary>
    internal sealed class Actor
    {

        public readonly Mesh mMesh;
        public Matrix mModelMatrix;

        public bool mRender;

        /// <summary>
        /// Constructs an <see cref="Actor"/>.
        /// </summary>
        /// <param name="mesh"></param>
        public Actor(Mesh mesh)
        {

            mMesh = mesh;
            mModelMatrix = new Matrix();

            // Actor should be rendered
            mRender = true;

        }
    }
}
