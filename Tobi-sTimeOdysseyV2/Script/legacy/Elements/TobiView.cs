using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.UI;
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
            centerDirectionPath,
            directionPath,
            jumpPath;

        private static PackedScene
            cutscenesFactory = GD.Load<PackedScene>("res://Scenes/UI/Cutscenes.tscn"),
            winFactory = GD.Load<PackedScene>("res://Scenes/UI/WinScreen.tscn");

        private Position2D
            centerDirection;

        private Sprite
            directionCursor;

        private TouchScreenButton
            direction,
            jump;

        public void Init()
        {
            centerDirection = GetNode<Position2D>(centerDirectionPath);

            directionCursor = GetNode<Sprite>(directionCursorPath);

            direction = GetNode<TouchScreenButton>(directionPath);

            jump = GetNode<TouchScreenButton>(jumpPath);
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

        public void HideButton()
        {
            direction.Visible = false;
            jump.Visible = false;
            directionCursor.Visible = false;
        }

        public void ShowButton()
        {
            direction.Visible = true;
            jump.Visible = true;
            directionCursor.Visible = true;
        }

        public void PlayCutscenes()
        {
            HideButton();

            Cutscenes lCut = cutscenesFactory.Instance<Cutscenes>();

            AddChild(lCut);
        }

        public void AddWinScreen()
        {

            WinScreen lWin = winFactory.Instance<WinScreen>();

            AddChild(lWin);

            HideButton();
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