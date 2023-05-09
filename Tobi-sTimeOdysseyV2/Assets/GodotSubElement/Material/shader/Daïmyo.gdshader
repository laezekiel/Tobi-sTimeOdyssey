shader_type canvas_item;

void fragment() 
{
	COLOR = texture(TEXTURE,UV);
	
	float new = (UV.y + UV.x  + (1. - UV.y + 1. - UV.x)) / 4.;
	
	if(COLOR.r < .975 && COLOR.r > .5)
	{
		COLOR.r += new;
		COLOR.bg -= vec2(new);
	}
}
