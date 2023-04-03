using Godot;
using System;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements
{

	public class Element : KinematicBody2D
	{

		protected Action
			gameState;

		public virtual void Init()
		{

		}

		public override void _Ready()
		{
			Init();
		}

        #region State Machine
        // Mode 
        public virtual void SetGameModePlay()
        {
            gameState = DoGameModePlay;
             
        }
        public virtual void SetGameModePause()
        {
            gameState = DoGameModePause;
             
        }
        public virtual void SetGameModeLose()
        {
            gameState = DoGameModeLose;
             
        }
        public virtual void SetGameModeWin()
        {
            gameState = DoGameModeWin;
             
        }
        // Action 
        protected virtual void DoGameModePlay()
        {

        }
        protected virtual void DoGameModePause()
        {

        }
        protected virtual void DoGameModeLose()
        {

        }
        protected virtual void DoGameModeWin()
        {

        }
        #endregion

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            gameState();
        }

    }

}