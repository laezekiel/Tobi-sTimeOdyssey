using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{
    public static class StateMachine
    {
        public enum State
        {
            GameSceneloading,
            Playing,
        }

        public static State Current_State;
    }
}
