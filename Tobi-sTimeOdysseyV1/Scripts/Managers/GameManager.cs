using Com.IronicEntertainment.TobisTimeOdyssey.Characters;
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

        private static int _index = 1;// Levels_JSON.GetStart();

        public static int Index { get { return _index; } private set { _index = value; } }

        public static Level Level { get { return AllLevels.GetLevel(Index); } }

        protected override void Init()
        {
            base.Init();
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
        }

        #region State Machine
        // Mode
        public override void SetGameModePlay()
        {
            base.SetGameModePlay();
            PlayerManager.GetInstance().SetGameModePlay();
        }
        public override void SetGameModePause()
        {
            base.SetGameModePause();
            PlayerManager.GetInstance().SetGameModePause();
        }
        // Action
        protected override void DoGameModePlay()
        {
            base.DoGameModePlay();
            if (Input.IsActionJustPressed("pause"))
            {
                SetGameModePause();
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