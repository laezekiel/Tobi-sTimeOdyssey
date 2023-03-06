using Com.IronicEntertainment.TobisTimeOdyssey.Characters;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Walls;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{

    public class FieldManager : Manager
    {
        #region singleton
        static private FieldManager instance;
		
		static public FieldManager GetInstance() 
        {
            if (instance == null) instance = new FieldManager ();
            return instance;

        }

        static private FieldManager Instance { get {return GetInstance(); } }
        #endregion

        private FieldManager (): base() {}

        [Export]
        private PackedScene
            enemyfactory;

        [Export]
        private NodePath
            groundPath,
            enemiesPath;

        private Node2D
            enemies,
            walls;

        private TileMap
            ground;

        private const int
            OFFSET = 32;

        protected override void Init()
        {
            base.Init();
            ground = GetNode<TileMap>(groundPath);
            enemies = GetNode<Node2D>(enemiesPath);
            SetField();
        }

        public override void _Ready()
        {
            base._Ready();
            #region singleton
            if (instance != null){  
                QueueFree();
                GD.Print(nameof(FieldManager) + " Instance already exist, destroying the last added.");
                return;
            }
            
            instance = this;
            #endregion
        }

        #region State Machine
        // Mode
        // Action
        #endregion

        private void SetField()
        {
            int lEnemiesIndex = 0;
            for (int i = 0; i < GameManager.Level.Ground.Count; i++)
            {
                for (int j = 0; j < GameManager.Level.Ground[i].Length; j++)
                {
                    switch (GameManager.Level.Ground[i][j])
                    {
                        case '#':
                            ground.SetCell(j, i, 1);
                            break;
                        case '@':
                            PlayerManager.GetInstance().Player.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            ground.SetCell(j, i, 0);
                            break;
                        case '.':
                            Enemy lEnemy = enemyfactory.Instance<Enemy>();
                            enemies.AddChild(lEnemy);
                            lEnemy.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            lEnemy.GlobalRotationDegrees = GameManager.Level.Enemies_Rotation[lEnemiesIndex][0];
                            lEnemy.rotation = GameManager.Level.Enemies_Rotation[lEnemiesIndex];
                            lEnemy.path = GameManager.Level.Enemies_Path[lEnemiesIndex];
                            for (int k = 0; k < lEnemy.path.Count; k++)
                            {
                                lEnemy.path[k] = lEnemy.path[k] * OFFSET * 2 + Vector2.One * OFFSET;
                            }
                            ground.SetCell(j, i, 0);
                            lEnemiesIndex++;
                            break;
                        case ' ':
                            ground.SetCell(j, i, 0);
                            break;
                        default:
                            break;
                    }
                }
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