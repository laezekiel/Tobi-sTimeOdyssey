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


            Tween lRot = GetTree().CreateTween();


            lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotTODO), time / 2);
            lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotOrigin), time / 2);
            lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotTODO), time / 2);


            lRot.Play();


            await Task.Delay((int)(1.5f * time * 1000));


            lRot.Dispose();


            if (pattern == Pattern.Patrolling) SetModeForward();
            else SetModeSurveilling();
        }
    }
}
