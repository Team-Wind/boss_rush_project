shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;
uniform float scanline_count : hint_range(0, 1080) = 300.0;

// Curvature function
vec2 curve(vec2 uv) {
    uv = (uv - 0.5) * 2.0;
    uv *= 1.1;	
    uv.x *= 1.0 + pow((abs(uv.y) / 5.0), 2.0);
    uv.y *= 1.0 + pow((abs(uv.x) / 4.0), 2.0);
    uv  = (uv / 2.0) + 0.5;
    return uv;
}

void fragment() {
    vec2 uv = curve(SCREEN_UV);
    vec4 col = texture(screen_texture, uv);
    
    // Scanlines
    float s = sin(uv.y * scanline_count * PI * 2.0);
    col.rgb = mix(col.rgb, col.rgb * 0.7, s);

    // Vignette
    float vignette = uv.x * uv.y * (1.0 - uv.x) * (1.0 - uv.y);
    col.rgb *= pow(16.0 * vignette, 0.1);

    // Cutoff for curved borders
    if (uv.x < 0.0 || uv.x > 1.0 || uv.y < 0.0 || uv.y > 1.0) {
        col.rgba = vec4(0.0, 0.0, 0.0, 1.0);
    }
    
    COLOR = col;
}