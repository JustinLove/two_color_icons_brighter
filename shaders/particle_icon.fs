#version 150

// textured_unlit_bgra_colorized.fs

#ifdef GL_ES
precision mediump float;
#endif

uniform sampler2D Texture;

in vec2 v_TexCoord;
in vec4 v_ColorPrimary;
in vec4 v_ColorSecondary;
in float v_SelectedState;

out vec4 out_FragColor;

void main() 
{
    vec4 texel = texture(Texture, v_TexCoord).bgra;
    if (v_ColorPrimary.w == 0.0)
        out_FragColor = texel;
    else
    {
        // horrible, horrible hack to work aground webkit png handling
        texel.rgb = clamp((texel.rgb - 0.27) / 0.7, 0.0, 1.0);

        vec3 brighterSec = mix(vec3(1.0, 1.0, 1.0), v_ColorSecondary.rgb, 0.8);
        vec3 outline = v_SelectedState > 0.0 ? vec3(1.0, 1.0, 1.0) : brighterSec;
        vec3 body = v_ColorPrimary.rgb;
        vec3 color = texel.r * mix(outline, body, pow(texel.g, 1.0 / 2.2));

        float alpha = texel.a;
        out_FragColor = vec4(color, alpha * v_ColorPrimary.a);
    }
}
