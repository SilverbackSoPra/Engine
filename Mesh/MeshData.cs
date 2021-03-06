﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor.Engine.Mesh
{

    /// <summary>
    /// Contains all the data of a mesh like textures or vertices.
    /// </summary>
    internal sealed class MeshData
    {

        public VertexPositionTexture[] mVertices;
        public int[] mIndices;

        public Texture2D mTexture;

        public PrimitiveType mPrimitiveType;
        public int mPrimitiveCount;

        public BoundingSphere mBoundingSphere;

        /// <summary>
        /// Constructs a <see cref="MeshData"/>
        /// </summary>
        public MeshData()
        {

            mVertices = null;
            mIndices = null;
            mTexture = null;

            mPrimitiveType = PrimitiveType.TriangleList;
            mPrimitiveCount = 0;

            mBoundingSphere = new BoundingSphere(new Vector3(0.0f), 0.0f);

        }

    }

}
