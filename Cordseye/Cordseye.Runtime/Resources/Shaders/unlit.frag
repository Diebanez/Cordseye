#version 330 core
in vec2 oTexCoords;

out vec4 FragColor;

uniform sampler2D TextureSampler;

void main()
{
    FragColor = texture(TextureSampler, oTexCoords);
}