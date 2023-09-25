using Com.BeerAndDev.TobisTimeOdyssey.Elements.AddOns;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers.Enemies;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters
{
	public partial class Character : CharacterBody2D
    {
        [Export]
        private NodePath
            colliderPath,
            checkPath,
            bodyPath,
            sightPath;



        protected CollisionPolygon2D
            collider;

        public Area2D
            check;

        public AnimatedSprite2D
            body;

        protected PointLight2D
            sight;



        protected float
            deltaExt;



        public virtual void Init()
        {
            collider = GetNode<CollisionPolygon2D>(colliderPath);

            check = GetNode<Area2D>(checkPath);

            body = GetNode<AnimatedSprite2D>(bodyPath);

            sight = GetNode<PointLight2D>(sightPath);
        }



        public override void _Ready()
		{
			base._Ready();

            Init();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

            deltaExt = (float)delta;



            switch (State.Current_State)
            {
                case State.GameState.Player_Win:
                    GameStateIsWin();
                    break;
                case State.GameState.Loading:
                    GameStateIsLoading();
                    break;
                case State.GameState.Player_Aiming:
                    GameStateIsAiming();
                    break;
                case State.GameState.Player_Dashing:
                    GameStateIsDashing();
                    break;
                case State.GameState.Player_Caught:
                    GameStateIsCaught();
                    break;
                case State.GameState.Cinematics:
                    GameStateIsCinematics();
                    break;
                case State.GameState.Pause:
                    GameStateIsPause();
                    break;
                default:
                    break;
            }
        }



        public virtual void GameStateIsLoading() { }

        public virtual void GameStateIsAiming() { }

        public virtual void GameStateIsDashing() { }

        public virtual void GameStateIsCaught() { }

        public virtual void GameStateIsWin() { }

        public virtual void GameStateIsCinematics() { }

        public virtual void GameStateIsPause() { }
	}
}
