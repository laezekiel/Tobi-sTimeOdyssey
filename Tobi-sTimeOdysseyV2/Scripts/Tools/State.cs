using System;
using System.Collections.Generic;
using System.IO;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Com.BeerAndDev.TobisTimeOdyssey.Scenes;
using Godot;
using Godot.Collections;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Tools
{
    static public class State
    {
        public enum GameState
        {
            Loading,
            Player_Aiming,
            Player_Dashing,
            Player_Caught,
            Cinematics,
            Pause,
        }

        public enum LoseType
        {
            dead,
            burnt,
            caught,
        }

        public enum Eras
        {
            Edo,
            Present
        }

        public enum Places
        {
            Dojo,
            Village,
            Secret_Passage,
            Castle,
        }

        public enum MissionType
        {
            Tutorial,
            Mission,
            Bonus
        }



        public struct LevelIndex
        {
            public LevelIndex(MissionType pType, int pGroup, int pLevel) 
            { 
                _type = pType;
                _group = pGroup;
                _level = pLevel;
            }

            private int
                _group,
                _level;

            private MissionType
                _type;

            public int Group { get { return _group; } }
            public int Level { get { return _level; } }
            public MissionType Type { get { return _type; } }
        }



        public static GameState Current_State { get { return _curentState; } }

        private static GameState _curentState = GameState.Loading;



        public static LoseType Lose_Type { get { return _loseType; } }

        private static LoseType _loseType;


        
        public static Eras Era { get { return _era; } set { _era = value; } }

        private static Eras _era = Eras.Edo;


        
        public static Places Place { get { return _place; } set { _place = value; } }

        private static Places _place = Places.Dojo;



        public static LevelIndex Level_Index { get { return _levelIndex; } }

        private static LevelIndex _levelIndex = new LevelIndex(MissionType.Tutorial, 1, 1);



        public static System.Collections.Generic.Dictionary<State.MissionType, Array<Array<string>>>
            allFilePath;



        public static int
            villagerMode;



        public static void SetGameToLoad() => _curentState = GameState.Loading;
        
        public static void SetGameToAim() => _curentState = GameState.Player_Aiming;
        
        public static void SetGameToDash() => _curentState = GameState.Player_Dashing;
        
        public static void SetGameToCaught() => _curentState = GameState.Player_Caught;
        
        public static void SetGameToCinematic() => _curentState = GameState.Cinematics;
        
        public static void SetGameToPause() => _curentState = GameState.Pause;



        public static void SetLoseTypeToBurnt() => _loseType = LoseType.burnt;
        
        public static void SetLoseTypeToCaught() => _loseType = LoseType.caught;
        
        public static void SetLoseTypeToDead() => _loseType = LoseType.dead;



        public static void SetEraToEdo() => _era = Eras.Edo;

        public static void SetEraToPresent() => _era = Eras.Present;



        public static string GetLevelJSONPath(State.LevelIndex pLevelIndex) => allFilePath[pLevelIndex.Type][pLevelIndex.Group - 1][pLevelIndex.Level - 1];

        public static Variant ReadJSON(string jsonString)
        {
            Json JSON = new Json();
            Error result = JSON.Parse(jsonString);

            if (result != Error.Ok)
            {
                // Handle error
                GD.Print("Error parsing JSON: " + result);
                Variant nill = new Variant();
                return nill;
            }

            return JSON.Data;
        }

        public static object AsACSObject(Variant pObject)
        {
            switch (pObject.Obj)
            {
                case double:
                    double intTemp = (double)pObject.Obj;
                    return Convert.ToInt32(intTemp);
                case string:

                    return pObject.Obj;

                case Godot.Collections.Array:

                    Godot.Collections.Array arrayTemp = pObject.AsGodotArray();

                    List<object> newList = new List<object>();

                    foreach (var item in arrayTemp)
                    {
                        newList.Add(AsACSObject(item));
                    }

                    return newList;

                default:
                    return null;
            }
        }
    }
}
