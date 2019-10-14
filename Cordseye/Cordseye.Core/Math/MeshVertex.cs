using System;
using OpenTK;

namespace Cordseye.Core.Math
{
    public class MeshVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexCoords;

        public MeshVertex(Vector3 position, Vector3 normal, Vector2 texCoords)
        {
            Position = position;
            Normal = normal;
            TexCoords = texCoords;
        }

        public float[] GetRawVertex()
        {
            return new float[] { Position.X, Position.Y, Position.Z, Normal.X, Normal.Y, Normal.Z, TexCoords.X, TexCoords.Y };
        }
    }
}
