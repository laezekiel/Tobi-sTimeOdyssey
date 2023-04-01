using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Godot;
using System;
using System.Collections.Generic;

// Author: Louis Bouré
namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs
{
	/// <summary>
	/// Allow one to access the leveldesign.json file 
	/// </summary>
	public static class Tobi_Data_JSON
	{
		/// <summary>
		/// Used to get the needed field of the JSON file 
		/// </summary>
		public enum TobiData
		{
			StartLevel,
			Speaker,
			Tutorial,
			Mission_,
			Bonus___
		}

		public enum MissionData
		{
			path,
			text,
			locked,
			completed,
			unlock
		}

		public enum SceneData
        {
			Tobi,
			Sifu,
			Scientist
		}

		private static string
			pathToJSON = "res://Scripts/config/TobiData.json";

		private static File
			jsonF = new File();

		private static Godot.Collections.Dictionary
			_dataFile = null,
			_tutorial = null,
			_mission = null,
			_bonus = null;

		public static Godot.Collections.Dictionary Data_File
		{
			get
			{
				if (_dataFile != null) return _dataFile;

				jsonF.Open(pathToJSON, File.ModeFlags.Read);

				string lText = jsonF.GetAsText();

				_dataFile = JSON.Parse(lText).Result as Godot.Collections.Dictionary;
				jsonF.Close();

				return _dataFile;
			}
		}

		public static List<string> Start
		{
			get
			{
				List<string> lstart = new List<string>();
				Godot.Collections.Array lTemp = Data_File[TobiData.StartLevel.ToString()] as Godot.Collections.Array;
				foreach (var item in lTemp)
				{
					lstart.Add(item as string);
				}
				return lstart;
			}
		}

		public static Godot.Collections.Dictionary Tutorials
		{
			get
			{
				if (_tutorial != null) return _tutorial;

				_tutorial = Data_File[TobiData.Tutorial.ToString()] as Godot.Collections.Dictionary;

				return _tutorial;
			}
		}

		public static Godot.Collections.Dictionary Missions
		{
			get
			{
				if (_mission != null) return _mission;

				_mission = Data_File[TobiData.Mission_.ToString()] as Godot.Collections.Dictionary;

				return _mission;
			}
		}

		public static Godot.Collections.Dictionary Bonus
		{
			get
			{
				if (_bonus != null) return _bonus;

				_bonus = Data_File[TobiData.Tutorial.ToString()] as Godot.Collections.Dictionary;

				return _bonus;
			}
		}

		public static Godot.Collections.Dictionary LoadFileLV(List<string> pLevel) => LoadFile(GetMissionData(pLevel)[MissionData.path.ToString()].ToString())[pLevel[2]] as Godot.Collections.Dictionary;
		
		public static Godot.Collections.Dictionary LoadFileTXT(List<string> pLevel) => LoadFile(GetMissionData(pLevel)[MissionData.text.ToString()].ToString())[pLevel[2]] as Godot.Collections.Dictionary;

		public static Godot.Collections.Dictionary LoadFile(string pPath)
		{
			jsonF.Open(pPath, File.ModeFlags.Read);

			string lText = jsonF.GetAsText();

			jsonF.Close();

			return JSON.Parse(lText).Result as Godot.Collections.Dictionary;
		}

		public static int GetMissionLevelNumber() => LoadFile(GetMissionData(GameManager.JSON_Index)[MissionData.path.ToString()].ToString()).Count; 

		public static Godot.Collections.Dictionary GetMissionData(List<string> pLevel)
		{
			switch (pLevel[0])
			{
				case "Tutorial":
					return Tutorials[pLevel[1]] as Godot.Collections.Dictionary;
				case "Mission_":
					return Missions[pLevel[1]] as Godot.Collections.Dictionary;
				case "Bonus___":
					return Bonus[pLevel[1]] as Godot.Collections.Dictionary;
				default:
					return null;
			}
		}

		public static List<List<string>> GetUnlockableMission(List<string> pLevel)
        {
			Godot.Collections.Array lUnlock = GetMissionData(pLevel)[MissionData.unlock.ToString()] as Godot.Collections.Array;
			Godot.Collections.Array lUnlock2;

			List<List<string>> lMission = new List<List<string>>();

            foreach (var mission in lUnlock)
			{
				lUnlock2 = mission as Godot.Collections.Array;

				lMission.Add(new List<string>());

                foreach (var str in lUnlock2) lMission[lMission.Count - 1].Add(str.ToString());
            }

			return lMission;
        }

		public static SpriteFrames GetSpeaker(string pSpeaker)
        {
			Godot.Collections.Dictionary lSpeakers = Data_File[TobiData.Speaker.ToString()] as Godot.Collections.Dictionary;
			return GD.Load<SpriteFrames>(lSpeakers[pSpeaker].ToString());
        }

		public static void WrightOverLevelStart()
        {
			jsonF.Open(pathToJSON, File.ModeFlags.ReadWrite);

			string lText = jsonF.GetAsText();
			string[] lTextArray = lText.Split("\n");
			List<string> newStartLevel = GameManager.JSON_Index;

			lTextArray[1] = $"  \"StartLevel\": [ \"{newStartLevel[0]}\", \"{newStartLevel[1]}\", \"{newStartLevel[2]}\" ],";

			lText = "";

			for (int i = 0; i < lTextArray.Length - 1; i++) lText += lTextArray[i] + " \n";

			lText += lTextArray[lTextArray.Length - 1];			

			jsonF.StoreString(lText);
			jsonF.Close();
		}

		public static void WrightoverCinematicsPlayed(CutscenesText.TypeCutscenes pType, bool pReset = false)
		{
			List<string> newStartLevel = GameManager.JSON_Index;
			bool isAGo = (GameManager.Level.Cutscenes.Begining != null && pType == CutscenesText.TypeCutscenes.Begining) || (GameManager.Level.Cutscenes.End != null && pType == CutscenesText.TypeCutscenes.End);

			jsonF.Open(GetMissionData(newStartLevel)[MissionData.text.ToString()].ToString(), File.ModeFlags.ReadWrite);

			string lText = jsonF.GetAsText();
			string[] lTextArray = lText.Split("\n");
			int lIndex = 0;

			lText = "";

			foreach (string line in lTextArray)
            {
				if (line.Contains($"  \"{newStartLevel[2]}\": {{")) break;
				lIndex++;
            }

            if (isAGo)
			{
				for (int i = lIndex; i < lTextArray.Length; i++)
				{
					if (lTextArray[i].Contains(pType.ToString()))
					{
						if (!pReset) lTextArray[i + 1] = "      \"Played\": true,";
						else lTextArray[i + 1] = "      \"Played\": false,";
						break;
					}
				}
			}

			for (int i = 0; i < lTextArray.Length - 1; i++) lText += lTextArray[i] + " \n";

			lText += lTextArray[lTextArray.Length - 1];

			jsonF.StoreString(lText);
			jsonF.Close();
		}
	}
}