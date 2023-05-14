using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Threading.Tasks;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers
{
	public partial class Villager : Character
	{
		/// <summary>
		/// Store All Pattern of a NPC
		/// </summary>
		public enum Pattern
		{
			Static,
			Patrolling,
			Surveilling,
		}



		[Export]
		private NodePath
			occluderPath,
			leftPath,
			rightPath,
			forwardPath;



		[Export]
		public Pattern 
			pattern = Pattern.Static;



		[Export]
		protected float
			speed = 64,
			time = 1f,
			startRotation;



		protected LightOccluder2D
			occluder;

		protected RayCast2D
			left,
			right,
			forward;



		protected Action
			doAction;



        protected Vector2
            direction = Vector2.Zero;



        public override void Init()
        {
            base.Init();

			occluder = GetNode<LightOccluder2D>(occluderPath);

			left = GetNode<RayCast2D>(leftPath);
			right = GetNode<RayCast2D>(rightPath);
			forward = GetNode<RayCast2D>(forwardPath);

			left.ExcludeParent = true; right.ExcludeParent = true; forward.ExcludeParent = true;
        }



        public override void _Ready()
		{
			base._Ready();

			switch (pattern)
			{
				case Pattern.Static:
					SetModeStatic();
					break;
				case Pattern.Patrolling:
                    SetModeForward();
                    break;
				case Pattern.Surveilling:
					SetModeSurveilling();
					break;
				default:
					break;
			}
        }



		public override void _Process(double delta)
		{
			base._Process(delta);

            doAction();
        }



		/// <summary>
		/// Define the enemy direction and Set doAction to MoveForward
		/// </summary>
		protected virtual async void SetModeForward()
        {
			direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));


            doAction = MoveForward;


            await Task.Delay(0);
        }

		/// <summary>
		/// Create and play a tween rotating the NPC towards an empty Direction according to the pattern for { time } in miliseconds and set doAction to Turn
		/// </summary>
		protected virtual async void SetModeTurn()
		{
			doAction = Turn;


            float lRotTODO = GlobalRotationDegrees;

			switch (pattern)
			{
				case Pattern.Static:
					break;
				case Pattern.Patrolling:
                    if (right.GetCollider() == null && forward.GetCollider() != null && !(forward.GetCollider() is Villager))
                    {
                        if (lRotTODO == -180) lRotTODO = 180;
                        else if (lRotTODO == -90) lRotTODO = GlobalRotationDegrees = -90;
                        else if (lRotTODO == 90) lRotTODO = GlobalRotationDegrees = 90;


                        lRotTODO += 90;
                    }
                    else if (forward.GetCollider() != null && right.GetCollider() != null && left.GetCollider() == null && !(forward.GetCollider() is Villager))
                    {
                        if (lRotTODO == 180) lRotTODO = -180;
                        else if (lRotTODO == 90) lRotTODO = GlobalRotationDegrees = 90;


                        lRotTODO -= 90;
                    }
                    else if (forward.GetCollider() != null && right.GetCollider() != null && left.GetCollider() != null || forward.GetCollider() is Villager)
                    {
                        if (lRotTODO == -90) lRotTODO = GlobalRotationDegrees = 270;
                        else if (lRotTODO == -180) lRotTODO = GlobalRotationDegrees = 180;


                        lRotTODO -= 180;
                    }
                    break;
				case Pattern.Surveilling:
					if (right.GetCollider() == null)
                    {
                        if (lRotTODO == -180) lRotTODO = 180;
                        else if (lRotTODO == -90) lRotTODO = GlobalRotationDegrees = -90;
                        else if (lRotTODO == 90) lRotTODO = GlobalRotationDegrees = 90;

                        lRotTODO += 90;
					}
					else if (left.GetCollider() == null && right.GetCollider() != null) lRotTODO -= 90;
					else if (left.GetCollider() != null && right.GetCollider() != null) lRotTODO -= 180;
            
                    break;
				default:
					break;
			}



            Tween lRot = GetTree().CreateTween();


            lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotTODO), time);


            lRot.Play();


			await Task.Delay((int)time * 1000);


			lRot.Dispose();

			if (pattern == Pattern.Patrolling) SetModeForward();
			else SetModeSurveilling();
        }

        /// <summary>
        /// set doAction to Static
        /// </summary>
        protected virtual async void SetModeStatic()
		{
			doAction = Static;


            await Task.Delay(0);
        }

		/// <summary>
        /// Create and play a tween rotating the NPC left and right before stopping straight ahead for 4 * { time } in miliseconds and set doAction to Turn
        /// </summary>
        protected virtual async void SetModeSurveilling()
		{
			doAction = Turn;

            Tween lRot = GetTree().CreateTween();


			float lRotation = GlobalRotationDegrees;


			if ((lRotation < 0 && lRotation > 0 - 2) || (lRotation > 0 && lRotation < 0 + 2)) lRotation = 0;
			if ((lRotation < 90 && lRotation > 90 - 2) || (lRotation > 90 && lRotation < 90 + 2)) lRotation = 90;
			if ((lRotation < 180 && lRotation > 180 - 2) || (lRotation > 180 && lRotation < 180 + 2)) lRotation = 180;
			if ((lRotation < -180 && lRotation > -180 - 2) || (lRotation > -180 && lRotation < -180 + 2)) lRotation = -180;
			if ((lRotation < -90 && lRotation > -90 - 2) || (lRotation > -90 && lRotation < -90 + 2)) lRotation = -90;


			if (lRotation == -180) lRotation = 180;
            else if (lRotation == -90) lRotation = GlobalRotationDegrees = -90;
            else if (lRotation == 90) lRotation = GlobalRotationDegrees = 90;

            float lRotationMin = lRotation - 45;
			float lRotationMax = lRotation + 45;

			GD.Print(lRotation);


            lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotation), time / 2);
            lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotationMin), time);
			lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotationMax), time);
			lRot.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotation), time);


            lRot.Play();


            await Task.Delay((int)(time * POC.All_Numbers.Fours) * 1000);


            lRot.Dispose();


			SetModeTurn();
        }



        /// <summary>
        /// MoveForward mode : Move the NPC forward and check for collision
        /// </summary>
        protected virtual void MoveForward()
		{
            GlobalPosition += direction * speed * deltaExt;


            if (forward.GetCollider() != null)
            {
                SetModeTurn();
            }
        }

		/// <summary>
		/// Turn mode :
		/// </summary>
		protected virtual void Turn()
		{

        }

		/// <summary>
		/// Static mode :
		/// </summary>
		protected virtual void Static()
		{

		}
    }
}
