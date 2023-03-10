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

		private Light2D
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

        protected override void Init()
        {
            base.Init();
            heat = GetNode<Light2D>(heatPath);
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
                MOC.Retry();
            }
            if (1.5f * heatTimer.WaitTime / 10 > heatTimer.TimeLeft && !change)
            {
                change = true;
                ChangeTemperature();
            }
        }
        protected override void DoTrapModeOff()
        {
            if (1.5f * heatTimer.WaitTime / 10 > heatTimer.TimeLeft && change)
            {
                change = false;
                ChangeTemperature();
            }
        }
        #endregion

        private void ChangeTemperature()
        {
            Tween lHeat = new Tween();
            AddChild(lHeat);
            if (trapState == DoTrapModeOn)
            {
                lHeat.InterpolateProperty(heat, "color", new Color(1, 0, 0), new Color(0, 0, 1), heatTimer.TimeLeft);
            }
            else if (trapState == DoTrapModeOff)
            {
                lHeat.InterpolateProperty(heat, "color", new Color(0, 0, 1), new Color(1, 0, 0), heatTimer.TimeLeft);
            }
            lHeat.Start();
        }

        private void Timeout()
        {
            if (trapState == DoTrapModeOn)
            {
                heat.Color = new Color(1, 0, 0);
                SetTrapModeOff();
            }
            else if (trapState == DoTrapModeOff)
            {
                heat.Color = new Color(0, 0, 1);
                SetTrapModeOn();
            }
        }
    }
}