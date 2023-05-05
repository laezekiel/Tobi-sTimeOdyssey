using Godot;
using System;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{

	public class TrapManager : Manager
	{
        #region singleton
        static private TrapManager instance;

        static public TrapManager GetInstance()
        {
            if (instance == null) instance = new TrapManager();
            return instance;

        }

        static private TrapManager Instance { get { return GetInstance(); } }
        #endregion

        private TrapManager() : base() { }

        [Export]
        private NodePath
            trapsPath;

        public Node2D Traps
        {
            get { return GetNode<Node2D>(trapsPath); }
        }

        public override void _Ready()
        {

            base._Ready();
            #region singleton
            if (instance != null)
            {
                QueueFree();
                GD.Print(nameof(TrapManager) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion
        }

        public void ResetTraps()
        {
            foreach (Traps trap in Traps.GetChildren())
            {
                trap.QueueFree();
            }
        }

        public PackedScene SelectCharacter(int ptype)
        {
            string playerType;
            int playerScene = 0;
            int tableSize;


            SQLCommands.dataBase.Open();

            tableSize = SQLCommands.GetTableLength(SQLCommands.Table.Scenes_Traps);

            SQLCommands.dataBase.Close();


            for (int i = 1; i <= tableSize; i++)
            {
                playerScene = i;
                playerType = (string)SQLCommands.GetCell(SQLCommands.Table.Scenes_Charcters, i, "CType");

                if (playerType == (string)GameManager.Current_Level.Enemies[ptype][Level.EnemyKey.Type]) break;
            }

            return GD.Load<PackedScene>((string)SQLCommands.GetCell(SQLCommands.Table.Scenes_Charcters, playerScene, "GameScene"));
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