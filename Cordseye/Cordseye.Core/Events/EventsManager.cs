using System;
using OpenTK;

namespace Cordseye.Core.Events
{
    public static class EventsManager
    {
        public static event Action Load;
        public static event Action<Matrix4, Matrix4> Render;
        public static event Action<double> Update;
        public static event Action<int, int> Resize;
        public static event Action Unload;

        public static void CallLoad() => Load?.Invoke();
        public static void CallRender(Matrix4 projectionMatrix, Matrix4 viewMatrix) => Render?.Invoke(projectionMatrix, viewMatrix);
        public static void CallUpdate(double deltaTime) => Update?.Invoke(deltaTime);
        public static void CallResize(int width, int height) => Resize?.Invoke(width, height);
        public static void CallUnload() => Unload?.Invoke();
    }
}
