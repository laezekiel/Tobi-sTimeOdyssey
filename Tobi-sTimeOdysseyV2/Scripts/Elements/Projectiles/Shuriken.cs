using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Projectiles
{
	public partial class Shuriken : Projectile
	{
		[Export]
		private NodePath
			bodyPath;



		private Node2D
			body;



        public override void Init()
        {
            base.Init();

			body = GetNode<Node2D>(bodyPath);
        }
        public override void _Ready()
		{
			base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

            if (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing) body.RotationDegrees++;
		}
	}
}
