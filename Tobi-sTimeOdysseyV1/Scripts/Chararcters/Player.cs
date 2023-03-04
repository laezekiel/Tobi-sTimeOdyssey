using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Characters
{

    public class Player : Character
    {
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
        }

        #region State Machine
        // Mode 
        #region game State Mode
        #endregion
        #region character State mode
        #endregion
        // Action 
        #region Game State Action
        protected override void DoGameModePlay()
        {
            base.DoGameModePlay();
        }
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
            MoveAndCollide(new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized() * 25);
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
            base.Dispose(pDisposing);
        }
    }
}