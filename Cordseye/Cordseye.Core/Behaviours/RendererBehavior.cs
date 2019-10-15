using System.Collections.Generic;
using Cordseye.Core.Events;
using Cordseye.Core.Math;
using Cordseye.Core.Resources;
using OpenTK;

namespace Cordseye.Core.Behaviours
{
    public class RendererBehavior : CordseyeBehaviour
    {
        private static List<RendererBehavior> _Renderers;

        public static List<RendererBehavior> Renderers => _Renderers ?? (_Renderers = new List<RendererBehavior>());

        private List<string> m_Meshes;
        private string m_Shader;
        private List<string> m_Textures;

        public RendererBehavior(ref Transform attachedTransform,  List<string> meshes, string shader, List<string> textures) : base(ref attachedTransform)
        {
            m_Meshes = meshes;
            m_Shader = shader;
            m_Textures = textures;

            var sh = Resource.GetResource<Shader>(m_Shader);
        }

        protected override void OnRender(Matrix4 projectionMatrix, Matrix4 viewMatrix)
        {
            for (int i = 0; i < m_Textures.Count; i++)
            {
                Resource.GetResource<Texture2D>(m_Textures[i]).Bind(i);
            }

            var sh = Resource.GetResource<Shader>(m_Shader);

            sh.Use();

            sh.SetMatrix4("ProjectionMatrix", projectionMatrix);
            sh.SetMatrix4("ViewMatrix", viewMatrix);
            sh.SetMatrix4("ModelMatrix", AttachedTransform.TransformationMatrix);

            for (var i = 0; i < m_Textures.Count; i++)
            {
                sh.SetInt("Texture" + i.ToString(), i);
            }
            
            for (int i = 0; i < m_Meshes.Count; i++)
            {
                Resource.GetResource<Mesh>(m_Meshes[i]).Draw();
            }
        }
    }
}