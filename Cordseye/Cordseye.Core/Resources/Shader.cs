using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Cordseye.Core.Resources
{
    public class Shader : Resource
    {
        private string m_VertexPath;
        private string m_FragmentPath;
        private int m_Handle;

        public Shader(string vertexPath, string fragmentPath) : base()
        {
            string infoLogVert;

            m_VertexPath = vertexPath;
            m_FragmentPath = fragmentPath;

            string vertexSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                vertexSource = reader.ReadToEnd();
            }

            var vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vs, vertexSource);

            GL.CompileShader(vs);

            infoLogVert = GL.GetShaderInfoLog(vs);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            string fragmentSource;

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                fragmentSource = reader.ReadToEnd();
            }

            var fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, fragmentSource);

            GL.CompileShader(fs);

            infoLogVert = GL.GetShaderInfoLog(fs);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            m_Handle = GL.CreateProgram();

            GL.AttachShader(m_Handle, vs);
            GL.AttachShader(m_Handle, fs);

            GL.LinkProgram(m_Handle);

            GL.DetachShader(m_Handle, vs);
            GL.DetachShader(m_Handle, fs);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);

            Init(typeof(Shader));
        }

        public void Use()
        {
            GL.UseProgram(m_Handle);
        }

        public override string GetKey()
        {
            return m_VertexPath + "@" + m_FragmentPath;
        }

        protected override void OnDispose()
        {
            GL.DeleteProgram(m_Handle);
            base.OnDispose();
        }
    }
}
