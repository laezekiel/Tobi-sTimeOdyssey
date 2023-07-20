using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Threading.Tasks;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers.Enemies
{
	public partial class Enemy : Villager
	{
		[Export]
		protected int
			_maxLife = 1;



		protected int
			_currentLife;



		public int Current_Life { get { return _currentLife; } }



        public override void Init()
        {
            base.Init();

			_currentLife = _maxLife;
        }



        public override void _Ready()
		{
			base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);
		}



        /// <summary>
		/// Create and play a tween rotating the NPC towards an empty Direction then backward and finaly back 
        /// to the empty position according to the pattern for { time } in miliseconds and set doAction to Turn
        /// </summary>
        protected override async void SetModeTurn()
        {
            doAction = Turn;


            float lRotOrigin = GlobalRotationDegrees + 180;
            float lRotTODO = GlobalRotationDegrees;


            switch (pattern)
            {
                case Pattern.Static:
                    break;
                case Pattern.Patrolling:
                    lRotTODO = Patrolling_Rotattion(lRotTODO);
                    break;
                case Pattern.Surveilling:
                    lRotTODO = Surveilling_Rotattion(lRotTODO);
                    break;
                default:
                    break;
            }


            animation = CreateTween();


            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotTODO), time / 2);
            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotOrigin), time / 2);
            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotTODO), time / 2);


            animation.Play();


            await Task.Delay((int)(1.5f * time * 1000));


            if (doAction == Turn && (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing))
            {
                animation.Kill();

                if (pattern == Pattern.Patrolling) SetModeForward();
                else SetModeSurveilling();
            }
        }

        protected virtual async void SetModeKnockOut(float pRot)
        {
            doAction = KnockOut;
            if (animation != null) animation.Kill();

            SwitchState("dead");
            check.Monitoring = sight.Enabled = false;

            await Task.Delay((int)(2.5f * time * 1000));

            SwitchState("idle");
            check.Monitoring = sight.Enabled = true;

            SetModeControl(pRot);
        }

        protected virtual async void SetModeControl(float pRot)
        {
            doAction = Control;
            animation = CreateTween();

            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, pRot, time / 2);
            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, pRot - 0.25f * pRot, time / 2);
            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, pRot + 0.25f * pRot, time / 2);
            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, 0, time / 2);

            await Task.Delay((int)(2 * time * 1000));

            if (doAction == Control && (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing))
            {
                animation.Kill();

                if (pattern == Pattern.Patrolling) SetModeForward();
                else SetModeSurveilling();
            }
        }



        public virtual void KnockOut()
        {

        }

        protected virtual void Control()
        {

        }



        public void LoseLife()
        {
            _currentLife--;
            if(_currentLife == 0)
            {
                if (animation != null) animation.Kill();

                QueueFree();
            }
            else
            {
                float rotation = GlobalPosition.AngleToPoint(POC.Player_Position);
                SetModeKnockOut(rotation);
            }
        }

        protected virtual void SwitchState(string pState)
        {
            switch (pState)
            {
                case "dead":
                    body.Animation = "dead";
                    check.Monitoring = sight.Enabled = false;
                    break;
                case "idle":
                    body.Animation = "idle";
                    check.Monitoring = sight.Enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
