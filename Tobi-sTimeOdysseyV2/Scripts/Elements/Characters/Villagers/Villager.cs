using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using Godot.Collections;
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
			Static = 1,
			Patrolling = 2,
			Surveilling = 3,
		}



		[Export]
		private NodePath
			occluderPath,
			leftPath,
			rightPath,
			forwardPath,
			forward2Path,
			killerPath;



		[Export]
		private SpriteFrames
			edo,
			present;



		[Export]
		public Pattern 
			pattern = Pattern.Static;



		[Export]
		protected float
			speed = 64,
			time = 1f;



		protected LightOccluder2D
			occluder;

		protected RayCast2D
			left,
			right,
			forward,
			forward2,
			killer;

		protected Tween
			animation;



		public Action
			doAction;



        protected Vector2
            direction = Vector2.Zero;



		protected bool
			turningRight = true;

        protected float
            startRotation;



        public override void Init()
        {
            base.Init();

			occluder = GetNode<LightOccluder2D>(occluderPath);

			left = GetNode<RayCast2D>(leftPath);
			right = GetNode<RayCast2D>(rightPath);
			forward = GetNode<RayCast2D>(forwardPath);
			forward2 = GetNode<RayCast2D>(forward2Path);
			killer = GetNode<RayCast2D>(killerPath);

			left.ExcludeParent = true; right.ExcludeParent = true; forward.ExcludeParent = true; forward2.ExcludeParent = true;

			switch (State.Era)
			{
				case State.Eras.Edo:
					body.SpriteFrames = edo;
                    break;
				case State.Eras.Present:
                    body.SpriteFrames = present;
                    break;
				default:
					break;
			}

			pattern = (Pattern)State.villagerMode;
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

			switch (State.Current_State)
			{
				case State.GameState.Player_Aiming:
				case State.GameState.Player_Dashing:
                    killer.LookAt(POC.Player_Position);
                    KillPlayer();
					break;
				default:
					break;
			}
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
					lRotTODO = Patrolling_Rotattion(lRotTODO);
                    break;
				case Pattern.Surveilling:
                    lRotTODO = Surveilling_Rotattion(lRotTODO);
                    break;
				default:
					break;
			}


            animation = CreateTween();


            animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotTODO), time);


            animation.Play();


			await Task.Delay((int)time * 1000);

			if (doAction == Turn && (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing))
			{
                if (animation != null) animation.Kill();


                if (pattern == Pattern.Patrolling) SetModeForward();
                else SetModeSurveilling();
            }
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


			animation = CreateTween();


			float lRotation = GlobalRotationDegrees;


			if ((lRotation < 0 && lRotation > 0 - 10) || (lRotation > 0 && lRotation < 0 + 10)) lRotation = 0;
			if ((lRotation < 90 && lRotation > 90 - 10) || (lRotation > 90 && lRotation < 90 + 10)) lRotation = 90;
			if ((lRotation < 180 && lRotation > 180 - 10) || (lRotation > 180 && lRotation < 180 + 10)) lRotation = 180;
			if ((lRotation < -180 && lRotation > -180 - 10) || (lRotation > -180 && lRotation < -180 + 10)) lRotation = -180;
			if ((lRotation < -90 && lRotation > -90 - 10) || (lRotation > -90 && lRotation < -90 + 10)) lRotation = -90;


			if (lRotation == -180 && turningRight) lRotation = 180;
			else if (lRotation == 180 && !turningRight) lRotation = -180;
			else if (lRotation == -90) lRotation = GlobalRotationDegrees = -90;
			else if (lRotation == 90) lRotation = GlobalRotationDegrees = 90;


			float lRotationMin = lRotation - 45;
			float lRotationMax = lRotation + 45;


			animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotation), time / 2);
			animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotationMin), time);
			animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotationMax), time);
			animation.TweenProperty(this, PandS.Properties.Node_2D.Rotation, Mathf.DegToRad(lRotation), time);


			animation.Play();


			await Task.Delay((int)(time * POC.All_Numbers.Fours) * 1000);


			if (doAction == Turn && (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing))
            {
                if (animation != null) animation.Kill();

                SetModeTurn();
			}
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



		/// <summary>
		/// 
		/// </summary>
		/// <param name="pRotTODO"></param>
		/// <returns></returns>
		protected virtual float Patrolling_Rotattion(float pRotTODO)
        {
            if (right.GetCollider() == null && forward.GetCollider() != null && !(forward.GetCollider() is Villager))
            {
                if (pRotTODO == -180) pRotTODO = 180;
                else if (pRotTODO == -90) pRotTODO = GlobalRotationDegrees = -90;
                else if (pRotTODO == 90) pRotTODO = GlobalRotationDegrees = 90;


                pRotTODO += 90;
            }
            else if (forward.GetCollider() != null && right.GetCollider() != null && left.GetCollider() == null && !(forward.GetCollider() is Villager))
            {
                if (pRotTODO == 180) pRotTODO = -180;
                else if (pRotTODO == 90) pRotTODO = GlobalRotationDegrees = 90;


                pRotTODO -= 90;
            }
            else if (forward.GetCollider() != null && right.GetCollider() != null && left.GetCollider() != null || forward.GetCollider() is Villager)
            {
                if (pRotTODO == -90) pRotTODO = GlobalRotationDegrees = 270;
                else if (pRotTODO == -180) pRotTODO = GlobalRotationDegrees = 180;


                pRotTODO -= 180;
            }


			return pRotTODO;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pRotTODO"></param>
		/// <returns></returns>
		protected virtual float Surveilling_Rotattion(float pRotTODO)
        {
            if (right.GetCollider() != null && turningRight) turningRight = false;
            else if (left.GetCollider() != null && !turningRight) turningRight = true;


            if (right.GetCollider() == null && turningRight)
            {
                if (pRotTODO == -180) pRotTODO = 180;
                else if (pRotTODO == 180) pRotTODO = -180;
                else if (pRotTODO == -90) pRotTODO = GlobalRotationDegrees = -90;
                else if (pRotTODO == 90) pRotTODO = GlobalRotationDegrees = 90;


                pRotTODO += 90;
            }
            else if (left.GetCollider() == null && right.GetCollider() != null && turningRight) pRotTODO -= 90;
            if (left.GetCollider() == null && !turningRight)
            {
                if (pRotTODO == 180) pRotTODO = -180;
                else if (pRotTODO == -180) pRotTODO = 180;
                else if (pRotTODO == -90) pRotTODO = GlobalRotationDegrees = -90;
                else if (pRotTODO == 90) pRotTODO = GlobalRotationDegrees = 90;


                pRotTODO -= 90;
            }
            else if (left.GetCollider() != null && right.GetCollider() == null && !turningRight) pRotTODO += 90;
            else if (left.GetCollider() != null && right.GetCollider() != null) pRotTODO -= 180;


            return pRotTODO;
        }

		protected virtual void KillPlayer()
		{
            Array<Node2D> array = check.GetOverlappingBodies();

            foreach (Node2D node in array)
            {
				if (node is Player && killer.GetCollider() is Player) 
				{
					if (animation != null) animation.Kill();

					body.Animation = "happy";

					State.SetLoseTypeToCaught();
					State.SetGameToCaught(); 
				} 
				else continue;
            }
        }
    }
}
