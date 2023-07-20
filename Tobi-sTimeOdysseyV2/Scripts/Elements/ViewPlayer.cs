using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
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

        public TouchScreenButton
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
            if (_instance != null)
			{
				QueueFree();
				GD.Print(nameof(ViewPlayer) + " Instance already exist, destroying the last added.");
				return;
            }
            else _instance = new ViewPlayer();
			
			Init();

            base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

            GlobalPosition = new Vector2(Mathf.Lerp(GlobalPosition.X, POC.Player_Position.X, POC.All_Numbers.TenthS),
                                         Mathf.Lerp(GlobalPosition.Y, POC.Player_Position.Y, POC.All_Numbers.TenthS));


            switch (State.Current_State)
			{
				case State.GameState.Loading:
					break;
				case State.GameState.Player_Aiming:
					GameStateIsAiming();
					break;
                case State.GameState.Player_Dashing:
                    break;
				case State.GameState.Player_Caught:
					break;
				case State.GameState.Cinematics:
					break;
				case State.GameState.Pause:
					break;
				default:
					break;
			}


        }

		public void GameStateIsAiming()
		{
			if(!dash.Visible && !aim.Visible) { SwitchHUD(); }
            if (Input.IsActionPressed("Aim"))
            {
                aimCursor.GlobalPosition = new Vector2(Mathf.Lerp(aimCursor.GlobalPosition.X, GetGlobalMousePosition().X, POC.All_Numbers.TenthS),
                                                       Mathf.Lerp(aimCursor.GlobalPosition.Y, GetGlobalMousePosition().Y, POC.All_Numbers.TenthS));

                POC.Player_Rotation = Mathf.RadToDeg(aim.GlobalPosition.AngleToPoint(aimCursor.GlobalPosition));
            }
            else aimCursor.GlobalPosition = new Vector2(Mathf.Lerp(aimCursor.GlobalPosition.X, aim.GlobalPosition.X, POC.All_Numbers.HundredthS),
                                                        Mathf.Lerp(aimCursor.GlobalPosition.Y, aim.GlobalPosition.Y, POC.All_Numbers.HundredthS));

			if (POC.Player_Can_Jump)
            {
                dash.Modulate = POC.AllColor.Visible;
                if (Input.IsActionJustPressed("Dash"))
                {
                    State.SetGameToDash();

                    SwitchHUD();
                }
            }
			else
			{
				dash.Modulate = POC.AllColor.HalfVisible;
			}


            dash.Position = new Vector2(256, 4);
            aim.Position = new Vector2(-352, 100);
        }



		public void SwitchHUD()
		{
            if (dash.Visible) dash.Hide(); else dash.Show();
            if (aim.Visible) aim.Hide(); else aim.Show();
        } 



        protected override void Dispose(bool pDisposing)
		{
			if (pDisposing && _instance == this) _instance = null;
			base.Dispose(pDisposing);
		}
	}
}
