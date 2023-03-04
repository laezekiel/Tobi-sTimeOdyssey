using Godot;
using System;
using System.Collections.Generic;

// Author: Louis Bouré
namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs
{
	/// <summary>
	/// Allow one to access the leveldesign.json file 
	/// </summary>
	public static class  Levels_JSON
	{
		/// <summary>
		/// Used to get the needed field of the JSON file for a level
		/// </summary>
		public enum Field
		{
			time,
			skin,
			map
		}

		private static string 
			pathToJSON = "res://Scripts/config/leveldesign.json",
			START_LEVEL = "StartLevel",
			LEVELS = "levels",
			TIME = "time",
			MAP = "map",
			SKIN = "skin";

		private static File 
			files = new File();

		private static Godot.Collections.Dictionary
			firstJSONDIC;

		private static List<Godot.Collections.Dictionary>
			levels,
			map;

		/// <summary>
		/// make sure the the firstJSONDIC and levels are not null and if they are fill them
		/// </summary>
		private static void Init()
        {
            if (firstJSONDIC == null)
            {
				files.Open(pathToJSON, File.ModeFlags.Read);
				string fileText = files.GetAsText();;
				firstJSONDIC = JSON.Parse(fileText).Result as Godot.Collections.Dictionary;
			}
            if (levels == null)
            {
				levels = new List<Godot.Collections.Dictionary>();
				Godot.Collections.Array lLevels = firstJSONDIC[LEVELS] as Godot.Collections.Array;
                foreach (var dic in lLevels)
                {
                    levels.Add(dic as Godot.Collections.Dictionary);
                }
			}
		}

		/// <summary>
		/// GetStart allow one to get the index of the first Godot.Collections.Dictionary by using the LevelStart field of the JSON file stocked in firstJSONDIC
		/// </summary>
		/// <returns> int </returns>
		public static int GetStart()
        {
			Init();
			float tInt = (float) firstJSONDIC[START_LEVEL];
			return (int)tInt ;
		}

		/// <summary>
		/// GetLevel make one able to get the full Godot.Collections.Dictionary in levels coresponding to the index 
		/// </summary>
		/// <param name="pLevel"> index of a Godot.Collections.Dictionary inside levels </param>
		/// <returns> Godot.Collections.Dictionary </returns>
		public static object GetLevel(int pLevel)
        {
            Init();
            return levels[pLevel];
        }

		/// <summary>
		/// GetLevel make one able to get a certain field of one of the Godot.Collections.Dictionary in levels according to the Field Chosen
		/// </summary>
		/// <param name="pLevel"></param>
		/// <param name="pField"></param>
		/// <returns>
		/// object <br/><br/><br/>
		/// /!\ Warning /!\<br/><br/><br/>
		/// if your Levels.Field is not filled or wrong, you have to convert it into a Godot.Collections.Dictionary to make use if it<br/><br/>
		/// </returns>
		public static object GetLevelInfo(int pLevel, Field pField)
        {
            Init();
            switch (pField)
            {
                case Field.time:
					return levels[pLevel][TIME];
				case Field.skin:
					return levels[pLevel][SKIN];
				case Field.map:
					return levels[pLevel][MAP];
				default:
					return levels[pLevel];
            }
        }

		/// <summary>
		/// Get the total number of level in the JSON
		/// </summary>
		/// <returns></returns>
		public static int GetNumberLevel()
		{
			Init();
			return levels.Count;
        }
    }

}