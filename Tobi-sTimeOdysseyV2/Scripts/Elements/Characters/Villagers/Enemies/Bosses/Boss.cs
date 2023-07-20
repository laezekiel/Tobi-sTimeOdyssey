using Com.BeerAndDev.TobisTimeOdyssey.Tools;
using Godot;
using Godot.Collections;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters.Villagers.Enemies.Bosses
{
	public partial class Boss : Enemy
	{
		[Export]
		protected NodePath
			awarenessFullPath,
			awarenessCheckPath,
			awarenessSightPath,
			awarenessOccluderPath;



		protected Node2D
			awarenessFull;

		protected Area2D
			awarenessCheck;

		protected PointLight2D
			awarenessSight;

		protected LightOccluder2D
			awaranessOccluder;



        public override void Init()
        {
            base.Init();

			awarenessFull = GetNode<Node2D>(awarenessFullPath);

			awarenessCheck = GetNode<Area2D>(awarenessCheckPath);

			awarenessSight = GetNode<PointLight2D>(awarenessSightPath);

			awaranessOccluder = GetNode<LightOccluder2D>(awarenessOccluderPath);
        }



        public override void _Ready()
		{
			base._Ready();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);

            awarenessFull.RotationDegrees += .25f;
        }



        protected override void SwitchState(string pState)
        {
            base.SwitchState(pState);

			switch (pState)
			{
				case "dead":
					awarenessCheck.Monitoring = awarenessSight.Enabled = false;
					break;
				case "idle":
                    awarenessCheck.Monitoring = awarenessSight.Enabled = true;
                    break;
                default:
					break;
			}
		}

        protected override void KillPlayer()
        {
            base.KillPlayer();

            Array<Node2D> array = awarenessCheck.GetOverlappingBodies();

            foreach (Node2D node in array)
            {
                if (node is Player)
                {
                    animation.Kill();
                    body.Animation = "happy";
                    State.SetLoseTypeToCaught();
                    State.SetGameToCaught();
                }
                else continue;
            }
        }
    }
}
