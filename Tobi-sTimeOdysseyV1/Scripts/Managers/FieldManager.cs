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
            wallFactory,
            enemyfactory;

        [Export]
        private NodePath
            groundPath,
            enemiesPath,
            wallsPath;

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
            walls = GetNode<Node2D>(wallsPath);
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
            PlayerManager.GetInstance().Player.GlobalPosition = (GameManager.Level.Player * OFFSET * 2) + Vector2.One * OFFSET;
            foreach (List<int> enemy in GameManager.Level.Enemies)
            {
                Enemy lEnemy = enemyfactory.Instance<Enemy>();
                enemies.AddChild(lEnemy);
                lEnemy.GlobalPosition = (new Vector2(enemy[1], enemy[0]) * OFFSET * 2) + Vector2.One * OFFSET;
                lEnemy.RotationDegrees = enemy[2];
            }
            for (int i = 0; i < GameManager.Level.Ground.Count; i++)
            {
                for (int j = 0; j < GameManager.Level.Ground[i].Length; j++)
                {
                    switch (GameManager.Level.Ground[i][j])
                    {
                        case '#':
                            Wall lWall = wallFactory.Instance<Wall>();
                            walls.AddChild(lWall);
                            lWall.GlobalPosition = (new Vector2(j, i) * OFFSET * 2) + Vector2.One * OFFSET;
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