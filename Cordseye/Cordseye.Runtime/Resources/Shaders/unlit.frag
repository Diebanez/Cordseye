#version 330 core
in vec2 oTexCoords;

out vec4 FragColor;

uniform sampler2D Texture0;
uniform sampler2D Texture1;

void main()
{
    FragColor = mix(texture(Texture0, oTexCoords), texture(Texture1, oTexCoords), oTexCoords.x);
}