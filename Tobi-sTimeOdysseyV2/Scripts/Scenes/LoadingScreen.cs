using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Formats.Asn1.AsnWriter;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Scenes
{
	public partial class LoadingScreen : Node2D
    {
		[Export]
		private Array<Array<string>>
			allTutorialPath = new Array<Array<string>>()
			{
				new Array<string>()
				{
					""
				},
			},
            allMissionPath = new Array<Array<string>>()
			{
				new Array<string>()
				{
					""
				}, 
			},
			allBonusPath = new Array<Array<string>>()
			{
				new Array<string>()
				{
					""
				}, 
			};

		[Export]
		private PackedScene
			levelGeneratorFactory;



		private void Init()
		{
            State.allFilePath = new System.Collections.Generic.Dictionary<State.MissionType, Array<Array<string>>>()
            {
                { State.MissionType.Tutorial, allTutorialPath },
                { State.MissionType.Mission, allMissionPath },
                { State.MissionType.Bonus, allBonusPath },
            };
        }



		public override void _Ready()
		{
			base._Ready();
            Init();

			State.SetGameToLoad();
        }



		public override void _Process(double delta)
		{
			base._Process(delta);

			if (GetTree().Root.GetChildren().Count < 2) CreateLevelGenerator();
		}



		public void CreateLevelGenerator()
		{
            LevelGenerator currentLevel = levelGeneratorFactory.Instantiate<LevelGenerator>();
            currentLevel.Hide();
            GetTree().Root.AddChild(currentLevel);

			currentLevel.LoadMap();
        }
	}
}
