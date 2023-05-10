using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters
{
	public partial class Player : Character
	{
		static private Player _instance;

		static public Player Instance
		{
			get
			{
				if (_instance == null) _instance = new Player();
				return _instance;
			}
		}



		private Player():base() { }



        [Export]
        private NodePath
            killerPath;



		private RayCast2D
			killer;



        public override void Init()
        {
            base.Init();

			killer = GetNode<RayCast2D>(killerPath);
        }



        public override void _Ready()
		{
			if (_instance != null)
			{
				QueueFree();
				GD.Print(nameof(Player) + " Instance already exist, destroying the last added.");
				return;
			}

			base._Ready();

			GD.Print(nameof(Player) + " is in!");
		}



		public override void _Process(double delta)
		{
			base._Process(delta);
		}



		protected override void Dispose(bool pDisposing)
		{
			if (pDisposing && _instance == this) _instance = null;
			base.Dispose(pDisposing);
		}
	}
}
