using Com.IronicEntertainment.TobisTimeOdyssey.Elements;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs;
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

        [Export]
        private NodePath
            parentPath;

        public Node2D Parent { get { return GetNode<Node2D>(parentPath); } }

        private static List<string> 
            _JSONIndex = Tobi_Data_JSON.Start;

        private static int 
            _levelIndex;

        public static List<string> JSON_Index { get { return _JSONIndex; } private set { _JSONIndex = value; } }

        public static int Level_Index
        {
            get { return _levelIndex = _JSONIndex[2].ToInt(); }
            set
            {
                _JSONIndex[2] = value.ToString();
                _levelIndex = value;
            }
        }

        public static Level Level { get { return new Level(JSON_Index); } }

        protected override void Init()
        {
            base.Init();
            //if (PlayTest.StartAtBegining) JSON_Index = new List<string> { "Tutorial", "1", "1" };
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

            string pathdb = @"data source=C:\Users\louis\Documents\LoOw\Tobi-sTimeOdyssey\Tobi-sTimeOdysseyV1\Scripts\config\SQLite\TobiDataBank.db";
            string tableName = "Tobi_Data";
            string sqlFileName = "C:\\Users\\louis\\Documents\\LoOw\\Tobi-sTimeOdyssey\\Tobi-sTimeOdysseyV1\\Scripts\\Tools\\SQLs\\Tobi_Data_SQL.sql";

            string sql = System.IO.File.ReadAllText(sqlFileName);

            using (SQLiteConnection connection = new SQLiteConnection(pathdb))
            {
                connection.Open();

                using (SQLiteCommand countRow = new SQLiteCommand($"SELECT COUNT(*) FROM {tableName}", connection))
                {
                    int count = Convert.ToInt32(countRow.ExecuteScalar());
                    GD.Print($"The {tableName} table contains {count} rows.");
                    if (count == 0)
                    {
                        List<string> columns = new List<string>();
                        List<string> values = new List<string>();
                        string columnsString = "";
                        string valueString = "";
                        int value = 0;

                        using (SQLiteCommand command = new SQLiteCommand($"PRAGMA table_info({tableName})", connection))
                        {
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read()) if (reader.GetString(1) != "ID") 
                                { 
                                    columns.Add(reader.GetString(1)); 
                                    values.Add("@value" + (1 + value)); 
                                    value++; 
                                }

                                columnsString = columns[0];
                                valueString = values[0];

                                for (int i = 1; i < value; i++)
                                {
                                    columnsString += ", " + columns[i];
                                    valueString += ", " + values[i];
                                }
                            }
                        } 

                        string query = $"INSERT INTO {tableName} ({columnsString}) VALUES ({valueString})";

                        SQLiteCommand command2 = new SQLiteCommand(query, connection);

                        for (int i = 0; i < value; i++)
                        {
                            command2.Parameters.AddWithValue(values[i], JSON_Index[i]);
                        }

                        command2.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }

            //lvGround TEXT,
            //lvGuards TEXT,
            //lvVillager TEXT,
            //lvLauncher TEXT,
            //lvBouncer TEXT,
            //lvNails TEXT,
            //lvHeat TEXT

            //if (PlayTest.ResetAllCutScenes)
            //{
            //    Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.Begining, true);
            //    Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.End, true);
            //}
            //else if (PlayTest.ResetBeginingCutScenes) Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.Begining, true);
            //else if (PlayTest.ResetEndCutScenes) Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.End, true);

            MOC.Retry();

        }

        #region State Machine
        // Mode
        public override void SetGameModePlay()
        {
            base.SetGameModePlay();

            foreach (Manager manager in GetChildren()) manager.SetGameModePlay();
        }

        public override void SetGameModePause()
        {
            base.SetGameModePause();

            foreach (Manager manager in GetChildren()) manager.SetGameModePause();
        }

        public override void SetGameModeWin()
        {
            base.SetGameModeWin();

            foreach (Manager manager in GetChildren()) manager.SetGameModeWin();
        }

        public override void SetGameModeLose()
        {
            base.SetGameModeLose();

            foreach (Manager manager in GetChildren()) manager.SetGameModeLose();
        }
        // Action
        protected override void DoGameModePlay()
        {
            base.DoGameModePlay();

            if (POC.Enemy_Manager.Number == 0)
            {
                if (Level.Cutscenes.HasPlayed[1]) WinEffect();
                else POC.Camera.PlayCutscenes(CutscenesText.TypeCutscenes.End);
            }
        }

        protected override void DoGameModeLose()
        {
            base.DoGameModeLose();
            if (Input.IsActionJustPressed("Move_Player")) MOC.Retry();
        }
        #endregion

        public void WinEffect()
        {
            Level_Index++;

            if (Level_Index > Tobi_Data_JSON.GetMissionLevelNumber()) if (Tobi_Data_JSON.GetUnlockableMission(JSON_Index).Count > 0) JSON_Index = Tobi_Data_JSON.GetUnlockableMission(JSON_Index)[0];
            else MOC.Quit(this);

            //Tobi_Data_JSON.WrightOverLevelStart();

            //if (PlayTest.ResetAllCutScenes)
            //{
            //    Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.Begining, true);
            //    Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.End, true);
            //}
            //else if (PlayTest.ResetBeginingCutScenes) Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.Begining, true);
            //else if (PlayTest.ResetEndCutScenes) Tobi_Data_JSON.WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes.End, true);

            SetGameModeWin();

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