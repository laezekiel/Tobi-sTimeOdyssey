using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements
{

	public class TobiView : Camera2D
    {
        #region singleton
        static private TobiView instance;

        static public TobiView GetInstance()
        {
            if (instance == null) instance = new TobiView();
            return instance;

        }

        static private TobiView Instance { get { return GetInstance(); } }
        #endregion

        private TobiView() : base() { }

        [Export]
        private NodePath
            directionCursorPath,
            centerDirectionPath;

        private Position2D
            centerDirection;

        private Sprite
            directionCursor;

        public void Init()
        {
            centerDirection = GetNode<Position2D>(centerDirectionPath);
            directionCursor = GetNode<Sprite>(directionCursorPath);
        }

		public override void _Ready()
		{
            base._Ready();
            #region singleton
            if (instance != null)
            {
                QueueFree();
                GD.Print(nameof(TobiView) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion
            Init();
		}

        public override void _Process(float delta)
        {
            base._Process(delta);
            GlobalPosition = POC.Player_Manager.Player.GlobalPosition;
            if (Input.IsActionPressed("Look_Player"))
            {
                directionCursor.GlobalPosition = GetGlobalMousePosition();
            }
            else directionCursor.GlobalPosition = centerDirection.GlobalPosition;
        }

        public float RotationPlayer()
        {
            Vector2 lPolar = Mathf.Cartesian2Polar(centerDirection.GlobalPosition.DirectionTo(directionCursor.GlobalPosition).x, centerDirection.GlobalPosition.DirectionTo(directionCursor.GlobalPosition).y);
            return lPolar.y;
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