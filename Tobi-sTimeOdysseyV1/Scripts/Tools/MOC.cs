using Com.IronicEntertainment.TobisTimeOdyssey.Tools.Dictionarys;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools.Enums;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{
	/// <summary>
	/// Method On Call
	/// </summary>
	static public class MOC
	{
        //static public void LoadScene(AllPath pPath, Node pCaller)
        //{
        //    pCaller.GetTree().ChangeScene(PathDic.GetPath(pPath));
        //}

        static public void Retry()
        {
            POC.Player_Manager.ResetPlayer();
            POC.Enemy_Manager.ResetEnemies();
            POC.Trap_Manager.ResetTraps();
            POC.Field_Manager.Retry();
            POC.Game_Manager.SetGameModePlay();
        }

        static public void Lose()
        {
            POC.Game_Manager.SetGameModeLose();
        }

        #region Connect
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSignal"></param>
        /// <param name="pButton"></param>
        /// <param name="pCaller"></param>
        /// <param name="pMethod"></param>
        /// <param name="binds"></param>
        /// <param name="flags"></param>
        static public void NewConnect(AllSignal pSignal, BaseButton pButton, Node pCaller, string pMethod, Godot.Collections.Array binds = null, uint flags = 0)
        {
            pButton.Connect(SignalDic.GetSignal(pSignal), pCaller, pMethod, binds, flags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSignal"></param>
        /// <param name="pTimer"></param>
        /// <param name="pCaller"></param>
        /// <param name="pMethod"></param>
        static public void NewConnect(AllSignal pSignal, Timer pTimer, Node pCaller, string pMethod)
        {
            pTimer.Connect(SignalDic.GetSignal(pSignal), pCaller, pMethod);
        }
        #endregion

        /// <summary>
        /// close Game
        /// </summary>
        /// <param name="pCaller"></param>
        static public void Quit(Node pCaller)
		{
			pCaller.GetTree().Quit();
		}

		//static public void StartSound(AllSound pSound)
		//      {
		//	SoundManager.GetInstance().Sound(pSound);
		//      }
	}

}