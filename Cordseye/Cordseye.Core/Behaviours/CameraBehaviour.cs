using Cordseye.Core.Math;
using OpenTK;

namespace Cordseye.Core.Behaviours
{
    public class CameraBehaviour : CordseyeBehaviour
    {
        private static CameraBehaviour m_MainCamera;
        public static CameraBehaviour MainCamera => m_MainCamera;

        private float m_FovY;
        private float m_Aspect;
        private float m_NearClip;
        private float m_FarClip;

        public CameraBehaviour(ref Transform attachedTransform, float fovY = 60.0f, float aspect = 1.0f, float nearClip = .01f, float farClip = 200f) : base(ref attachedTransform)
        {
            if (m_MainCamera == null)
                m_MainCamera = this;

            m_FovY = fovY;
            m_Aspect = aspect;
            m_NearClip = nearClip;
            m_FarClip = farClip;
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView( MathHelper.DegreesToRadians(60.0f), 1280.0f / 720.0f, 0.01f, 200f);
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(AttachedTransform.Position ,AttachedTransform.Position + AttachedTransform.Rotation * Vector3.UnitZ, AttachedTransform.Rotation * Vector3.UnitY);
        }
    }
}
