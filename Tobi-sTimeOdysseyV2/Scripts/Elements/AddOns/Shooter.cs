using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Projectiles;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.AddOns
{
	public partial class Shooter : AddOn
	{
		private enum Pattern
		{
			c,
			l,
			r,
			cl,
			cr,
			lr,
			clr,
			ctl,
			ctr,
			ltc,
			ltr,
			rtl,
			rtc,
			ctrl,
			cltr,
			crtl,
			ltrc,
			lrtc,
			rtcl,
			ctltr,
			ctrtl,
			ltctr,
			ltrtc,
			rtctl,
			rtltc
		}



		[Export]
		private PackedScene
			projectile;

        [Export]
		private NodePath
			cPath,
			lPath,
			rPath,
			tPath;



		private Marker2D
			c,
			l,
			r;

		private Timer
			t;



        private Pattern 
			pattern;

		private float
			newWaitTime = 0;



        public override void Init()
		{
			base.Init();
			c = GetNode<Marker2D>(cPath);
			l = GetNode<Marker2D>(lPath);
			r = GetNode<Marker2D>(rPath);

			t = GetNode<Timer>(tPath);
			t.Timeout += StartShooting;

			pattern = (Pattern)rand.RandiRange(0, (Enum.GetValues(typeof(Pattern)).Length) - 1);

            StartShooting();
		}



		public override void _Ready()
		{
			base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta); switch (State.Current_State)
            {
                case State.GameState.Player_Aiming:
                case State.GameState.Player_Dashing:
                    if (t.Paused) t.Paused = false;
                    break;
                case State.GameState.Player_Caught:
                case State.GameState.Cinematics:
                case State.GameState.Loading:
                case State.GameState.Pause:
                    if (!t.Paused) t.Paused = true;
                    break;
                default:
                    break;
            }
        }
		


		public void StartShooting()
		{
			char[] sequenceArray = pattern.ToString().ToCharArray();
			List<char> sequence = new List<char>();

			foreach (char String in sequenceArray)
            {
				sequence.Add(String);
			}

            newWaitTime = 0;

			if (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing) Shoot(sequence);
        }

		public async void Shoot(List<char> pSequence)
		{
			Projectile shoot = projectile.Instantiate<Projectile>();
			AddChild(shoot);

			switch (pSequence[0])
			{
				case 'c':
					shoot.GlobalPosition = c.GlobalPosition; 
					break;
				case 'l':
					shoot.GlobalPosition = l.GlobalPosition; 
					break;
				case 'r':
					shoot.GlobalPosition = r.GlobalPosition; 
					break;
				default:
					break;
			}

			newWaitTime++;

			pSequence.RemoveAt(0);

            if (pSequence.Count == 0)
            {
                pattern = (Pattern)rand.RandiRange(0, (Enum.GetValues(typeof(Pattern)).Length) - 1);

                if (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing) t.Start(newWaitTime);
            }
			else if (pSequence[0] != 't')
            {
                if (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing) Shoot(pSequence);
            }
            else if(pSequence[0] == 't')
            {
				await Task.Delay((int)(0.25f * 1000));

				newWaitTime -= 0.5f;

				pSequence.RemoveAt(0);

                if (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing) Shoot(pSequence);
            }
        }
	}
}
