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
				_threeS = 3f;
				_fours = 4f;
			}



			private float
				_thousandthS,
				_hundredthS,
				_tenthS,
				_halfS,
				_oneS,
				_twoS,
				_threeS,
				_fours;



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
			/// <summary>
			/// get 3
			/// </summary>
			public float ThreeS { get { return _threeS; } }
			/// <summary>
			/// get 
			/// </summary>
			public float Fours { get { return _fours; } }
		}



		private static Numbers _allNumbers = new Numbers();

		public static Numbers All_Numbers { get { return _allNumbers; } }



		public struct ColorState
		{
			public ColorState()
			{
				_visible = Colors.White;
				_red = Colors.Red;
                _halfVisible = new Color(0.5f, 0.5f, 0.5f, 0.5f);
				_invisible = new Color(1, 1, 1, 0);
			}



			private Color
				_visible,
				_red,
				_halfVisible,
				_invisible;



			/// <summary>
			/// Get a white/visible color
			/// </summary>
			public Color Visible { get { return _visible; } }
			public Color Red { get { return _red; } }
			/// <summary>
			/// Get an half Visible color
			/// </summary>
			public Color HalfVisible { get { return _halfVisible; } }
			/// <summary>
			/// Get an invisible color
			/// </summary>
			public Color Invisible { get { return _invisible; } }
		}



		private static ColorState _allColor = new ColorState();

		public static ColorState AllColor { get { return _allColor; } }



		public static Vector2 Player_Position { get; set; }
		public static float Player_Rotation { get; set; }
		public static bool Player_Can_Jump { get; set; }
	}
}

