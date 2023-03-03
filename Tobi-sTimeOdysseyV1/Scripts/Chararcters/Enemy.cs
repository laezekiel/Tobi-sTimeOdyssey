using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Characters
{

	public class Enemy : Character
	{
        protected override void Init()
        {
            base.Init();
            SetCharacterModeMove();
        }

        public override void _Ready()
		{
			base._Ready();
		}

        #region State Machine
        // Mode 
        #region game State Mode
        #endregion
        #region character State mode
        #endregion
        // Action 
        #region Game State Action
        #endregion
        #region character State action
        protected override void DoCharacterModeStatic()
        {
            
        }
        protected override void DoCharacterModeMove()
        {
            CheckVision();
        }
        #endregion
        #endregion

        public void CheckVision()
        {
            foreach (RayCast2D check in checkCollider)
            {
                if (check.GetCollider() is Player)
                {
                    Player.GetInstance().SetGameModeLose();
                }
            }
        }

    }

}