using Godot;
using System;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{

	public class EnemyManager : Manager
    {
        #region singleton
        static private EnemyManager instance;

        static public EnemyManager GetInstance()
        {
            if (instance == null) instance = new EnemyManager();
            return instance;

        }

        static private EnemyManager Instance { get { return GetInstance(); } }
        #endregion

        private EnemyManager() : base() { }

        [Export]
        private NodePath
            enemiesPath,
            villagerspath;

        public Node2D Enemies { get { return GetNode<Node2D>(enemiesPath); } }
        public Node2D Villagers { get { return GetNode<Node2D>(villagerspath); } }

        public int Number { get { return Enemies.GetChildren().Count; } }

        public override void _Ready()
		{

            base._Ready();
            #region singleton
            if (instance != null)
            {
                QueueFree();
                GD.Print(nameof(EnemyManager) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion
        }

        public void ResetCharacter()
        {
            foreach (Enemy enemy in Enemies.GetChildren())
            {
                enemy.QueueFree();
            }
            foreach (Villager villager in Villagers.GetChildren())
            {
                villager.QueueFree();
            }
        }

        public PackedScene SelectCharacter(int ptype)
        {
            string playerType;
            int playerScene = 0;
            int tableSize;


            SQLCommands.dataBase.Open();

            tableSize = SQLCommands.GetTableLength(SQLCommands.Table.Scenes_Charcters);

            SQLCommands.dataBase.Close();


            for (int i = 1; i <= tableSize; i++)
            {
                playerScene = i;
                playerType = (string)SQLCommands.GetCell(SQLCommands.Table.Scenes_Charcters, i, "CType");

                if (playerType == (string)GameManager.Current_Level.Enemies[ptype][Level.EnemyKey.Type]) break;
            }

            return GD.Load<PackedScene>((string)SQLCommands.GetCell(SQLCommands.Table.Scenes_Charcters, playerScene, "GameScene"));
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
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