using Com.IronicEntertainment.TobisTimeOdyssey.Tools.Enums;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools.Dictionarys
{

	static public class PropertyDic
	{
		static private Dictionary<AllProperty, string>
			properties = new Dictionary<AllProperty, string>
			{
				{AllProperty.Scale, "scale"},
				{AllProperty.RectScale, "rect_scale"},
				{AllProperty.GlobalPosition, "global_position"},
				{AllProperty.Modulate, "modulate"},
				{AllProperty.FontColorOverride, "custom_colors/font_color"},
			};

		static public string GetProperty(AllProperty property)
        {
			return properties[property];
        }
	}

}