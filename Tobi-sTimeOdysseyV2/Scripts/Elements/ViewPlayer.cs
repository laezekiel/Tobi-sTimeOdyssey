using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements
{
	public partial class ViewPlayer : Camera2D
	{
		static private ViewPlayer _instance;

		static public ViewPlayer Instance
		{
			get
			{
				if (_instance == null) _instance = new ViewPlayer();
				return _instance;
			}
		}



		private ViewPlayer():base() {}



        [Export]
        private NodePath
            aimCursorPath,
            aimPath,
            dashPath;



        private Sprite2D
            aimCursor;

        private TouchScreenButton
            aim,
            dash;



        public void Init()
        {
            aimCursor = GetNode<Sprite2D>(aimCursorPath);

            aim = GetNode<TouchScreenButton>(aimPath);

            dash = GetNode<TouchScreenButton>(dashPath);
        }



        public override void _Ready()
		{
			Init();



			if (_instance != null)
			{
				QueueFree();
				GD.Print(nameof(ViewPlayer) + " Instance already exist, destroying the last added.");
				return;
			}
			


			base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);



			GlobalPosition = POC.Player.GlobalPosition;



			if (Input.IsActionPressed("Aim"))
			{
				aimCursor.GlobalPosition = new Vector2(Mathf.Lerp(aimCursor.GlobalPosition.X, GetGlobalMousePosition().X, POC.All_Numbers.TenthS),
													   Mathf.Lerp(aimCursor.GlobalPosition.Y, GetGlobalMousePosition().Y, POC.All_Numbers.TenthS));
			}
			else aimCursor.GlobalPosition = new Vector2(Mathf.Lerp(aimCursor.GlobalPosition.X, aim.GlobalPosition.X, POC.All_Numbers.HundredthS),
														Mathf.Lerp(aimCursor.GlobalPosition.Y, aim.GlobalPosition.Y, POC.All_Numbers.HundredthS));



        }



        protected override void Dispose(bool pDisposing)
		{
			if (pDisposing && _instance == this) _instance = null;
			base.Dispose(pDisposing);
		}
	}
}
