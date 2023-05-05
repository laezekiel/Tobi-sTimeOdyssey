using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Com.IronicEntertainment.TobisTimeOdyssey
{

    public class LoadingStartScreen : Node2D
    {
        [Export]
        private NodePath
            WelcomePath,
            TitlePath,
            loaderPath,
            ToLoadPath;

        [Export]
        private List<Color>
            loaderColors = new List<Color>()
            {
                Colors.Green,
                new Color(Colors.Green, 0.5f)
            },
            ToLoadColors = new List<Color>() 
            {
                new Color(Colors.Red, 0.5f),
                Colors.Red
            };

        private PackedScene gameScene;

        private Node2D preload;


        private AnimatedSprite Loader { get { return GetNode<AnimatedSprite>(loaderPath); }  }
        private AnimatedSprite ToLoad { get { return GetNode<AnimatedSprite>(ToLoadPath); } }


        private VBoxContainer Welcome { get { return GetNode<VBoxContainer>(WelcomePath); } }
        private VBoxContainer Title { get { return GetNode<VBoxContainer>(TitlePath); } }


        private const string
            LEVEL = "Level";


        private static Stopwatch stopwatch = new Stopwatch();


        private float _time;
        private float Timer { get { if (_time > 3f) return _time; else return 3f; } }

        public override Godot.Collections.Array _GetPropertyList()
        {
            Godot.Collections.Array properties = new Godot.Collections.Array();

            Godot.Collections.Dictionary category = new Godot.Collections.Dictionary()
            {
                { "name", LEVEL },
                { "type", Variant.Type.Nil },
                { "usage", PropertyUsageFlags.Category }
            };

            return properties;
        }

        
        public override void _Ready()
        {
            base._Ready();

            StateMachine.Current_State = StateMachine.State.GameSceneloading;

            Loader.Modulate = loaderColors[0];
            ToLoad.Modulate = ToLoadColors[0];


            InitializeAsync();
        }


        private async void InitializeAsync()
        {
            stopwatch.Start();

            Task task1 = CheckDataBaseAsync();

            stopwatch.Stop();

            _time = (float)stopwatch.Elapsed.TotalSeconds;


            Tween lLoad = new Tween();
            AddChild(lLoad);

            lLoad.Connect("tween_all_completed", this, nameof(OnTweenCompleted));

            lLoad.InterpolateProperty(Loader, "frame", 0, 48, Timer);
            lLoad.InterpolateProperty(ToLoad, "frame", 0, 48, Timer);

            lLoad.Start();


            Tween lWright = new Tween();
            AddChild(lWright);

            lWright.InterpolateProperty(Welcome, "modulate", Welcome.Modulate, POC.Visible, Timer / 5);

            float lDelay = lWright.GetRuntime() + Timer / 5;

            lWright.InterpolateProperty(Welcome, "modulate", Welcome.Modulate, POC.Invisible, Timer / 5, delay: lDelay);

            lDelay = lWright.GetRuntime();

            lWright.InterpolateProperty(Title, "modulate", Welcome.Modulate, POC.Visible, Timer / 5, delay: lDelay);

            lWright.Start();


            await task1;
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            if (Input.IsActionJustPressed("Reset_Data_Base")) { SQLCommands.ResetDataBase(); MOC.Quit(this); }            
        }

        public async Task CheckDataBaseAsync()
        {
            await Task.Run(() =>
            {
                // Code for database check here
                foreach (SQLCommands.Table enumValue in Enum.GetValues(typeof(SQLCommands.Table))) SQLCommands.CreateTable(enumValue);
            });
        }

        private void OnTweenCompleted()
        {
            MOC.LoadScene(AllPath.Game_Container, this);
        }
    }

}