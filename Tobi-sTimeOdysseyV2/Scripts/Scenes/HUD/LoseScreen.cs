using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Scenes.HUD
{
	public partial class LoseScreen : Node2D
	{
		[Export]
		private NodePath
			retryPath;

        private PackedScene
            loadingScreenFactory = GD.Load<PackedScene>("res://GameScreensAndScenes/LoadingScreen.tscn");



        private TouchScreenButton
			retry;



		public void Init()
		{
			retry = GetNode<TouchScreenButton>(retryPath);

			Hide();
		}



		public override void _Ready()
		{
			base._Ready();
			Init();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

            switch (State.Current_State)
            {
                case State.GameState.Loading:
                case State.GameState.Player_Aiming:
                case State.GameState.Player_Dashing:
                case State.GameState.Player_Win:
                case State.GameState.Cinematics:
                case State.GameState.Pause:
					Visible = false;
                    break;
                case State.GameState.Player_Caught:
					Visible = true;

                    if (Input.IsActionJustPressed("Retry"))
					{
                        LoadingScreen newLoad = loadingScreenFactory.Instantiate<LoadingScreen>();

                        GetTree().Root.AddChild(newLoad);

                        GetTree().Root.GetChild(0).Free();
						Player.Instance.QueueFree();
						ViewPlayer.Instance.QueueFree();
                    }
                    break;
                default:
                    break;
            }
        }
	}
}
