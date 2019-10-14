using System;
using System.Collections.Generic;
using Cordseye.Core.Behaviours;
using Cordseye.Core.Events;
using Cordseye.Core.Globals;
using Cordseye.Core.Math;
using Cordseye.Core.Resources;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Cordseye.Runtime
{
    public class RuntimeWindow : GameWindow
    {
        private static RuntimeWindow mWindow;

        public static RuntimeWindow Window
        {
            get => mWindow;
        }

        public RuntimeWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
           mWindow = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Globals.ClearColor.R, Globals.ClearColor.G, Globals.ClearColor.B, Globals.ClearColor.A);
            EventsManager.CallLoad();

            MeshVertex[] vertices = {
                new MeshVertex(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(1.0f, 1.0f)),
                new MeshVertex(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(1.0f, 0.0f)),
                new MeshVertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(0.0f, 0.0f)),
                new MeshVertex(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(0.0f, 1.0f))
            };

            uint[] indices = {  
                0, 1, 3,   
                1, 2, 3    
            };

            Transform obj = new Transform();

            RendererBehavior renderer = new RendererBehavior(ref obj,
                new List<string>() {new Mesh("Test", 0, vertices, indices).GetKey()},
                new Shader(@"./Resources/Shaders/unlit.vert", @"./Resources/Shaders/unlit.frag").GetKey(),
                new List<string>(){new Texture2D(@"./Resources/Textures/testTexture.jpg").GetKey(), new Texture2D(@"./Resources/Textures/testTexture2.jpg").GetKey()});
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            EventsManager.CallRender();

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            EventsManager.CallUpdate(e.Time);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            EventsManager.CallResize(Width, Height);
        }

        protected override void OnUnload(EventArgs e)
        {
            EventsManager.CallUnload();
            base.OnUnload(e);
        }
    }
}