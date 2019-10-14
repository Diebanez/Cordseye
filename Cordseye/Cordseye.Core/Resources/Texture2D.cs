using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Cordseye.Core.Resources
{   
    public class Texture2D : Resource
    {
        private string m_TexturePath;
        private int m_Handle;

        public Texture2D(string texturePath)
        {
            m_TexturePath = texturePath;

            m_Handle = GL.GenTexture();

            Bind(0);
            var image = Image.Load<Rgba32>(m_TexturePath);
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            Rgba32[] tempPixel = image.GetPixelSpan().ToArray();

            List<byte> pixels = new List<byte>();

            foreach (var pixel in tempPixel)
            {
                pixels.Add(pixel.R);
                pixels.Add(pixel.G);
                pixels.Add(pixel.B);
                pixels.Add(pixel.A);
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            Init(typeof(Texture2D));
        }

        public override string GetKey()
        {
            return m_TexturePath;
        }

        public void Bind(int texUnit)
        {
                GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
                GL.BindTexture(TextureTarget.Texture2D, m_Handle);
        }

        protected override void OnDispose()
        {
            GL.DeleteTextures(1, ref m_Handle);
            base.OnDispose();
        }
    }
}
