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
			gameState;

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