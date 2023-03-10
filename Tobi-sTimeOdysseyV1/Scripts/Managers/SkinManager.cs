using Godot;
using System;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{

	public class SkinManager : Manager
    {
        #region singleton
        static private SkinManager instance;

        static public SkinManager GetInstance()
        {
            if (instance == null) instance = new SkinManager();
            return instance;

        }

        static private SkinManager Instance { get { return GetInstance(); } }
        #endregion

        private SkinManager() : base() { }

        public override void _Ready()
		{

            base._Ready();
            #region singleton
            if (instance != null)
            {
                QueueFree();
                GD.Print(nameof(SkinManager) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion
        }

        #region State Machine
        // Mode
        // Action
        #endregion

        protected override void Dispose(bool pDisposing)
        {
            #region singleton
            if (pDisposing && instance == this) instance = null;
            #endregion
            base.Dispose(pDisposing);
        }

    }

}