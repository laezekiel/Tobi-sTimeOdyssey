using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters
{

	public class Character : Element
	{
		[Export]
		private NodePath
			checkColliderContainerPath;

		protected List<RayCast2D>
			checkCollider = new List<RayCast2D>();

		protected override void Init()
        {
            foreach (RayCast2D check in GetNode<Node2D>(checkColliderContainerPath).GetChildren()) 
            {
                checkCollider.Add(check);
                check.AddException(this);
            }
			base.Init();
        }
			
		public override void _Ready()
		{
			base._Ready();
		}
	}

}