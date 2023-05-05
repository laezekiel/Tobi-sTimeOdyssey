using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{
	public class Manager : Node
	{
		
		protected virtual void Init()
        {

        }

		public override void _Ready()
		{
			base._Ready();
			Init();
		}

	}

}