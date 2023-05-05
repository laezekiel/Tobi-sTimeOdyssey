using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters
{
	public class Enemy : Villager
	{
        private int 
            _life = 1;

        [Export]
        public int Life { get { return _life; } protected set { _life = value; } }

        public void LoseLife()
        {
            Life--;
            if (Life == 0)
            {
                QueueFree();
            }
        }

    }

}