using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Engine.Engine.Mesh
{

    /// <summary>
    /// 
    /// </summary>
    internal sealed class Mesh
    {

        public readonly List<ModelMeshPart> mMeshParts;

        public readonly float mRadius;

        /// <summary>
        /// Constructs a <see cref="Mesh"/>.
        /// </summary>
        /// <param name="model">A textured model which was loaded with the content pipeline of MonoGame.</param>
        public Mesh(Model model)
        {

            // How to use animations: 

            mRadius = model.Meshes[0].BoundingSphere.Radius;

            mMeshParts = new List<ModelMeshPart>();

            for (ushort i = 0; i < model.Meshes.Count; i++)
            {
                var modelMesh = model.Meshes[i];

                for (ushort j = 0; j < modelMesh.MeshParts.Count; j++)
                {
                    mMeshParts.Add(modelMesh.MeshParts[j]);
                }
            }

        }

    }
}
