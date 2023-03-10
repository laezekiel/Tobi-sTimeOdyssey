using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{
	public class Manager : Node
	{
		private Action
			gameState;

		protected virtual void Init()
        {

        }

		public override void _Ready()
		{
			base._Ready();
			Init();
		}

        public override void _Process(float delta)
        {
            base._Process(delta);
			gameState();
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

	}

}