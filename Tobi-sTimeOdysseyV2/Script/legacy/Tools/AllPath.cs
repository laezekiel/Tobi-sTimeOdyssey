using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{

	public static class AllPath
	{
		private static string
			thisPath = "./Scripts/Tools",
			json = "/JSONs",
			sql = "/SQLs",
			playerData = "/PlayerData",
			gameData = "/GameData",
			levelData = "/LevelData";

		public static string
			levelDataPath = thisPath + sql + levelData,
			gameDataPath = thisPath + sql + gameData,
			playerDataPath = thisPath + sql + playerData;

		public static string Data_Base { get { return thisPath + sql + "/TobiDataBank.db"; } }

		public static Dictionary<int, string> Player_Data
		{
			get
			{
				return new Dictionary<int, string>()
				{
					{ 1, playerDataPath + "/Create_Player_Data_Table.sql" },
					{ 2, playerDataPath + "/Create_Mission_Data_Table.sql" },
					{ 3, playerDataPath + "/Create_Level_Data_Table.sql" },
					{ 4, playerDataPath + "/first_Player_Data_Update.sql" },
					{ 5, playerDataPath + "/first_Mission_Data_Update.sql" },
					{ 6, playerDataPath + "/first_Level_Data_Update.sql" },
				};
			}
		}

		public static Dictionary<int, string> Game_Data
		{
			get
			{
				return new Dictionary<int, string>()
				{
					{ 1, gameDataPath + "/Create_Scenes_Character_Table.sql" },
					{ 2, gameDataPath + "/Scenes_Character_Update.sql" },
				};
			}
		}

		public static Dictionary<int, string> All_Level
		{
			get
			{
				return new Dictionary<int, string>()
				{
					{ 1, levelDataPath + "/Create_Level_Data_Table.sql" },
					{ 2, levelDataPath + "/Create_Level_Map_Table.sql" },
					{ 3, levelDataPath + "/Create_Level_Enemies_Table.sql" },
					{ 4, levelDataPath + "/Create_Level_Traps_Table.sql" },
					{ 5, levelDataPath + "/Create_Level_Unlock_Table.sql" },
				};
			}
		}

		public static string Game_Container { get { return "res://Scenes/GameContainer.tscn"; } }

		public static string JSON_localisation { get { return thisPath + json; } }
	}
}