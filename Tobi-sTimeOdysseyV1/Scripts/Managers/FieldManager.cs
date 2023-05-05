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
            groundPath,
            environmentPath;

        private TileMap Ground { get { return GetNode<TileMap>(groundPath); } } 

        private CanvasModulate Environment { get { return GetNode<CanvasModulate>(environmentPath); } }


        private const int
            OFFSET = 32;


        private int
            enemiesIndex = 0,
            lTrapsIndex = 0,
            xPos = 0,
            yPos = 0;


        protected override void Init()
        {
            base.Init();
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

        public override void _Process(float delta)
        {
            base._Process(delta);
            SetField();
        }
        public void Retry()
        {
            Ground.Clear();
        }


        private void SetField()
        {
            if (xPos == 0 && yPos == 0)
            {
                POC.Player_Manager.Player.GlobalPosition = GameManager.Current_Level.Player * OFFSET * 2 + Vector2.One * OFFSET;

                SQLCommands.dataBase.Open();

                int lNbrEnemies = SQLCommands.GetTableLength(SQLCommands.Table.Level_Enemies);
                int lNbrTraps = SQLCommands.GetTableLength(SQLCommands.Table.Level_Traps);

                SQLCommands.dataBase.Close();

                for (int i = 1; i <= lNbrEnemies ; i++)
                {
                    PackedScene charScene = POC.Enemy_Manager.SelectCharacter(i);

                    Villager lEnemy = charScene.Instance<Villager>();

                    string lEType = (string)GameManager.Current_Level.Enemies[enemiesIndex][Level.EnemyKey.Type];

                    if (lEType == "Villager") POC.Enemy_Manager.Villagers.AddChild(lEnemy);
                    else POC.Enemy_Manager.Enemies.AddChild(lEnemy);


                    lEnemy.GlobalPosition = (Vector2)(GameManager.Current_Level.Enemies[enemiesIndex][Level.EnemyKey.Start_Pos]) * OFFSET * 2 + Vector2.One * OFFSET;
                    lEnemy.GlobalRotationDegrees = (float)GameManager.Current_Level.Enemies[enemiesIndex][Level.EnemyKey.Start_Rot];

                    Ground.SetCell(xPos, yPos, 0);
                }

                for (int i = 1; i <= lNbrTraps; i++)
                {
                    Traps lTrap = nailsWallFactory.Instance<Traps>();
                    POC.Trap_Manager.Traps.AddChild(lTrap);
                    lTrap.GlobalPosition = new Vector2(xPos, yPos) * OFFSET * 2 + Vector2.One * OFFSET;
                    lTrap.GlobalRotationDegrees = Convert.ToSingle(GameManager.Current_Level.Traps[lTrapsIndex][Level.TrapKey.Start_Rot]);
                    Ground.SetCell(xPos, yPos, 0);
                    lTrapsIndex++;

                }
            }


            if (GameManager.Current_Level.Map.Count == yPos) 
            {
                Environment.Color = Colors.Black;
                return; 
            }
            else Environment.Color = new Color(Colors.Black, 0.5f);


            switch (GameManager.Current_Level.Map[yPos][xPos])
            {
                case '#':
                    Ground.SetCell(xPos, yPos, 1);
                    break;
                case '-':
                    NailsWall lNailsWall = nailsWallFactory.Instance<NailsWall>();
                    POC.Trap_Manager.Traps.AddChild(lNailsWall);
                    lNailsWall.GlobalPosition = new Vector2(xPos, yPos) * OFFSET * 2 + Vector2.One * OFFSET;
                    lNailsWall.GlobalRotationDegrees = Convert.ToSingle(GameManager.Current_Level.Traps[lTrapsIndex][Level.TrapKey.Start_Rot]);
                    Ground.SetCell(xPos, yPos, 0);
                    lTrapsIndex++;
                    break;
                case '*':
                    HeatWall lHeatWall = heatWallFactory.Instance<HeatWall>();
                    POC.Trap_Manager.Traps.AddChild(lHeatWall);
                    lHeatWall.GlobalPosition = new Vector2(xPos, yPos) * OFFSET * 2 + Vector2.One * OFFSET;
                    lHeatWall.GlobalRotationDegrees = Convert.ToSingle(GameManager.Current_Level.Traps[lTrapsIndex][Level.TrapKey.Start_Rot]);
                    Ground.SetCell(xPos, yPos, 0);
                    lTrapsIndex++;
                    break;
                case '$':
                    Bouncer lBouncer = bouncerFactory.Instance<Bouncer>();
                    POC.Trap_Manager.Traps.AddChild(lBouncer);
                    lBouncer.GlobalPosition = new Vector2(xPos, yPos) * OFFSET * 2 + Vector2.One * OFFSET;
                    lBouncer.GlobalRotationDegrees = Convert.ToSingle(GameManager.Current_Level.Traps[lTrapsIndex][Level.TrapKey.Start_Rot]);
                    Ground.SetCell(xPos, yPos, 0);
                    lTrapsIndex++;
                    break;
                case '_':
                    Launcher lLauncher = launcherFactory.Instance<Launcher>();
                    POC.Trap_Manager.Traps.AddChild(lLauncher);
                    lLauncher.GlobalPosition = new Vector2(xPos, yPos) * OFFSET * 2 + Vector2.One * OFFSET;
                    lLauncher.GlobalRotationDegrees = Convert.ToSingle(GameManager.Current_Level.Traps[lTrapsIndex][Level.TrapKey.Start_Rot]);
                    Ground.SetCell(xPos, yPos, 0);
                    lTrapsIndex++;
                    break;
                case ' ':
                    Ground.SetCell(xPos, yPos, 0);
                    break;
                default:
                    break;
            }

            if (GameManager.Current_Level.Map.Count >= yPos + 1)
            {
                if (GameManager.Current_Level.Map[yPos].Length > xPos + 1)
                {
                    xPos++;
                }
                else
                {
                    yPos++;
                    xPos = 0;
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