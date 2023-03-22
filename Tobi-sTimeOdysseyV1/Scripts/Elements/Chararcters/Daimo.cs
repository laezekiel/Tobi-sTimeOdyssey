using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters
{

	public class Daimo : Enemy
	{
		[Export]
		private NodePath
			awarenessPath,
			awarenessColliderPath;

		private Node2D
			awareness;

        public override void Init()
        {
            base.Init();
			awareness = GetNode<Node2D>(awarenessPath);
            foreach (RayCast2D check in GetNode<Node2D>(awarenessColliderPath).GetChildren())
            {
                checkCollider.Add(check);
                check.AddException(this);
            }
        }



        #region State Machine
        // Mode 
        // Action 
        protected override void DoGameModePlay()
        {
            base.DoGameModePlay();
            awareness.RotationDegrees++;
        }
        #endregion
    }

}