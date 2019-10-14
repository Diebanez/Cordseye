using Cordseye.Core.Math;

namespace Cordseye.Core.Behaviours
{
    public abstract class CordseyeBehaviour
    {
        public Transform AttachedTransform { get; }

        public CordseyeBehaviour(ref Transform attachedTransform)
        {
            AttachedTransform = attachedTransform;
        }
    }
}
