
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters;
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
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
            enemyFactory,
            launcherFactory,
            bouncerFactory,
            nailsWallFactory,
            heatWallFactory;

        [Export]
        private NodePath
            groundPath;

        private TileMap
            ground;

        private const int
            OFFSET = 32;

        protected override void Init()
        {
            base.Init();
            ground = GetNode<TileMap>(groundPath);
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

        public void Retry()
        {
            ground.Clear();
            SetField();
        }

        public void SetField()
        {
            int lEnemiesIndex = 0;
            int lLauchersIndex = 0;
            int lBouncersIndex = 0;
            int lNailsWallIndex = 0;
            int lHeatWallIndex = 0;
            for (int i = 0; i < GameManager.Level.Ground.Count; i++)
            {
                for (int j = 0; j < GameManager.Level.Ground[i].Length; j++)
                {
                    switch (GameManager.Level.Ground[i][j])
                    {
                        case '#':
                            ground.SetCell(j, i, 1);
                            break;
                        case '-':
                            NailsWall lNailsWall = nailsWallFactory.Instance<NailsWall>();
                            POC.Trap_Manager.Traps.AddChild(lNailsWall);
                            lNailsWall.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            lNailsWall.GlobalRotationDegrees = GameManager.Level.Nails_Rotation[lNailsWallIndex];
                            ground.SetCell(j, i, 0);
                            lNailsWallIndex++;
                            break;
                        case '*':
                            HeatWall lHeatWall = heatWallFactory.Instance<HeatWall>();
                            POC.Trap_Manager.Traps.AddChild(lHeatWall);
                            lHeatWall.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            lHeatWall.GlobalRotationDegrees = GameManager.Level.Heat_Rotation[lHeatWallIndex];
                            lHeatWall.HeatSpeed = GameManager.Level.Heat_Time[lHeatWallIndex];
                            ground.SetCell(j, i, 0);
                            lHeatWallIndex++;
                            break;
                        case '$':
                            Bouncer lBouncer = bouncerFactory.Instance<Bouncer>();
                            POC.Trap_Manager.Traps.AddChild(lBouncer);
                            lBouncer.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            lBouncer.GlobalRotationDegrees = GameManager.Level.Bouncer_Rotation[lBouncersIndex];
                            ground.SetCell(j, i, 0);
                            lBouncersIndex++;
                            break;
                        case '_':
                            Launcher lLauncher = launcherFactory.Instance<Launcher>();
                            POC.Trap_Manager.Traps.AddChild(lLauncher);
                            lLauncher.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            lLauncher.GlobalRotationDegrees = GameManager.Level.Launchers_Rotation[lLauchersIndex];
                            lLauncher.ShotSpeed = GameManager.Level.Laucher_Shot_Speed[lLauchersIndex];
                            ground.SetCell(j, i, 0);
                            lLauchersIndex++;
                            break;
                        case '@':
                            POC.Player_Manager.Player.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            ground.SetCell(j, i, 0);
                            break;
                        case '.':
                            Villager lEnemy = POC.Enemy_Manager.SelectCharacter(GameManager.Level.Enemies_Type[lEnemiesIndex]).Instance<Villager>();
                            if (GameManager.Level.Enemies_Type[lEnemiesIndex].x < 2)
                            {
                                POC.Enemy_Manager.Enemies.AddChild(lEnemy);
                            }
                            else
                            {
                                POC.Enemy_Manager.Villagers.AddChild(lEnemy);
                            }
                            lEnemy.GlobalPosition = new Vector2(j, i) * OFFSET * 2 + Vector2.One * OFFSET;
                            lEnemy.GlobalRotationDegrees = GameManager.Level.Enemies_Rotation[lEnemiesIndex][0];
                            lEnemy.rotation = GameManager.Level.Enemies_Rotation[lEnemiesIndex];
                            foreach (Vector2 path in GameManager.Level.Enemies_Path[lEnemiesIndex])
                            {
                                lEnemy.path.Add(path * OFFSET * 2 + Vector2.One * OFFSET);
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

        public Vector2 GetCellLocation(Player pPlayer)
        {
            float Xpos = Mathf.RoundToInt((pPlayer.GlobalPosition - Vector2.One * OFFSET).x / 64) * 64;
            float Ypos = Mathf.RoundToInt((pPlayer.GlobalPosition - Vector2.One * OFFSET).y / 64) * 64;
            if (Xpos + OFFSET > pPlayer.GlobalPosition.x)
            {
                Xpos += 20;
            }
            else
            {
                Xpos += 44;
            }
            if (Ypos + OFFSET > pPlayer.GlobalPosition.y)
            {
                Ypos += 20;
            }
            else
            {
                Ypos += 44;
            }
            return new Vector2(Xpos, Ypos);
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