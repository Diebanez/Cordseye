using System.Collections.Generic;
using Cordseye.Core.Events;
using Cordseye.Core.Math;
using Cordseye.Core.Resources;

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
            

            EventsManager.Render += OnRender;
        }

        ~RendererBehavior()
        {
            EventsManager.Render -= OnRender;
        }

        private void OnRender()
        {
            for (int i = 0; i < m_Textures.Count; i++)
            {
                Resource.GetResource<Texture2D>(m_Textures[i]).Bind(i);
            }

            var sh = Resource.GetResource<Shader>(m_Shader);

            sh.Use();

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