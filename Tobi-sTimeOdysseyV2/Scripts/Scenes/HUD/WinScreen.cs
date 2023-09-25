using Com.BeerAndDev.TobisTimeOdyssey.Data;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using static Com.BeerAndDev.TobisTimeOdyssey.Scenes.LevelGenerator;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Scenes.HUD
{
	public partial class WinScreen : Node2D
    {
        [Export]
        private string
            UnlockPath;

        [Export]
        private NodePath
            retryPath,
            nextPath;

        private PackedScene
            loadingScreenFactory = GD.Load<PackedScene>("res://GameScreensAndScenes/LoadingScreen.tscn");



        private TouchScreenButton
            retry,
            next;



        public void Init()
        {
            retry = GetNode<TouchScreenButton>(retryPath);
            next = GetNode<TouchScreenButton>(nextPath);

            Hide();
        }



        public override void _Ready()
        {
            base._Ready();
            Init();
        }



        public override void _Process(double delta)
        {
            base._Process(delta);

            switch (State.Current_State)
            {
                case State.GameState.Loading:
                case State.GameState.Player_Aiming:
                case State.GameState.Player_Dashing:
                case State.GameState.Player_Caught:
                case State.GameState.Cinematics:
                case State.GameState.Pause:
                    Visible = false;
                    break;
                case State.GameState.Player_Win:
                    Visible = true;

                    if (Input.IsActionJustPressed("Retry"))
                    {
                        LoadingScreen newLoad = loadingScreenFactory.Instantiate<LoadingScreen>();

                        GetTree().Root.AddChild(newLoad);

                        GetTree().Root.GetChild(0).Free();
                        Player.Instance.QueueFree();
                        ViewPlayer.Instance.QueueFree();
                    }

                    if (Input.IsActionJustPressed("Next"))
                    {
                        State.LevelIndex present = State.Level_Index;


                        if (present.Level + 1 <= State.allFilePath[present.Type][present.Group - 1].Count)
                        {
                            Database.UpdateProgressionData(new State.LevelIndex(present.Type, present.Group, present.Level + 1));
                        }
                        else
                        {
                            Dictionary jsonTemp = State.ReadJSON(File.ReadAllText( UnlockPath)).Obj as Dictionary;
                            Godot.Collections.Array unlockableType = jsonTemp[present.Type.ToString()].Obj as Godot.Collections.Array;
                            Godot.Collections.Array unlockableGroup = unlockableType[present.Group-1].Obj as Godot.Collections.Array;

                            if (unlockableGroup.Count > 0)
                            {
                                Godot.Collections.Array unlockable0 = unlockableGroup[0].Obj as Godot.Collections.Array;

                                Database.UpdateProgressionData(new State.LevelIndex((State.MissionType)Enum.Parse(typeof(State.MissionType), unlockable0[0].AsString()), unlockable0[1].AsInt32(), 1));

                            }
                            else Database.ResetProgression();
                        }

                        LoadingScreen newLoad = loadingScreenFactory.Instantiate<LoadingScreen>();

                        GetTree().Root.AddChild(newLoad);

                        GetTree().Root.GetChild(0).Free();
                        Player.Instance.QueueFree();
                        ViewPlayer.Instance.QueueFree();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
