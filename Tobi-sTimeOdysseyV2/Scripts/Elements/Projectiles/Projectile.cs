using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Projectiles
{
	public partial class Projectile : Node2D
	{
		[Export]
		private NodePath
			killerPath;



		protected RayCast2D
			killer;



		public virtual void Init()
		{
			killer = GetNode<RayCast2D>(killerPath);
		}



		public override void _Ready()
		{
			base._Ready();
			Init();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

            switch (State.Current_State)
            {
                case State.GameState.Loading:
                    break;
                case State.GameState.Player_Aiming:
                case State.GameState.Player_Dashing:
                    Position += new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized();

                    if (killer.GetCollider() is Player)
                    {
                        State.SetLoseTypeToDead();
                        State.SetGameToCaught();
                        QueueFree();
                    }
                    else if (killer.GetCollider() is Wall)
                    {
                        QueueFree();
                    }
                    break;
                case State.GameState.Player_Caught:
                    break;
                case State.GameState.Cinematics:
                    break;
                case State.GameState.Pause:
                    break;
                default:
                    break;
            }
        }
	}
}
