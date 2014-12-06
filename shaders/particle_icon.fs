#version 140

// particle_icon.fs

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
    vec4 texel = texture(Texture, v_TexCoord).bgra; // webkit texture is bgra
    if (v_ColorPrimary.w == 0.0)
        out_FragColor = texel;
    else
    {
        // horrible, horrible hack to work aground webkit png handling
        texel.rgb = clamp((texel.rgb - 0.27) / 0.7, 0.0, 1.0);

        float gamma = 1.0/2.2;
        vec3 secondary;
        if (v_ColorSecondary == v_ColorPrimary) {
          secondary = vec3(0.0, 0.0, 0.0);
        } else {
          secondary = pow(v_ColorSecondary.rgb, vec3(gamma, gamma, gamma));
        }
        vec3 outline = v_SelectedState > 0.0 ? vec3(1.0, 1.0, 1.0) : secondary;
        vec3 body = pow(v_ColorPrimary.rgb, vec3(gamma, gamma, gamma));
        vec3 color = texel.r * mix(outline, body, pow(texel.g, gamma) / texel.a + 0.00001);

        float alpha = texel.a;

        // check for hover
        if (abs(v_SelectedState) > 1.5)
        {
            float mask = pow(texel.r, gamma);
            alpha = mix(min(1.0, 2.0 * alpha), alpha, mask);
            color = mix(vec3(1.0, 1.0, 1.0), color, mask);
        }

        out_FragColor = vec4(color, alpha * v_ColorPrimary.a);
    }
}
