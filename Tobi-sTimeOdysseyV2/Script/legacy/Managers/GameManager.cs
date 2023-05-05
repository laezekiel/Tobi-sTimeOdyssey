using Com.IronicEntertainment.TobisTimeOdyssey.Elements;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.UI;
using Godot;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

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

        public static Level Current_Level { get { return new Level(SQLCommands.GetAllCell(SQLCommands.Table.Player_Generic_Data, 1)); } }

        protected override void Init()
        {
            base.Init();


            GD.Print(Current_Level.Map.ToString());


            POC.Player_Manager.Player.Init();
        }

        public override void _Ready()
        {
            base._Ready();
            #region singleton
            if (instance != null) {
                QueueFree();
                GD.Print(nameof(GameManager) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion

            MOC.Retry();
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            if (Input.IsActionJustPressed("Move_Player")) MOC.Retry();
        }

        public void WinEffect()
        {
            POC.Camera.AddWinScreen();
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