using Com.BeerAndDev.TobisTimeOdyssey.Elements.AddOns;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers;
using Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers.Enemies;
using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using Godot.Collections;
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
            killerPath,
			ambiencePath;



		private RayCast2D
			killer;

		private PointLight2D
			ambience;



		private Vector2
			direction;



        public override void Init()
        {
            base.Init();

			killer = GetNode<RayCast2D>(killerPath);

			ambience = GetNode<PointLight2D>(ambiencePath);

			killer.ExcludeParent = true;
        }



        public override void _Ready()
		{
			if (_instance != null)
			{
				QueueFree();
				GD.Print(nameof(Player) + " Instance already exist, destroying the last added.");
				return;
			}
			else _instance = new Player();

			base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

			POC.Player_Position = GlobalPosition;
        }



        public override void GameStateIsAiming()
        {
            RotationDegrees = POC.Player_Rotation;

			POC.Player_Can_Jump = CanJump();
        }

        public override void GameStateIsDashing()
        {
			UpdateDirection();

			if (MoveAndCollide(direction) != null)
            {
                CheckEntered(MoveAndCollide(direction).GetCollider() as Node2D);
            }

            MoveAndCollide(direction);

			if (killer.GetCollider() is Enemy)
			{
				Enemy target = killer.GetCollider() as Enemy;
				if(!(target.doAction == target.KnockOut)) target.LoseLife();
			}
        }



        public override void GameStateIsCaught()
        {
            base.GameStateIsCaught();
			switch (State.Lose_Type)
			{
				case State.LoseType.burnt:
                    body.Animation = "burnt";
                    break;
				case State.LoseType.dead:
                    body.Animation = "dead";
                    break;
				case State.LoseType.caught:
					body.Animation = "caught";
					break;
				default:
					break;
			}
		}



        public void CheckEntered(Node2D pBody)
        {
            switch (pBody)
            {
                case Wall _:
                    CollideWall(pBody as Wall);
                    break;
                case Bouncer _:
                    CollideBouncer(pBody as Bouncer);
                    break;
				case Spike _:
					CollideSpike(pBody as Spike);
					break;
                case Enemy _:
                    CollideEnemy(pBody as Enemy);
                    break;
                case Villager _:
                    CollideVillager(pBody as Villager);
                    break;
                default:
                    break;
            }
        }



        public  void CollideWall(Wall pBody)
        {
			State.SetGameToAim();
        }

        public void CollideEnemy(Enemy pBody)
        {
            RotationDegrees -= 180;

			UpdateDirection();
        }

        public void CollideBouncer(Bouncer pBody)
		{
			float rD = Rotation - pBody.Rotation;
			float modif;

            if (Mathf.Abs(pBody.Rotation) % (Mathf.Pi / 2) != 0)
			{
				rD *= 2;
				modif = 0;
            }
			else
			{
				modif = (Mathf.Abs(pBody.Rotation) % Mathf.Pi) * 2 * Mathf.Pi;

            }

            Rotation = pBody.Rotation + modif - rD;

			UpdateDirection();
        }

        public void CollideSpike(Spike pBody)
        {
            State.SetLoseTypeToDead();
            State.SetGameToCaught();
		}

        public void CollideVillager(Villager pBody) 
		{ 
			State.SetLoseTypeToCaught();
			State.SetGameToCaught();
		}



        public bool CanJump()
		{
			Array<Node2D> array = check.GetOverlappingBodies();

			foreach (Node2D node in array)
			{
				if (node is Wall) return false; else continue;
			}

			return true;
		}

		public void UpdateDirection()
		{
            direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized()*2.5f;
        }



        protected override void Dispose(bool pDisposing)
		{
			if (pDisposing && _instance == this) _instance = null;
			base.Dispose(pDisposing);
		}
	}
}
