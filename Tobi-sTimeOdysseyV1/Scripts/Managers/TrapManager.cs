using Godot;
using System;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{

	public class TrapManager : Manager
	{
        #region singleton
        static private TrapManager instance;

        static public TrapManager GetInstance()
        {
            if (instance == null) instance = new TrapManager();
            return instance;

        }

        static private TrapManager Instance { get { return GetInstance(); } }
        #endregion

        private TrapManager() : base() { }

        [Export]
        private NodePath
            trapsPath;

        public Node2D Traps
        {
            get { return GetNode<Node2D>(trapsPath); }
        }

        public override void _Ready()
        {

            base._Ready();
            #region singleton
            if (instance != null)
            {
                QueueFree();
                GD.Print(nameof(TrapManager) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion
        }

        #region State Machine
        // Mode
        public override void SetGameModePlay()
        {
            base.SetGameModePlay();
            foreach (Traps traps in Traps.GetChildren())
            {
                traps.SetGameModePlay();
            }
        }
        public override void SetGameModePause()
        {
            base.SetGameModePause();
            foreach (Traps traps in Traps.GetChildren())
            {
                traps.SetGameModePause();
            }
        }
        public override void SetGameModeWin()
        {
            base.SetGameModeWin();
            foreach (Traps traps in Traps.GetChildren())
            {
                traps.SetGameModeWin();
            }
        }
        public override void SetGameModeLose()
        {
            base.SetGameModeLose();
            foreach (Traps traps in Traps.GetChildren())
            {
                traps.SetGameModeLose();
            }
        }
        // Action 
        #endregion

        public void ResetTraps()
        {
            foreach (Traps trap in Traps.GetChildren())
            {
                trap.QueueFree();
            }
        }

        protected override void Dispose(bool pDisposing)
        {
            #region singleton
            if (pDisposing && instance == this) instance = null;
            #endregion
            base.Dispose(pDisposing);
        }

    }

}