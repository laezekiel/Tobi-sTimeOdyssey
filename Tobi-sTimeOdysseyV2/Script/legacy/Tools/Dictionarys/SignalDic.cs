using Com.IronicEntertainment.TobisTimeOdyssey.Tools.Enums;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools.Dictionarys
{

	static public class SignalDic
	{
		static private Dictionary<AllSignal, string>
			signal = new Dictionary<AllSignal, string>()
			{
				{AllSignal.Timeout , "timeout"},
				{AllSignal.Clicked , "pressed"},
				{AllSignal.TweenFinished , "tween_completed"},
				{AllSignal.SceneTreetweenFinished , "finished"},
			};

		static public string GetSignal(AllSignal pPathToGet)
        {
			return signal[pPathToGet];
        }
	}

}