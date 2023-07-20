using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Elements.AddOns
{
    public partial class AddOn : StaticBody2D
	{
		protected RandomNumberGenerator
			rand = new RandomNumberGenerator();



		public virtual void Init()
		{
			rand.Randomize(); 
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
