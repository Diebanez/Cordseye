#version 330 core
layout (location = 0) in vec3 iPosition;
layout (location = 1) in vec3 iNormal;
layout (location = 2) in vec2 iTexCoords;

out vec2 oTexCoords;

void main()
{
    gl_Position = vec4(iPosition, 1.0);
	oTexCoords = iTexCoords;
}