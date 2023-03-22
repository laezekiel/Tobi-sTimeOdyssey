using Godot;
using System;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Managers;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps
{

	public class HeatWall : Traps
	{
		[Export]
		private NodePath
			heatPath,
			heatTimerPath;

		private Particles2D
			heat;

		private Timer
			heatTimer;

        private bool
            change = false;

        private float
            _heatSpeed = 1;

        public float HeatSpeed
        {
            get { return _heatSpeed; }
            set
            {
                if (heatTimer == null)
                {
                    _heatSpeed = value;
                }
                else
                {
                    heatTimer.WaitTime = value;
                    _heatSpeed = value;
                }
            } 
        }

        public override void Init()
        {
            base.Init();
            heat = GetNode<Particles2D>(heatPath);
			heatTimer = GetNode<Timer>(heatTimerPath);
			heatTimer.Connect("timeout", this, nameof(Timeout));
            heatTimer.WaitTime = HeatSpeed;
            heatTimer.Start();
        }

        public override void _Ready()
		{
			base._Ready();
		}

        #region State Machine
        // Mode
        public override void SetTrapModeOn()
        {
            base.SetTrapModeOn();
        }
        public override void SetTrapModeOff()
        {
            base.SetTrapModeOff();
        }
        // Action
        protected override void DoTrapModeOn()
        {
            if (POC.Player_Manager.Player.GlobalPosition.x < GlobalPosition.x + 32 && POC.Player_Manager.Player.GlobalPosition.x > GlobalPosition.x - 32 &&
                POC.Player_Manager.Player.GlobalPosition.y < GlobalPosition.y + 32 && POC.Player_Manager.Player.GlobalPosition.y > GlobalPosition.y - 32)
            {
                //POC.Player_Manager.
                MOC.Lose(MOC.LoseType.Burnt);
            }
        }
        protected override void DoTrapModeOff()
        {

        }
        #endregion

        private void Timeout()
        {
            if (trapState == DoTrapModeOn)
            {
                heat.Emitting = false;
                SetTrapModeOff();
            }
            else if (trapState == DoTrapModeOff)
            {
                heat.Emitting = true;
                SetTrapModeOn();

            }
        }
    }
}