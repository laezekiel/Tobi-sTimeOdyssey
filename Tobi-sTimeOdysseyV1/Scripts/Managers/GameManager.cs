using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs;
using Godot;
using System;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{

    public class GameManager : Manager
    {
        #region singleton
        static private GameManager instance;
		
		static public GameManager GetInstance() 
        {
            if (instance == null) instance = new GameManager ();
            return instance;

        }

        static private GameManager Instance { get {return GetInstance(); } }
        #endregion

        private GameManager (): base() {}

        private static int _index = Levels_JSON.GetStart();

        public static int Index { get { return _index; } private set { _index = value; } }

        public static Level Level { get { return AllLevels.GetLevel(Index); } }

        protected override void Init()
        {
            base.Init();
            POC.Player_Manager.Player.Init();
        }
        public override void _Ready()
        {
            base._Ready();
            #region singleton
            if (instance != null){  
                QueueFree();
                GD.Print(nameof(GameManager) + " Instance already exist, destroying the last added.");
                return;
            }
            
            instance = this;
            #endregion
            SetGameModePlay();
            POC.Field_Manager.SetField();
            MOC.Retry();
        }

        #region State Machine
        // Mode
        public override void SetGameModePlay()
        {
            base.SetGameModePlay();
            foreach (Manager manager in GetChildren())
            {
                manager.SetGameModePlay();
            }
        }
        public override void SetGameModePause()
        {
            base.SetGameModePause();
            foreach (Manager manager in GetChildren())
            {
                manager.SetGameModePause();
            }
        }
        public override void SetGameModeWin()
        {
            base.SetGameModeWin();
            foreach (Manager manager in GetChildren())
            {
                manager.SetGameModeWin();
            }
        }
        public override void SetGameModeLose()
        {
            base.SetGameModeLose();
            foreach (Manager manager in GetChildren())
            {
                manager.SetGameModeLose();
            }
        }
        // Action
        protected override void DoGameModePlay()
        {
            base.DoGameModePlay();
            if (Input.IsActionJustPressed("pause"))
            {
                SetGameModePause();
            }
            if (POC.Enemy_Manager.Number == 0)
            {
                Index++;
                if (Index < AllLevels.GetAllLevel().Count)
                {
                    MOC.Retry();
                }
                else
                {
                    MOC.Quit(this);
                }
            }
        }
        protected override void DoGameModePause()
        {
            base.DoGameModePause();
            if (Input.IsActionJustPressed("pause"))
            {
                SetGameModePlay();
            }
        }
        protected override void DoGameModeLose()
        {
            base.DoGameModeLose();
            if (Input.IsActionJustPressed("Move_Player"))
            {
                MOC.Retry();
            }
        }
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