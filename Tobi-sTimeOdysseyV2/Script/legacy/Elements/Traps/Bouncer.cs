using Godot;
using System;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Elements.Traps
{
	public class Bouncer : Traps
	{

		public override void _Ready()
		{
			base._Ready();
        }

        public void Rebound(Player pPlayer)
        {
            switch (RotationDegrees)
            {
                case 360:
                    pPlayer.Rotation = Mathf.Cartesian2Polar(-Mathf.Cos(pPlayer.Rotation), Mathf.Sin(pPlayer.Rotation)).y;
                    break;
                case 90:
                    pPlayer.Rotation = Mathf.Cartesian2Polar(Mathf.Cos(pPlayer.Rotation), -Mathf.Sin(pPlayer.Rotation)).y;
                    break;
                case 180:
                    pPlayer.Rotation = Mathf.Cartesian2Polar(-Mathf.Cos(pPlayer.Rotation), Mathf.Sin(pPlayer.Rotation)).y;
                    break;
                case 270:
                    pPlayer.Rotation = Mathf.Cartesian2Polar(Mathf.Cos(pPlayer.Rotation), -Mathf.Sin(pPlayer.Rotation)).y;
                    break;
                default:
                    break;
            }
        }

        #region State Machine
        // Mode
        // Action
        #endregion
    }

}