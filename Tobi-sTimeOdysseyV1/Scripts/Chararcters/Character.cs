using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Characters
{

	public class Character : KinematicBody2D
	{
		[Export]
		private NodePath
			checkColliderContainerPath;

		protected List<RayCast2D>
			checkCollider = new List<RayCast2D>();

		protected Action
			gameState,
			characterState;

		protected virtual void Init()
        {
            foreach (RayCast2D check in GetNode<Node2D>(checkColliderContainerPath).GetChildren()) 
            {
                checkCollider.Add(check);
                check.AddException(this);
            }
            SetGameModePlay();
        }
			
		public override void _Ready()
		{
			base._Ready();
            Init();
		}

        #region State Machine
        // Mode 
        #region game State Mode
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
        #endregion
        #region character State mode
        public virtual void SetCharacterModeMove()
        {
            characterState = DoCharacterModeMove;
        }
        public virtual void SetCharacterModeStatic()
        {
            characterState = DoCharacterModeStatic;
        }
        #endregion
        // Action 
        #region Game State Action
        protected virtual void DoGameModePlay()
        {
            characterState();
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
        #region character State action
        protected virtual void DoCharacterModeMove()
        {

        }
        protected virtual void DoCharacterModeStatic()
        {

        }
        #endregion
        #endregion

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            gameState();
        }
	}

}