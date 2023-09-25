using System;
using System.Data;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using Microsoft.Data.Sqlite;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Data
{
    static public class Database
    {
        private const string DatabaseFileName = "Scripts/Data/tobi_player_data.db";

        private const string ProgressionTable = "Progression";

        private const string AllTutorialTable = "All_Tutorial";
        private const string AllMissionTable = "All_Mission";
        private const string AllBonusTable = "All_Bonus";



        public static void CreateDatabaseAndTables()
        {
            using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
            {
                connection.Open();


                string createTableSql = $"CREATE TABLE IF NOT EXISTS {ProgressionTable} (Id INTEGER PRIMARY KEY, Name TEXT, Box INTEGER, Number INTEGER)";
                using (var command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }


                createTableSql = $"CREATE TABLE IF NOT EXISTS {AllTutorialTable} (Id INTEGER PRIMARY KEY, Box INTEGER, Number INTEGER, IsUnlocked INTEGER, IsCompleted INTEGER)";
                using (var command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }


                createTableSql = $"CREATE TABLE IF NOT EXISTS {AllMissionTable} (Id INTEGER PRIMARY KEY, Box INTEGER, Number INTEGER, IsUnlocked INTEGER, IsCompleted INTEGER)";
                using (var command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }


                createTableSql = $"CREATE TABLE IF NOT EXISTS {AllBonusTable} (Id INTEGER PRIMARY KEY, Box INTEGER, Number INTEGER, IsUnlocked INTEGER, IsCompleted INTEGER)";
                using (var command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }


                string countRowsSql = $"SELECT COUNT(*) FROM {ProgressionTable}";
                using (var command = new SqliteCommand(countRowsSql, connection))
                {
                    int rowCount = Convert.ToInt32(command.ExecuteScalar());

                    if (rowCount == 0)
                    {
                        // The table is empty, perform the insertion
                        string insertSql = $"INSERT INTO {ProgressionTable} (Id, Name, Box, Number) VALUES (@id, @name, @box, @number)";
                        using (var insertCommand = new SqliteCommand(insertSql, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@id", 1);
                            insertCommand.Parameters.AddWithValue("@name", State.MissionType.Tutorial.ToString());
                            insertCommand.Parameters.AddWithValue("@box", 1);
                            insertCommand.Parameters.AddWithValue("@number", 1);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }

                connection.Close();
            }
        }

        public static void CreateLevelData(int pGroup, int pNumber, int ptype = 0)
        {
            string table = "";

            switch (ptype)
            {
                case 0:
                    table = AllTutorialTable;
                    break;
                case 1:
                    table = AllMissionTable;
                    break;
                case 2:
                    table = AllBonusTable;
                    break;
                default:
                    break;
            }


            using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
            {
                connection.Open();

                string countRowSql = $"SELECT COUNT(*) FROM {table}";
                using(var command = new SqliteCommand(countRowSql, connection))
                {
                    int rowCount = Convert.ToInt32(command.ExecuteScalar()) ;

                    string insertSql = $"INSERT INTO {table} (Id, Box, Number, IsUnlocked, IsCompleted) VALUES (@id, @box, @number, @isUnlocked, @isCompleted)";
                    using (var insertCommand = new SqliteCommand( insertSql, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@id", rowCount + 1);
                        insertCommand.Parameters.AddWithValue("@box", pGroup);
                        insertCommand.Parameters.AddWithValue("@number", pNumber);
                        insertCommand.Parameters.AddWithValue("@isUnlocked", 0);
                        insertCommand.Parameters.AddWithValue("@isCompleted", 0);
                        insertCommand.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }



        public static State.LevelIndex GetProgressionInfo()
        {
            State.MissionType resultName = State.MissionType.Tutorial;
            int resultGroup = 1;
            int resultNumber = 1;

            using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
            {
                connection.Open();

                string selectSql = $"SELECT * FROM {ProgressionTable} WHERE Id = @id";
                using (var command = new SqliteCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@id", 1);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultName = (State.MissionType)Enum.Parse(typeof(State.MissionType), reader.GetString(1));
                            resultGroup = reader.GetInt32(2);
                            resultNumber = reader.GetInt32(3);

                        }
                    }
                }

                connection.Close();
            }

            return new State.LevelIndex(resultName,resultGroup,resultNumber);
        }

        public static int GetTableRowCount(int ptype = 0)
        {
            string table = "";

            int rowCountToReturn = 0;

            switch (ptype)
            {
                case 0:
                    table = AllTutorialTable;
                    break;
                case 1:
                    table = AllMissionTable;
                    break;
                case 2:
                    table = AllBonusTable;
                    break;
                default:
                    break;
            }


            using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
            {
                connection.Open();

                string countRowSql = $"SELECT COUNT(*) FROM {table}";
                using (var command = new SqliteCommand(countRowSql, connection))
                {
                    rowCountToReturn = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
            }


            return rowCountToReturn;
        }



        public static void UpdateProgressionData(State.LevelIndex pLevel)
        {
            using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
            {
                connection.Open();

                string updateSql = $"UPDATE {ProgressionTable} SET Name = @name, Box = @box, Number = @number WHERE Id = @id";
                using (var command = new SqliteCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@name", pLevel.Type.ToString());
                    command.Parameters.AddWithValue("@box", pLevel.Group);
                    command.Parameters.AddWithValue("@number", pLevel.Level);
                    command.Parameters.AddWithValue("@id", 1);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }



        public static void PrintAllTablesData()
        {
            using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
            {
                connection.Open();

                // Query the sqlite_master table to get a list of table names
                string getTableNamesSql = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%'";
                using (var command = new SqliteCommand(getTableNamesSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader.GetString(0);
                            GD.Print($"Table: {tableName}");

                            // Query each table to retrieve its contents
                            string selectTableSql = $"SELECT * FROM {tableName}";
                            using (var selectCommand = new SqliteCommand(selectTableSql, connection))
                            {
                                using (var tableReader = selectCommand.ExecuteReader())
                                {
                                    PrintTableData(tableReader);
                                }
                            }

                            GD.Print();
                        }
                    }
                }

                connection.Close();
            }
        }

        private static void PrintTableData(IDataReader reader)
        {
            int fieldCount = reader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                GD.Print(reader.GetName(i) + "\t");
            }
            GD.Print();

            while (reader.Read())
            {
                for (int i = 0; i < fieldCount; i++)
                {
                    GD.Print(reader.GetValue(i) + "\t");
                }
                GD.Print();
            }
        }



        public static void ResetProgression()
        {
            using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
            {
                connection.Open();

                string dropTableSql = $"DROP TABLE IF EXISTS {ProgressionTable}";
                using (var dropCommand = new SqliteCommand(dropTableSql, connection))
                {
                    dropCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
