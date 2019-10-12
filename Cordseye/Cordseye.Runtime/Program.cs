using System;
using System.Threading;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Cordseye.Runtime
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            ContextSettings settings = new ContextSettings
            {
                DepthBits = 24,
                StencilBits = 8,
                AntialiasingLevel = 2,
                MajorVersion = 4, 
                MinorVersion = 6,
                AttributeFlags = ContextSettings.Attribute.Core
            };
            RenderWindow window = new RenderWindow(new VideoMode(1280, 720), "SFML Works!",Styles.Default);
            window.Closed += OnWindowClosed;
            window.SetActive(true);

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);

                Thread.Sleep((1/60) * 1000);
                window.Display();
            }
        }

        private static void OnWindowClosed(object source, EventArgs args)
        {
            var window = (RenderWindow) source;
            window.Close();
        }
    }
}