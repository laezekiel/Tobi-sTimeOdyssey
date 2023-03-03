using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{
	/// <summary>
	/// Propriety On Call
	/// </summary>
	static public class POC
	{
		static private float
			thousandthS = 0.001f,
			hundredthS = 0.01f,
			tenthS = 0.1f,
			halfS = 0.5f,
			oneS = 1f,
			twoS = 2f;

		/// <summary>
		/// get a thousandth of 1
		/// </summary>
		static public float ThousandthS { get { return thousandthS; } }
		/// <summary>
		/// get a hundredth of 1
		/// </summary>
		static public float HundredthS { get { return hundredthS; } }
		/// <summary>
		/// get a tenth of 1
		/// </summary>
		static public float TenthS { get { return tenthS; } }
		/// <summary>
		/// get half of 1
		/// </summary>
		static public float HalfS { get { return halfS; } }
		/// <summary>
		/// get  1
		/// </summary>
		static public float OneS { get { return oneS; } }
		/// <summary>
		/// get 2
		/// </summary>
		static public float TwoS { get { return twoS; } }


		static private Color
			visible = Colors.White,
			invisible = new Color(1, 1, 1, 0);

		/// <summary>
		/// Get a white/visible color
		/// </summary>
		static public Color Visible { get { return visible; } }
		/// <summary>
		/// Get an invisible color
		/// </summary>
		static public Color Invisible { get { return invisible; } }
	}

}