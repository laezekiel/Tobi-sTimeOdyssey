using Com.IronicEntertainment.TobisTimeOdyssey.Elements;
using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.UI
{

	public class WinScreen : Node2D
	{
		[Export]
		private NodePath
			nextPath;

		private Button
			next;

		private void Init()
		{
			next = GetNode<Button>(nextPath);

			next.Connect("pressed", this, nameof(NextLevel));
		}

		public override void _Ready()
		{
			Init();
		}

		public void NextLevel()
		{
			POC.Player_Manager.ResetPlayer();
			POC.Enemy_Manager.ResetCharacter();
			POC.Trap_Manager.ResetTraps();
			POC.Field_Manager.Retry();

			QueueFree();
		}

	}

}