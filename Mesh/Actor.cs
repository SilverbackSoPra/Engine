using Microsoft.Xna.Framework;

namespace LevelEditor.Engine.Mesh
{
    /// <summary>
    /// An actor represents an instance of a mesh in a scene.
    /// </summary>
    internal sealed class Actor
    {

        public readonly Mesh mMesh;
        public Matrix mModelMatrix;

        public bool mRender;
        public bool mCastShadow;

        /// <summary>
        /// Constructs an <see cref="Actor"/>.
        /// </summary>
        /// <param name="mesh">The <see cref="Mesh"/> which the actor represents in the scene</param>
        public Actor(Mesh mesh)
        {

            mMesh = mesh;
            mModelMatrix = Matrix.Identity;

            // Actor should be rendered
            mRender = true;
            mCastShadow = true;

        }
    }
}
