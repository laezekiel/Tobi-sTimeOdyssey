using Com.BeerAndDev.TobisTimeOdyssey.Data;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using Godot.Collections;
using Microsoft.Data.Sqlite;

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
			Database.CreateDatabaseAndTables();

			if (State.allFilePath == null) State.allFilePath = new System.Collections.Generic.Dictionary<State.MissionType, Array<Array<string>>>()
											{
												{ State.MissionType.Tutorial, allTutorialPath },
												{ State.MissionType.Mission, allMissionPath },
												{ State.MissionType.Bonus, allBonusPath },
											};

			GoThroughArrayToData(allTutorialPath);
			GoThroughArrayToData(allMissionPath, 1);
			GoThroughArrayToData(allBonusPath, 2);
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

		public void GoThroughArrayToData(Array<Array<string>> pArray, int ptype = 0)
		{
            int arrayLength1 = pArray.Count;
            int arrayLength2;
			int counter = 0;

            for (int i = 0; i < arrayLength1; i++)
            {
                arrayLength2 = pArray[i].Count;

                for (int j = 0; j < arrayLength2; j++)
                {
					counter++;
					if (counter > Database.GetTableRowCount(ptype)) Database.CreateLevelData(i, j, ptype);
                }
            }
        }
	}
}
