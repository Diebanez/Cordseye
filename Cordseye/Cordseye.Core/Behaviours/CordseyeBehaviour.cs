using System.Diagnostics;
using Cordseye.Core.Events;
using Cordseye.Core.Math;
using OpenTK;

namespace Cordseye.Core.Behaviours
{
    public abstract class CordseyeBehaviour
    {
        protected Transform m_AttachedTransform;
        public Transform AttachedTransform => m_AttachedTransform;

        public CordseyeBehaviour(ref Transform attachedTransform)
        {
            m_AttachedTransform = attachedTransform;

            EventsManager.Render += OnRender;
            EventsManager.Update += OnUpdate;
            EventsManager.Load += OnLoad;
            EventsManager.Unload += OnUnload;
        }

        ~CordseyeBehaviour()
        {
            EventsManager.Render -= OnRender;
            EventsManager.Update -= OnUpdate;
            EventsManager.Load -= OnLoad;
            EventsManager.Unload -= OnUnload;
        }

        protected virtual void OnRender(Matrix4 projectionMatrix, Matrix4 viewMatrix)
        {

        }

        protected virtual void OnUpdate(double deltaTime)
        {

        }

        protected virtual void OnLoad()
        {
            
        }

        protected virtual void OnUnload()
        {

        }
    }
}
