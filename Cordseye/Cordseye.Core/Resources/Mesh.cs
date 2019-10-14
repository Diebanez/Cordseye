using System.Collections.Generic;
using Cordseye.Core.Math;
using OpenTK.Graphics.OpenGL;

namespace Cordseye.Core.Resources
{
    public class Mesh : Resource
    {
        private string m_FileName;
        private int m_MeshIndex;

        private float[] m_Vertices;
        private uint[] m_Indices;

        private int m_VBO;
        private int m_VAO;
        private int m_EBO;

        public Mesh(string fileName, int meshIndex, MeshVertex[] vertices, uint[] indices) : base()
        {
            m_FileName = fileName;
            m_MeshIndex = meshIndex;

            var rawVertices = new List<float>();

            foreach (var vertex in vertices)
            {
                foreach (var data in vertex.GetRawVertex())
                {
                    rawVertices.Add(data);
                }
            }

            m_Vertices = rawVertices.ToArray();
            m_Indices = indices;

            m_VAO = GL.GenVertexArray();
            GL.BindVertexArray(m_VAO);

            m_VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, m_Vertices.Length * sizeof(float), m_Vertices,
                BufferUsageHint.StaticDraw);

            m_EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, m_Indices.Length * sizeof(uint), m_Indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            Init(typeof(Mesh));
        }

        public void Draw()
        {
            GL.BindVertexArray(m_VAO);
            GL.DrawElements(PrimitiveType.Triangles, m_Indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }


        public override string GetKey()
        {
            return m_FileName + m_MeshIndex.ToString();
        }

        protected override void OnDispose()
        {
            GL.DeleteVertexArray(m_VAO);
            GL.DeleteBuffer(m_VBO);
            GL.DeleteProgram(m_EBO);

            base.OnDispose();
        }
    }
}