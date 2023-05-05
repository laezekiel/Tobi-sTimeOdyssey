using Com.IronicEntertainment.TobisTimeOdyssey.Elements;
using Com.IronicEntertainment.TobisTimeOdyssey.UI;
using Godot;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{

	public struct Level
	{
		private const string 
			ROW_MAP = "rowMap",
			ERA = "Era",
			PLACE = "Place",
			PXPOS = "PxPos",
			PYPOS = "PyPos",
			ENEMY_TYPE = "eType",
			ENEMY_X_START_POSITION = "eXinPos",
			ENEMY_Y_START_POSITION = "eYinPos",
			ENEMY_START_ROTATION = "eRin",
			ENEMY_MOVES = "eMoves",
			ENEMY_ROTATES = "eRotates",
			TRAPS_TYPE = "tType",
			TRAPS_X_START_POSITION = "tXinPos",
			TRAPS_Y_START_POSITION = "tYinPos",
			TRAPS_START_ROTATION = "tRin";


		public enum EnemyKey
		{
			Type,
			Start_Pos,
			Start_Rot,
			Moves,
			Rotates,
        }

		public enum TrapKey
		{
			Type,
			Start_Pos,
			Start_Rot,
		}


		public Level(List<object> pObjects)
		{
			// Creating local variable
			int lIndex = 0;


			// initiating Struct parameters 
			_path = $"{ AllPath.levelDataPath }/{ pObjects[0] }/{ pObjects[1] }/{ pObjects[2] }.sql";
			_map = new List<string>();
			_enemies = new List<Dictionary<EnemyKey, object>>();
			_traps = new List<Dictionary<TrapKey, object>>();


			// opening database
			SQLCommands.ResetLevelTables();

			SQLCommands.dataBase.Open();

			SQLCommands.ExecuteExternatNonQuery(_path);

			SQLCommands.dataBase.Close();


			// filling data
			_era = SQLCommands.GetCell(SQLCommands.Table.Level_Data, 1, ERA) as string;
			_place = SQLCommands.GetCell(SQLCommands.Table.Level_Data, 1, PLACE) as string;
			_player = new Vector2(Convert.ToInt32(SQLCommands.GetCell(SQLCommands.Table.Level_Data, 1, PXPOS)), Convert.ToInt32(SQLCommands.GetCell(SQLCommands.Table.Level_Data, 1, PYPOS)));


			// filling map
			SQLCommands.dataBase.Open();

			lIndex = SQLCommands.GetTableLength(SQLCommands.Table.Level_Map);

			SQLCommands.dataBase.Close();


			for (int i = 1; i <= lIndex; i++) _map.Add(SQLCommands.GetCell(SQLCommands.Table.Level_Map, i, ROW_MAP) as string);


			// filling enemies
			SQLCommands.dataBase.Open();

			lIndex = SQLCommands.GetTableLength(SQLCommands.Table.Level_Enemies);

			SQLCommands.dataBase.Close();


			for (int i = 1; i <= lIndex; i++)
			{
				_enemies.Add(new Dictionary<EnemyKey, object>()
				{
					{ EnemyKey.Type, SQLCommands.GetCell(SQLCommands.Table.Level_Enemies, i, ENEMY_TYPE) as string },
					{ EnemyKey.Start_Pos, new Vector2(Convert.ToInt32(SQLCommands.GetCell(SQLCommands.Table.Level_Enemies, i, ENEMY_X_START_POSITION)), Convert.ToInt32(SQLCommands.GetCell(SQLCommands.Table.Level_Enemies, i, ENEMY_Y_START_POSITION))) },
					{ EnemyKey.Start_Rot, Convert.ToSingle(SQLCommands.GetCell(SQLCommands.Table.Level_Enemies, i, ENEMY_START_ROTATION)) },
					{ EnemyKey.Moves, string.Equals(SQLCommands.GetCell(SQLCommands.Table.Level_Enemies, i, ENEMY_MOVES) as string, "Y", StringComparison.OrdinalIgnoreCase) },
					{ EnemyKey.Rotates, string.Equals(SQLCommands.GetCell(SQLCommands.Table.Level_Enemies, i, ENEMY_ROTATES) as string, "Y", StringComparison.OrdinalIgnoreCase) }
				});
			}


			// filling traps
			SQLCommands.dataBase.Open();

			lIndex = SQLCommands.GetTableLength(SQLCommands.Table.Level_Traps);

			SQLCommands.dataBase.Close();


			for (int i = 1; i <= lIndex; i++)
			{
				_traps.Add(new Dictionary<TrapKey, object>()
				{
					{ TrapKey.Type, SQLCommands.GetCell(SQLCommands.Table.Level_Traps, i, TRAPS_TYPE) as string },
					{ TrapKey.Start_Pos, new Vector2(Convert.ToInt32(SQLCommands.GetCell(SQLCommands.Table.Level_Traps, i, TRAPS_X_START_POSITION)), Convert.ToInt32(SQLCommands.GetCell(SQLCommands.Table.Level_Traps, i, TRAPS_Y_START_POSITION))) },
					{ TrapKey.Start_Rot, Convert.ToSingle(SQLCommands.GetCell(SQLCommands.Table.Level_Traps, i, TRAPS_START_ROTATION)) }
				});
			}
		}


		// Struct Parameters

		private string 
			_path,
			_era,
			_place;

		private List<string> 
			_map;

		private List<Dictionary<EnemyKey, object>>
			_enemies;

		private List<Dictionary<TrapKey, object>>
			_traps;

		private Vector2
			_player;


		// Struct Propriety

		public string Era { get { return _era; } }
		public string Place { get { return _place; } }

		public List<string> Map { get { return _map; } }

		public List<Dictionary<EnemyKey,object>> Enemies { get { return _enemies; } }
		public List<Dictionary<TrapKey,object>> Traps { get { return _traps; } }

		public Vector2 Player { get { return _player; } }


        // Methods
        public override string ToString()
        {
			string level = $"\n{ base.ToString() } {{\n";


			level += $"  Path = { _path }\n";

			level += $"  { ERA } = { Era }\n";

			level += $"  { PLACE } = { PLACE }\n";

			level += $"  Map = {{\n";

			for (int i = 0; i < Map.Count; i++) level += $"   -- { i } = { Map[i] }\n";

			level += $"  }}\n";

			level += $"  { nameof(Enemies) } = {{\n";

            for (int i = 0; i < Enemies.Count; i++)foreach (EnemyKey key in Enemies[i].Keys) level += $"   -- {key} = { Enemies[i][key] }\n";

			level += $"  }}\n";

			level += $"  { nameof(Traps) } = {{\n";

            for (int i = 0; i < Traps.Count; i++)foreach (TrapKey key in Traps[i].Keys) level += $"   -- {key} = { Traps[i][key] }\n";

			level += $"  }}\n}}";


			return level;
        }
	}
}
