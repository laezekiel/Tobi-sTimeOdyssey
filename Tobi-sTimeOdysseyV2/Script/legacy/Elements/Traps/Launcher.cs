using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps
{
	public class Launcher : Traps
	{
        [Export]
        private PackedScene
            shotFactory;

        [Export]
        private NodePath
            shotTimerPath,
            shotContainerPath,
            visorPath,
            killerPath;

        private float
            _shotTimerTime = 1;

        public float ShotSpeed 
        { 
            get { return _shotTimerTime; }
            set
            {
                if (shotTimer == null)
                {
                    _shotTimerTime = value;
                }
                else
                {
                    shotTimer.WaitTime = value;
                    _shotTimerTime = value;
                }
            }
            
        } 

        private Timer
            shotTimer;

        private Node2D
            shotContainer,
            visor;

        private RayCast2D
            killer;

        private bool
            blinking = false;

        public override void Init()
        {
            shotTimer = GetNode<Timer>(shotTimerPath);
            shotTimer.Connect("timeout", this, nameof(ChangeMode));
            shotContainer = GetNode<Node2D>(shotContainerPath);
            killer = GetNode<RayCast2D>(killerPath);
            visor = GetNode<Node2D>(visorPath);
            shotTimer.WaitTime = ShotSpeed;
            base.Init();
        }

        public override void _Ready()
		{
			base._Ready();
        }

        #region State Machine
        // Mode
        public override void SetTrapModeOn()
        {
            shotTimer.Start();
            visor.Visible = true;
            blinking = false;
            base.SetTrapModeOn();
        }
        public override void SetTrapModeOff()
        {
            shotTimer.Start();
            visor.Visible = false;
            base.SetTrapModeOff();
        }
        // Action
        protected override void DoTrapModeOn()
        {
            if (shotTimer.WaitTime / 10 > shotTimer.TimeLeft && !blinking)
            {
                Blinking();
            }
            if (killer.GetCollider() is Player)
            {
                MOC.Lose(MOC.LoseType.Killed);
            }
        }

        protected override void DoTrapModeOff()
        {
            
        }
        #endregion

        private void Blinking()
        {
            Tween lBlink = new Tween();
            AddChild(lBlink);
            lBlink.InterpolateProperty(visor, "visible", true, false, shotTimer.WaitTime / 10 / 5);
            float lDelay = lBlink.GetRuntime();
            lBlink.InterpolateProperty(visor, "visible", false, true, shotTimer.WaitTime / 10 / 5, delay: lDelay);
            lDelay = lBlink.GetRuntime();
            lBlink.InterpolateProperty(visor, "visible", true, false, shotTimer.WaitTime / 10 / 5, delay: lDelay);
            lDelay = lBlink.GetRuntime();
            lBlink.InterpolateProperty(visor, "visible", false, true, shotTimer.WaitTime / 10 / 5, delay: lDelay);
            lDelay = lBlink.GetRuntime();
            lBlink.InterpolateProperty(visor, "visible", true, false, shotTimer.WaitTime / 10 / 5, delay: lDelay);
            lBlink.Start();
        }

        private void ChangeMode()
        {
            if (trapState == DoTrapModeOn) SetTrapModeOff();
            else SetTrapModeOn();
        }
    }
}