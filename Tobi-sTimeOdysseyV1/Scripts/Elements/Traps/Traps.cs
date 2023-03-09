using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps
{

	public class Traps : Element
	{
        protected RandomNumberGenerator
            rand = new RandomNumberGenerator();

		public Action
			trapState;

        protected override void Init()
        {
            rand.Randomize();
            SetTrapModeOn();
            base.Init();
        }

        public override void _Ready()
		{
			base._Ready();
		}

        #region State Machine
        // Mode 
        public virtual void SetTrapModeOn()
        {
            trapState = DoTrapModeOn;
        }
        public virtual void SetTrapModeOff()
        {
            trapState = DoTrapModeOff;
        }
        // Action 
        protected virtual void DoTrapModeOn()
        {

        }
        protected virtual void DoTrapModeOff()
        {

        }
        #endregion

        public override void _Process(float delta)
        {
            base._Process(delta);
            trapState();
        }
    }

}