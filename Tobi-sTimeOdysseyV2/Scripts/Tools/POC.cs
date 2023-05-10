using System;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Godot;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Tools
{
	static public class POC
	{
		public struct Numbers
		{
			public Numbers()
			{
				_thousandthS = 0.001f;
				_hundredthS = 0.01f;
				_tenthS = 0.1f;
				_halfS = 0.5f;
				_oneS = 1f;
				_twoS = 2f;
			}



			private float
				_thousandthS,
				_hundredthS,
				_tenthS,
				_halfS,
				_oneS,
				_twoS;



			/// <summary>
			/// get a thousandth of 1
			/// </summary>
			public float ThousandthS { get { return _thousandthS; } }
			/// <summary>
			/// get a hundredth of 1
			/// </summary>
			public float HundredthS { get { return _hundredthS; } }
			/// <summary>
			/// get a tenth of 1
			/// </summary>
			public float TenthS { get { return _tenthS; } }
			/// <summary>
			/// get half of 1
			/// </summary>
			public float HalfS { get { return _halfS; } }
			/// <summary>
			/// get  1
			/// </summary>
			public float OneS { get { return _oneS; } }
			/// <summary>
			/// get 2
			/// </summary>
			public float TwoS { get { return _twoS; } }
		}



		private static Numbers _allNumbers = new Numbers();

		public static Numbers All_Numbers { get { return _allNumbers; } }



		public struct ColorState
		{
			public ColorState()
			{
				_visible = Colors.White;
				_invisible = new Color(1, 1, 1, 0);
			}



			private Color
				_visible,
				_invisible;



			/// <summary>
			/// Get a white/visible color
			/// </summary>
			public Color Visible { get { return _visible; } }
			/// <summary>
			/// Get an invisible color
			/// </summary>
			public Color Invisible { get { return _invisible; } }
		}



		private static ColorState _allColor = new ColorState();

		public static ColorState AllColor { get { return _allColor; } }



		public static Player Player { get { return Player.Instance; } }
	}
}

