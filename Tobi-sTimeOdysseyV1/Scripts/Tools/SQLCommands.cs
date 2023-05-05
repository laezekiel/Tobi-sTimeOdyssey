using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{
	public static class SQLCommands
	{
        public enum Table
        {
            Player_Generic_Data,
            Player_Mission_Data,
            Player_Level_Data,
            Level_Data,
            Level_Map,
            Level_Enemies,
            Level_Traps,
            Level_Unlock,
            Scenes_Charcters,
            Scenes_Traps,
        }

        public static SQLiteConnection dataBase = new SQLiteConnection($"Data Source={AllPath.Data_Base};Version=3;");

        public static void CreateTable(Table tableName)
        {
            dataBase.Open();

            if (TableExist(tableName)) { dataBase.Close(); return; }
            switch (tableName)
            {
                case Table.Player_Generic_Data:
                    ExecuteExternatNonQuery(AllPath.Player_Data[1]);

                    ExecuteExternatNonQuery(AllPath.Player_Data[4]);
                    break;

                case Table.Player_Mission_Data:
                    ExecuteExternatNonQuery(AllPath.Player_Data[2]);

                    ExecuteExternatNonQuery(AllPath.Player_Data[5]);
                    break;

                case Table.Player_Level_Data:
                    ExecuteExternatNonQuery(AllPath.Player_Data[3]);

                    ExecuteExternatNonQuery(AllPath.Player_Data[6]);
                    break;

                case Table.Level_Data:
                    ExecuteExternatNonQuery(AllPath.All_Level[1]);
                    break;

                case Table.Level_Map:
                    ExecuteExternatNonQuery(AllPath.All_Level[2]);
                    break;

                case Table.Level_Enemies:
                    ExecuteExternatNonQuery(AllPath.All_Level[3]);
                    break;

                case Table.Level_Traps:
                    ExecuteExternatNonQuery(AllPath.All_Level[4]);
                    break;

                case Table.Level_Unlock:
                    ExecuteExternatNonQuery(AllPath.All_Level[5]);
                    break;

                case Table.Scenes_Charcters:
                    ExecuteExternatNonQuery(AllPath.Game_Data[1]);

                    ExecuteExternatNonQuery(AllPath.Game_Data[2]);
                    break;
                case Table.Scenes_Traps:
                    //ExecuteExternatNonQuery(AllPath.Game_Data[3]);

                    //ExecuteExternatNonQuery(AllPath.Game_Data[4]);
                    break;
            }
            dataBase.Close();
        }

        public static void ExecuteExternatNonQuery(string pPath)
        {
            string query2 = System.IO.File.ReadAllText(pPath);

            using (SQLiteCommand createLine = new SQLiteCommand(query2, dataBase))
            {
                createLine.ExecuteNonQuery();
            }

        }

        public static int GetTableLength(Table tableName)
        {

            string query = $"SELECT COUNT(*) FROM {tableName}";

            using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
            {
                return Convert.ToInt32(command.ExecuteScalar());

            }
        }

        public static void PrintAllRowString(Table tableName)
        {
            dataBase.Open();

            int numberOfRow = GetTableLength(tableName);

            for (int i = 1; i <= numberOfRow; i++) GD.Print(GetRowString(tableName, i));

            dataBase.Close();
        } 

        private static string GetRowString(Table tableName, int index) 
        {
            string row = $"{tableName}: ";

            string query = $"SELECT * FROM {tableName} WHERE ID = @ValueToSearch";

            using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
            {
                command.Parameters.AddWithValue("@ValueToSearch", index);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows) return row = "No rows found.";

                    DataTable schemaTable = reader.GetSchemaTable();

                    while (reader.Read())
                    {
                        for (int i = 1; i < schemaTable.Rows.Count; i++)
                        {
                            string columnName = schemaTable.Rows[i]["ColumnName"].ToString();
                            object columnValue = reader[columnName];

                            row += $"{columnName}: {columnValue}";

                            if (i < schemaTable.Rows.Count - 1) row += " // ";
                        }
                    }
                }
            }

            return row;
        }


        public static object GetCell(Table tableName, int index, string pCollumn)
        {
            string query = $"SELECT { pCollumn } FROM {tableName} WHERE ID = @ValueToSearch";
            object something = null;


            dataBase.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
            {
                command.Parameters.AddWithValue("@ValueToSearch", index);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) while (reader.Read()) something = reader[pCollumn].ToString();
                }
            }

            dataBase.Close();


            return something;
        }

        public static List<object> GetAllCell(Table tableName, int index)
        {
            List<string> collumns = GetAllCollumn(tableName, index);

            List<object> objects = new List<object>();

            foreach (string collumn in collumns) objects.Add(GetCell(tableName, index, collumn));

            return objects;
        }

        private static bool TableExist(Table tableName)
        {
            using (SQLiteCommand command = new SQLiteCommand(dataBase))
            {
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name=@TableName";
                command.Parameters.AddWithValue("@TableName", tableName.ToString());

                object result = command.ExecuteScalar();

                if (result != null) return true;
                else return false;
            }

        }

        public static List<string> GetAllTableNames()
        {
            List<string> tableNames = new List<string>();

            string query = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";

            dataBase.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) tableNames.Add(reader.GetString(0));
                }
            }

            dataBase.Close();

            return tableNames;
        }

        public static List<string> GetAllCollumn(Table tableName, int index = 1)
        {
            List<string> collumns = new List<string>();

            string query = $"SELECT * FROM { tableName } WHERE ID = @ValueToSearch";

            dataBase.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
            {
                command.Parameters.AddWithValue("@ValueToSearch", index);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    DataTable schemaTable = reader.GetSchemaTable();

                    while (reader.Read())
                    {
                        for (int i = 1; i < schemaTable.Rows.Count; i++)
                        {
                            collumns.Add(schemaTable.Rows[i]["ColumnName"].ToString());
                        }
                    }
                }
            }

            dataBase.Close();

            return collumns;
        }

        private static void DropTable(Table tableName)
        {
            string query = $"DROP TABLE {tableName}";

            if (TableExist(tableName))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ResetDataBase()
        {
            List<string> TableNames = GetAllTableNames();

            dataBase.Open();

            foreach (string table in TableNames)
            {
                Table myEnum;

                if (Enum.TryParse(table, out myEnum) && Enum.IsDefined(typeof(Table), myEnum)) DropTable(myEnum);
            }

            dataBase.Close();
        }

        public static void DeleteTable(Table tableName)
        {
            string query = $"DELETE FROM { tableName }";

            dataBase.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
            {
                command.ExecuteNonQuery();
            }

            dataBase.Close();
        }

        public static void DeleteTable(Table tableName, string pCondition)
        {
            string query = $"DELETE FROM { tableName } WHERE { pCondition }";

            dataBase.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, dataBase))
            {
                command.ExecuteNonQuery();
            }

            dataBase.Close();
        }

        public static void ResetLevelTables()
        {
            DeleteTable(Table.Level_Data);
            DeleteTable(Table.Level_Map);
            DeleteTable(Table.Level_Enemies);
            DeleteTable(Table.Level_Traps);
            DeleteTable(Table.Level_Unlock);
        }
    }
}