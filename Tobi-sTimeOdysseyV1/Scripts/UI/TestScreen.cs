using Com.IronicEntertainment.TobisTimeOdyssey.Elements;
using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{
	public class TestScreen : Control
	{
        [Export]
        private NodePath
            startPath,
            ignoreAllCutPath,
            ignoreBeginingCutPath,
            ignoreEndCutPath,
            resetAllCutPath,
            resetBeginingCutPath,
            resetEndCutPath,
            playPath;

        private Button
            play;

        private CheckButton
            start,
            ignoreAllCut,
            ignoreBeginingCut,
            ignoreEndCut,
            resetAllCut,
            resetBeginingCut,
            resetEndCut;

        public void Init()
        {
            start = GetNode<CheckButton>(startPath);
            ignoreAllCut = GetNode<CheckButton>(ignoreAllCutPath);
            ignoreBeginingCut = GetNode<CheckButton>(ignoreBeginingCutPath);
            ignoreEndCut = GetNode<CheckButton>(ignoreEndCutPath);
            resetAllCut = GetNode<CheckButton>(resetAllCutPath);
            resetBeginingCut = GetNode<CheckButton>(resetBeginingCutPath);
            resetEndCut = GetNode<CheckButton>(resetEndCutPath);

            play = GetNode<Button>(playPath);

            play.Connect("pressed", this, nameof(LoadGame));

            //start.Pressed = PlayTest.StartAtBegining;
            //ignoreAllCut.Pressed = PlayTest.IgnoreAllCutScenes;
            //ignoreBeginingCut.Pressed = PlayTest.IgnoreBeginingCutScenes;
            //ignoreEndCut.Pressed = PlayTest.IgnoreEndCutScenes;
            //resetAllCut.Pressed = PlayTest.ResetAllCutScenes;
            //resetBeginingCut.Pressed = PlayTest.ResetBeginingCutScenes;
            //resetEndCut.Pressed = PlayTest.ResetEndCutScenes;
        }
        
		public override void _Ready()
		{
            Init();
		}

        public override void _Process(float delta)
        {
            base._Process(delta);
            //PlayTest.StartAtBegining = start.Pressed;
            //PlayTest.IgnoreAllCutScenes = ignoreAllCut.Pressed;
            //PlayTest.IgnoreBeginingCutScenes = ignoreBeginingCut.Pressed;
            //PlayTest.IgnoreEndCutScenes = ignoreEndCut.Pressed;
            //PlayTest.ResetAllCutScenes = resetAllCut.Pressed;
            //PlayTest.ResetBeginingCutScenes = resetBeginingCut.Pressed;
            //PlayTest.ResetEndCutScenes = resetEndCut.Pressed;

            if (ignoreAllCut.Pressed) { ignoreBeginingCut.Visible = false; ignoreEndCut.Visible = false; }
            else { ignoreAllCut.Visible = false; ignoreBeginingCut.Visible = true; ignoreEndCut.Visible = true; }

            if (ignoreEndCut.Pressed && ignoreBeginingCut.Pressed) { ignoreAllCut.Visible = true; ignoreAllCut.Pressed = true; ignoreBeginingCut.Pressed = false; ignoreEndCut.Pressed = false; }

            if (resetAllCut.Pressed) { resetBeginingCut.Visible = false; resetEndCut.Visible = false; }
            else { resetAllCut.Visible = false; resetBeginingCut.Visible = true; resetEndCut.Visible = true; }

            if (resetEndCut.Pressed && resetBeginingCut.Pressed) { resetAllCut.Visible = true; resetAllCut.Pressed = true; resetBeginingCut.Pressed = false; resetEndCut.Pressed = false; }
        }

        public void LoadGame()
        {
            GetTree().ChangeScene("res://Scenes/GameContainer.tscn");
        }
	}

}