using System.Collections.Generic;
using Assimp;
using Cordseye.Core.Math;
using OpenTK;

namespace Cordseye.Core.Resources
{
    public class Model : Resource
    {
        private string m_ModelPath;
        private int m_SpawnedMeshes = 0;

        public Model(string modelPath) : base()
        {
            m_ModelPath = modelPath;

            AssimpContext importer = new AssimpContext();
            var scene = importer.ImportFile(modelPath);

            ProcessNode(scene.RootNode, scene);

            Init(typeof(Model));
        }

        private void ProcessNode(Node node, Scene scene)
        {
            for (int i = 0; i < node.MeshCount; i++)
            {
                var mesh = scene.Meshes[node.MeshIndices[i]];
                ProcessMesh(mesh, scene);
            }

            for (int i = 0; i < node.ChildCount; i++)
            {
                ProcessNode(node.Children[i], scene);
            }
        }

        private void ProcessMesh(Assimp.Mesh mesh, Scene scene)
        {
            List<MeshVertex> vertices = new List<MeshVertex>();
            List<uint> indices = new List<uint>();

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vector3 position = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z);
                Vector3 normal = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z);

                Vector2 texCoords = Vector2.Zero;

                if (mesh.TextureCoordinateChannelCount > 0)
                {
                    texCoords = new Vector2(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y);
                }

                vertices.Add(new MeshVertex(position, normal, texCoords));
            }

            for (int i = 0; i < mesh.FaceCount; i++)
            {
                var face = mesh.Faces[i];
                for (int j = 0; j < face.IndexCount; j++)
                {
                    indices.Add((uint)face.Indices[j]);
                }
            }

            new Mesh(m_ModelPath, m_SpawnedMeshes, vertices.ToArray(), indices.ToArray());
            m_SpawnedMeshes++;
        }

        public override string GetKey()
        {
            return m_ModelPath;
        }

    }
}
