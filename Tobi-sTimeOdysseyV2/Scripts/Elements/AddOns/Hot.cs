using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.AddOns
{
	public partial class Hot : AddOn
	{
		[Export]
		private NodePath
			checkPath,
			lightPath,
			timePath;



		private Area2D
			check;

		private Sprite2D
			light;

		private Timer
			time;



		private bool
			isOn = true;

		private float
			newWaitTime = 0;



        public override void Init()
        {
            base.Init();

			check = GetNode<Area2D>(checkPath);

			light = GetNode<Sprite2D>(lightPath);

			time = GetNode<Timer>(timePath);
			time.Timeout += Update;

			Update();
        }

        public override void _Ready()
		{
			base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

            switch (State.Current_State)
            {
                case State.GameState.Player_Aiming:
                case State.GameState.Player_Dashing:
					if (time.Paused) time.Paused = false;
					if (isOn)
					{
						foreach (Node2D item in check.GetOverlappingBodies())
						{
							if (item is Player)
							{
								State.SetLoseTypeToBurnt();
								State.SetGameToCaught();
							}
						}
					}
                    break;
                case State.GameState.Player_Caught:
                case State.GameState.Player_Win:
                case State.GameState.Cinematics:
                case State.GameState.Loading:
                case State.GameState.Pause:
                    if (!time.Paused) time.Paused = true;
                    break;
                default:
                    break;
            }
        }



		private void Update()
		{
			if (isOn) { isOn = false; light.Modulate = Colors.Green; }
			else { isOn = true; light.Modulate = Colors.Red; }
			
            newWaitTime = rand.RandfRange(2f, 4);
            time.Start(newWaitTime);
        }
	}
}
