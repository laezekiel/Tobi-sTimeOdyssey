using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps;
using Godot;
using System;
using System.Collections.Generic;
using Com.IronicEntertainment.TobisTimeOdyssey.Managers;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters
{

    public class Player : Character
    {
        [Export]
        private NodePath
            killerContainerPath,
            bodyPath;

        private List<RayCast2D>
            killerContainer = new List<RayCast2D>();

        private Action
            playerState;

        private Vector2 
            direction = new Vector2();

        private AnimatedSprite
            body;

        private bool
            dying = false;

        public override void Init()
        {
            foreach (RayCast2D killer in GetNode<Node2D>(killerContainerPath).GetChildren())
            {
                killerContainer.Add(killer);
                killer.AddException(this);
            }
            body = GetNode<AnimatedSprite>(bodyPath);
            SetPlayerModeStatic();
            base.Init();
        }

        public override void _Ready()
        {
            base._Ready();
        }

        #region State Machine
        // Mode 
        #region game State Mode
        public override void SetGameModeWin()
        {
            body.Animation = "happy";
            base.SetGameModeWin();
        }
        #endregion
        #region character State mode
        public void SetPlayerModeMove()
        {
            direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized() * 25;
            playerState = DoPlayerModeMove;
        }
        public void SetPlayerModeStatic()
        {
            direction = Vector2.Zero;
            body.Animation = "living";
            playerState = DoPlayerModeStatic;
        }
        public void SetPlayerModeBurning()
        {
            body.Animation = "burnt";
            playerState = DoPlayerModeBurning;
        }
        public void SetPlayerModeDying()
        {
            body.Animation = "dead";
            playerState = DoPlayerModeDying;
        }
        public void SetPlayerModeCaught()
        {
            body.Animation = "caught";
            playerState = DoPlayerModeCaught;
        }
        #endregion
        // Action 
        #region Game State Action
        protected override void DoGameModePlay()
        {
            base.DoGameModePlay();
            playerState();
        }

        protected override void DoGameModeLose()
        {
            base.DoGameModeLose();
        }
        #endregion
        #region character State action
        private void DoPlayerModeStatic()
        {
            foreach (RayCast2D check in checkCollider)
            {
                if (check.IsColliding() && !(check.GetCollider() is HeatWall))
                {
                    GlobalPosition = POC.Field_Manager.GetCellLocation(this);
                    break;
                }
                else
                {
                    LookAt(GetGlobalMousePosition());
                    if (Input.IsActionJustPressed("Move_Player"))
                    {
                        SetPlayerModeMove();
                    }
                }
            }
        }
        private void DoPlayerModeMove()
        {
            foreach (RayCast2D killer in killerContainer)
            {
                if (killer.GetCollider() is Enemy)
                {
                    Enemy target = killer.GetCollider() as Enemy;
                    target.LoseLife();
                }
                else if (killer.GetCollider() is Villager && !(killer.GetCollider() is Enemy))
                {
                    MOC.Lose(MOC.LoseType.Caught);
                }
            }
            if (MoveAndCollide(direction) != null && MoveAndCollide(direction).Collider is Bouncer)
            {
                Bouncer lBouncer = MoveAndCollide(direction).Collider as Bouncer;
                lBouncer.Rebound(this);
                Vector2 normal = MoveAndCollide(direction).Normal;
                direction = direction.Bounce(normal);
            }
            MoveAndCollide(direction);
            foreach (RayCast2D check in checkCollider)
            {
                if (check.IsColliding())
                {
                    if (!(check.GetCollider() is Bouncer) && !(check.GetCollider() is NailsWall))
                    {
                        SetPlayerModeStatic();
                        break;
                    }
                    else if (check.GetCollider() is NailsWall)
                    {
                        MOC.Lose(MOC.LoseType.Killed);
                    }

                }
            }
        }
        private void DoPlayerModeBurning()
        {
            if (dying)
            {
                dying = false;
                Tween lDeath = new Tween();
                AddChild(lDeath);
                lDeath.Repeat = true;
                lDeath.InterpolateProperty(body, "modulate", POC.Visible, Colors.Red, POC.OneS);
                float lDelay = lDeath.GetRuntime();
                lDeath.InterpolateProperty(body, "modulate", Colors.Red, POC.Visible, POC.OneS, delay: lDelay);
                lDeath.Start();
            }
        }
        public void DoPlayerModeDying()
        {
            if (dying)
            {
                dying = false;
                Tween lDeath = new Tween();
                AddChild(lDeath);
                lDeath.Repeat = true;

                lDeath.Start();
            }
        }
        public void DoPlayerModeCaught()
        {
            if (dying)
            {
                dying = false;
                Tween lDeath = new Tween();
                AddChild(lDeath);
                lDeath.Repeat = true;

                lDeath.Start();
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