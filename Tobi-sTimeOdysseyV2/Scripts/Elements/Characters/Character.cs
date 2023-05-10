using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.Characters
{
	public partial class Character : CharacterBody2D
    {
        [Export]
        private NodePath
            colliderPath,
            checkPath,
            bodyPath,
            sightPath;



        protected CollisionPolygon2D
            collider,
            check;

        protected AnimatedSprite2D
            body;

        protected PointLight2D
            sight;



        public virtual void Init()
        {
            collider = GetNode<CollisionPolygon2D>(colliderPath);

            check = GetNode<CollisionPolygon2D>(checkPath);

            body = GetNode<AnimatedSprite2D>(bodyPath);

            sight = GetNode<PointLight2D>(sightPath);
        }



        public override void _Ready()
		{
			base._Ready();

            Init();
		}



		public override void _Process(double delta)
		{
			base._Process(delta);
		}
	}
}
