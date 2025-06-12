#version 330

// shader inputs
in vec4 positionWorld;      // fragment position in World Space
in vec4 normalWorld;        // fragment normal in World Space (should probably be vec3)
in vec2 uv;                 // fragment UV texture coordinates

uniform sampler2D diffuseTexture;  // texture sampler
vec4 ambientLightColor = vec4(0.93, 0.88, 0.74, 1.0); //Test ambient light

// shader output
out vec4 outputColor;

void main()
{
    vec3 normal = normalize(normalWorld.xyz);
    vec4 texColor = texture(diffuseTexture, uv);
    outputColor = texColor * ambientLightColor;
}
