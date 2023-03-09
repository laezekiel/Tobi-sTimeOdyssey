using Godot;
using System;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters;

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
            enemiesPath;

        public Node2D Enemies
        {
            get { return GetNode<Node2D>(enemiesPath); }
        }

        public int Number
        {
            get { return Enemies.GetChildren().Count; }
        }

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

        public void ResetEnemies()
        {
            foreach (Enemy enemy in Enemies.GetChildren())
            {
                enemy.QueueFree();
            }
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