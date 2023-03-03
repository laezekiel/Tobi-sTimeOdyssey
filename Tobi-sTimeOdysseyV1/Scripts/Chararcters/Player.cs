using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Characters
{

    public class Player : Character
    {
        #region singleton
        static private Player instance;
		
		static public Player GetInstance() 
        {
            if (instance == null) instance = new Player ();
            return instance;

        }

        static private Player Instance { get {return GetInstance(); } }
        #endregion

        private Player (): base() {}

        [Export]
        private NodePath
            killerContainerPath;

        private List<RayCast2D>
            killerContainer = new List<RayCast2D>();

        protected override void Init()
        {
            foreach (RayCast2D killer in GetNode<Node2D>(killerContainerPath).GetChildren())
            {
                killerContainer.Add(killer);
                killer.AddException(this);
            }
            base.Init();
            SetCharacterModeStatic();
        }

        public override void _Ready()
        {
            base._Ready();
            #region singleton
            if (instance != null){  
                QueueFree();
                GD.Print(nameof(Player) + " Instance already exist, destroying the last added.");
                return;
            }
            
            instance = this;
            #endregion
        }

        #region State Machine
        // Mode 
        #region game State Mode
        #endregion
        #region character State mode
        #endregion
        // Action 
        #region Game State Action
        protected override void DoGameModeLose()
        {
            Modulate = POC.Invisible;
        }
        #endregion
        #region character State action
        protected override void DoCharacterModeStatic()
        {
            LookAt(GetGlobalMousePosition());
            if (Input.IsActionJustPressed("Move_Player"))
            {
                SetCharacterModeMove();
            }
        }
        protected override void DoCharacterModeMove()
        {
            foreach (RayCast2D killer in killerContainer)
            {
                if (killer.GetCollider() is Enemy)
                {
                    Enemy target = killer.GetCollider() as Enemy;
                    target.QueueFree();
                }
            }
            MoveAndCollide(new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized());
            foreach (RayCast2D check in checkCollider)
            {
                if (check.IsColliding())
                {
                    SetCharacterModeStatic();
                    break;
                }
            }
        }
        #endregion
        #endregion

        protected override void Dispose(bool pDisposing)
        {
            #region singleton
            if (pDisposing && instance == this) instance = null;
            #endregion
            base.Dispose(pDisposing);
        }
    }
}