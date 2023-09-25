using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.IO;
using Godot.Collections;
using System.Collections.Generic;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers.Enemies;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Scenes
{
	public partial class LevelGenerator : Node2D
	{
        //static private LevelGenerator _instance;

        //static public LevelGenerator Instance
        //{
        //    get
        //    {
        //        if (_instance == null) _instance = new LevelGenerator();
        //        return _instance;
        //    }
        //}



        //private LevelGenerator() : base() { }



        public enum LevelKeys
		{
			Era,
			Place,
			Player,
			Floor,
			Wall,
			Bumper,
			Spike,
			Shooter,
			Hot,
			Character
		}



		public struct Level
		{
			public Level(System.Collections.Generic.Dictionary<LevelKeys, object> pJson)
			{
				string enumString = pJson[LevelKeys.Era] as string;
                State.Eras enumValue;

                if (Enum.TryParse(enumString, out enumValue)) State.Era = enumValue;
                else GD.Print($"Failed to convert { enumString } to enum.");
                

				enumString = pJson[LevelKeys.Place] as string;
                State.Places enumValue2;

                if (Enum.TryParse(enumString, out enumValue2)) State.Place = enumValue2; 
                else GD.Print($"Failed to convert { enumString } to enum."); 
				
				List<object> playerTempPos = pJson[LevelKeys.Player] as List<object>;
				_player = new Vector2I((int)playerTempPos[0], (int)playerTempPos[1]);


                List<string> list = new List<string>();

                foreach (object String in pJson[LevelKeys.Floor] as List<object>)
                {
                    list.Add(String as string);
                }

                _floor = list;


                list = new List<string>();

                foreach (object String in pJson[LevelKeys.Wall] as List<object>)
                {
                    list.Add(String as string);
                }

                _wall = list;


				list = new List<string>();

                foreach (object String in pJson[LevelKeys.Bumper] as List<object>)
                {
                    list.Add(String as string);
                }

                _bumper = list;


				list = new List<string>();

                foreach (object String in pJson[LevelKeys.Spike] as List<object>)
                {
                    list.Add(String as string);
                }

                _spike = list;


				list = new List<string>();

                foreach (object String in pJson[LevelKeys.Shooter] as List<object>)
                {
                    list.Add(String as string);
                }

                _shooter = list;


				list = new List<string>();

                foreach (object String in pJson[LevelKeys.Hot] as List<object>)
                {
                    list.Add(String as string);
                }

                _hot = list;

				_character = new System.Collections.Generic.Dictionary<Vector2I, List<string>>();

				List<object> dataChara = (pJson[LevelKeys.Character] as List<object>)[0] as List<object>;
				List<object> dataChara2 = dataChara[1] as List<object>;
				List<object> dataChara3 = dataChara2[1] as List<object>;
                list = new List<string>();

                foreach (object String in dataChara3)
                {
                    list.Add(String as string);
                }

                _character.Add(new Vector2I((int)dataChara[0], (int)dataChara2[0]), list);


                dataChara = (pJson[LevelKeys.Character] as List<object>)[1] as List<object>;
				dataChara2 = dataChara[1] as List<object>;
                dataChara3 = dataChara2[1] as List<object>;
                list = new List<string>();

                foreach (object String in dataChara3)
                {
                    list.Add(String as string);
                }

                _character.Add(new Vector2I((int)dataChara[0], (int)dataChara2[0]), list); 


                dataChara = (pJson[LevelKeys.Character] as List<object>)[2] as List<object>;
                dataChara2 = dataChara[1] as List<object>;
                dataChara3 = dataChara2[1] as List<object>;
                list = new List<string>();

                foreach (object String in dataChara3)
                {
                    list.Add(String as string);
                }

                _character.Add(new Vector2I((int)dataChara[0], (int)dataChara2[0]), list);
            }


			private Vector2I
				_player;
			public Vector2I Player { get { return _player; } }


			private List<string>
				_floor,
				_wall,
				_bumper,
				_spike,
				_shooter,
				_hot;

			public List<string> Floor { get { return _floor; } }
			public List<string> Wall { get { return _wall; } }
			public List<string> Bumper { get { return _bumper; } }
			public List<string> Spike { get { return _spike; } }
			public List<string> Shooter { get { return _shooter; } }
			public List<string> Hot { get { return _hot; } }


			private System.Collections.Generic.Dictionary<Vector2I, List<string>>
				_character;

			public System.Collections.Generic.Dictionary<Vector2I, List<string>> Character { get { return _character; } }

            public override string ToString()
			{
				string text = $"{LevelKeys.Floor} : \n";
				foreach (string item in _floor) text += item + "\n";

				text += $"{LevelKeys.Wall} : \n";
				foreach (string item in _wall) text += item + "\n";

				text += $"{LevelKeys.Bumper} : \n";
				foreach (string item in _bumper) text += item + "\n";

				text += $"{LevelKeys.Spike} : \n";
				foreach (string item in _spike) text += item + "\n";

				text += $"{LevelKeys.Shooter} : \n";
				foreach (string item in _shooter) text += item + "\n";

				text += $"{LevelKeys.Hot} : \n";
				foreach (string item in _hot) text += item + "\n";

				text += $"{LevelKeys.Character} : \n";
				return text;
			}
		}



		[Export]
		private NodePath
			floorPath,
			wallPath,
			characterPath;



		private TileMap
			floor,
			wall,
			character;



		public Level Current_Level = new Level();



        public static List<Villager>
            Villager_List;

        public static List<Enemy>
            Enemy_List;

		public static List<int>
			Pattern_List;



        public void Init()
		{
			floor = GetNode<TileMap>(floorPath);
			wall = GetNode<TileMap>(wallPath);
			character = GetNode<TileMap>(characterPath);
		}



        public override void _Ready()
		{
			base._Ready();

            //if (_instance != null)
            //{
            //    QueueFree();
            //    GD.Print(nameof(LevelGenerator) + " Instance already exist, destroying the last added.");
            //    return;
            //}
            //else _instance = new LevelGenerator();

            Init();

            ReadJSONLevel();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

			if (Enemy_List.Count == 0 && (State.Current_State == State.GameState.Player_Aiming || State.Current_State == State.GameState.Player_Dashing)) State.SetGameToWin();
		}



        public void ReadJSONLevel()
		{
            Dictionary jsonTEMP = State.ReadJSON(File.ReadAllText(State.GetLevelJSONPath(State.Level_Index))).Obj as Dictionary;
            System.Collections.Generic.Dictionary<LevelKeys, object> json = new System.Collections.Generic.Dictionary<LevelKeys, object>();

			foreach (LevelKeys key  in Enum.GetValues(typeof(LevelKeys)))
			{
                json.Add(key, State.AsACSObject(jsonTEMP[key.ToString()]));
            }

            Current_Level = new Level(json);
        }



		public void LoadMap()
		{
			character.SetCell(0, Current_Level.Player, 1, Vector2I.Zero, 1);


            int numLine = Current_Level.Floor.Count;
            int lineSize;

            for (int j = 0; j < numLine; j++)
            {
				lineSize = Current_Level.Floor[j].Length;
				for (int i = 0; i < lineSize; i++)
				{
					if (Current_Level.Floor[j][i] == ' ')
					{
						switch (State.Era)
						{
							case State.Eras.Edo:
								floor.SetCell(0, new Vector2I(i, j), 0, new Vector2I(0, 0));
								break;
							case State.Eras.Present:
                                floor.SetCell(0, new Vector2I(i, j), 0, new Vector2I(0, 1));
                                break;
							default:
								break;
						}
					}
				}
			}


			numLine = Current_Level.Wall.Count;
			int wallIndex = 0;

			for (int j = 0; j < numLine; j++)
			{
				lineSize = Current_Level.Wall[j].Length;
				for (int i = 0; i < lineSize; i++)
				{

					switch (Current_Level.Wall[j][i])
					{
						case '8':
							wallIndex = 1;
							break;
						case '6':
							wallIndex = 2;
							break;
						case '2':
							wallIndex = 3;
							break;
						case '4':
							wallIndex = 4;
							break;
						case '9':
							wallIndex = 5;
							break;
						case '3':
							wallIndex = 6;
							break;
						case '1':
							wallIndex = 7;
							break;
						case '7':
							wallIndex = 8;
							break;
						case 'M':
							wallIndex = 9;
							break;
						case 'D':
							wallIndex = 10;
							break;
						case 'U':
							wallIndex = 11;
							break;
						case 'C':
							wallIndex = 12;
							break;
						case '5':
							wallIndex = 13;
							break;
						case '=':
							wallIndex = 14;
							break;
						case 'H':
							wallIndex = 15;
							break;
						default:
							break;
                    }
                    if (Current_Level.Wall[j][i] != ' ' && Current_Level.Wall[j][i] != '+') wall.SetCell(0, new Vector2I(i, j), 0, Vector2I.Zero, wallIndex);
                }
			}


			numLine = Current_Level.Bumper.Count;

			for (int j = 0; j < numLine; j++)
			{
				lineSize = Current_Level.Bumper[j].Length;
				for (int i = 0; i < lineSize; i++)
				{
					if (Current_Level.Bumper[j][i] != ' ' && Current_Level.Bumper[j][i] != '+') wall.SetCell(1, new Vector2I(i, j), 1, Vector2I.Zero, Current_Level.Bumper[j][i].ToString().ToInt());
				}
			}


			numLine = Current_Level.Spike.Count;

			for (int j = 0; j < numLine; j++)
			{
				lineSize = Current_Level.Spike[j].Length;
				for (int i = 0; i < lineSize; i++)
				{
					if (Current_Level.Spike[j][i] != ' ' && Current_Level.Spike[j][i] != '+') wall.SetCell(1, new Vector2I(i, j), 2, Vector2I.Zero, Current_Level.Spike[j][i].ToString().ToInt());
				}
			}


			numLine = Current_Level.Shooter.Count;

			for (int j = 0; j < numLine; j++)
			{
				lineSize = Current_Level.Shooter[j].Length;
				for (int i = 0; i < lineSize; i++)
				{
					if (Current_Level.Shooter[j][i] != ' ' && Current_Level.Shooter[j][i] != '+') wall.SetCell(1, new Vector2I(i, j), 3, Vector2I.Zero, Current_Level.Shooter[j][i].ToString().ToInt());
				}
			}


			numLine = Current_Level.Hot.Count;

			for (int j = 0; j < numLine; j++)
			{
				lineSize = Current_Level.Hot[j].Length;
				for (int i = 0; i < lineSize; i++)
				{
					if (Current_Level.Hot[j][i] != ' ' && Current_Level.Hot[j][i] != '+') wall.SetCell(1, new Vector2I(i, j), 4, Vector2I.Zero, Current_Level.Hot[j][i].ToString().ToInt());
				}
			}


			Enemy_List = new List<Enemy>();
			Villager_List = new List<Villager>();
			Pattern_List = new List<int>();

			for (int x = 2; x < 5; x++)
			{
				Vector2I test = new Vector2I(x, 1);
				numLine = Current_Level.Character.GetValueOrDefault(test).Count;
				List<string> temp = Current_Level.Character[new Vector2I(x, 1)];

				for (int j = 0; j < numLine; j++)
				{
					lineSize = temp[j].Length;
					for (int i = 0; i < lineSize; i++)
					{
						if (temp[j][i] != ' ' && temp[j][i] != '+')
						{
							character.SetCell(x - 1, new Vector2I(i, j), x, Vector2I.Zero, 1);

							Pattern_List.Add(temp[j][i].ToString().ToInt());
                        }
                    }
				}
			}


			State.SetGameToAim();

			GetTree().Root.GetChild(0).Free();

            Show();
        }
    }
}
