#version 330

// shader inputs
in vec4 positionWorld;      // fragment position in World Space
in vec4 normalWorld;        // fragment normal in World Space (should probably be vec3)
in vec2 uv;                 // fragment UV texture coordinates

uniform sampler2D diffuseTexture;  // texture sampler
uniform vec4 ambientLightColor;

// shader output
out vec4 outputColor;

void main()
{
    vec4 texColor = texture(diffuseTexture, uv);
    outputColor = texColor * ambientLightColor;
}
