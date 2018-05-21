using Assimp;
using Assimp.Configs;
using LevelEditor.Engine.Mesh;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor.Engine.Loader
{
    internal class MeshLoader
    {
        
        public static Mesh.Mesh LoadMesh(string filename)
        {

            var vertexCount = 0;
            var indexCount = 0;
            var bonesCount = 0;

            var meshData = new MeshData();
            var importer = new AssimpContext();

            var boneConfig = new MaxBoneCountConfig(4);

            importer.SetConfig(boneConfig);

            var scene = importer.ImportFile(filename,
                PostProcessSteps.ImproveCacheLocality | 
                PostProcessSteps.JoinIdenticalVertices |
                PostProcessSteps.LimitBoneWeights | 
                PostProcessSteps.Triangulate | 
                PostProcessSteps.OptimizeMeshes | 
                PostProcessSteps.RemoveRedundantMaterials |
                PostProcessSteps.GenerateUVCoords);

            foreach (var mesh in scene.Meshes)
            {
                vertexCount += mesh.VertexCount;
                indexCount += (mesh.FaceCount * 3);
                bonesCount += mesh.BoneCount;
            }

            meshData.mVertices = new VertexPositionTexture[vertexCount];
            meshData.mIndices = new int[indexCount];

            if (scene.MaterialCount == 1)
            {
                
            }

            return new Mesh.Mesh(null);
        }

        private static bool LoadMaterial()
        {

            return true;

        }
        
    }

}