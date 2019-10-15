using System.Collections.Generic;
using System.Net.NetworkInformation;
using OpenTK;

namespace Cordseye.Core.Math
{
    public class Transform
    {
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3 Scale { get; private set; }
        public Transform Parent { get; set; }

        public Matrix4 LocalTransformationMatrix => Matrix4.CreateScale(Scale) * Matrix4.CreateFromQuaternion(Rotation) * Matrix4.CreateTranslation(Position);

        public Matrix4 TransformationMatrix => Parent == null ? LocalTransformationMatrix : Parent.TransformationMatrix * LocalTransformationMatrix;

        public Transform(Transform parent = null)
        {
            Parent = parent;
            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
            Scale = Vector3.One;
        }

        public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public void Translate(Vector3 movement)
        {
            Position += movement;
        }

        public void Rotate(Quaternion rotation)
        {
            Rotation *= rotation;
        }

        public void Resize(Vector3 scale)
        {
            Scale = scale;
        }
    }
}
