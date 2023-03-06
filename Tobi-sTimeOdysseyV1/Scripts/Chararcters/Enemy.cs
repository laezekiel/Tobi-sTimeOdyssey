using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Characters
{
	public class Enemy : Character
	{
        public List<float>
            rotation = new List<float>();

        public List<Vector2>
            path = new List<Vector2>();

        private int
            rotationIndex = 1,
            pathIndex = 1;

        public override void _Ready()
		{
			base._Ready();
            TurnAround();
		}

        #region State Machine
        // Mode 
        // Action 
        protected override void DoGameModePlay()
        {
            base.DoGameModePlay();
            CheckVision();
        }
        #endregion

        private void CheckVision()
        {
            foreach (RayCast2D check in checkCollider)
            {
                if (check.GetCollider() is Player)
                {
                    MOC.Quit(this);
                }
            }
        }

        private void TurnAround()
        {
            Tween lTurn = new Tween();
            AddChild(lTurn);
            if (rotation.Count > 1)
            {
                lTurn.InterpolateProperty(this, "rotation_degrees", RotationDegrees, rotation[rotationIndex], 
                    2 * Mathf.Abs((RotationDegrees - rotation[rotationIndex])/90), Tween.TransitionType.Elastic, Tween.EaseType.InOut);
                if (rotationIndex == rotation.Count - 1) rotationIndex = 0;
                else rotationIndex++;
            }
            float lDelay = lTurn.GetRuntime();
            lTurn.InterpolateCallback(this, lDelay, nameof(Move));
            lTurn.Start();
        }

        private void Move()
        {
            Tween lMove = new Tween();
            AddChild(lMove);
            float lDelay = 0;
            if (path.Count > 1)
            {
                lMove.InterpolateProperty(this, "global_position", GlobalPosition, path[pathIndex], 
                    2 * Mathf.Abs(GlobalPosition.DistanceTo(path[pathIndex])/128), Tween.TransitionType.Linear, Tween.EaseType.InOut);
                if (pathIndex == path.Count - 1) pathIndex = 0;
                else pathIndex++;
            }
            else
            {
                lDelay++;
            }
            lDelay += lMove.GetRuntime();
            lMove.InterpolateCallback(this, lDelay + 1, nameof(TurnAround));
            lMove.Start();
        }

    }

}