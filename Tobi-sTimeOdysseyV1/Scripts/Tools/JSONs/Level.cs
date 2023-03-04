using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Author: Louis Bouré
namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs
{
    /// <summary>
    /// A Level structure contain every information needed for a Level of Complikated: <br/>
    /// 
    /// ____ the skin of the level in a int 
    /// 
    /// </summary>
    public struct Level
    {
        /// <summary>
        /// Create a new Level with a an integer pLevelsJSON <br/>
        /// / ! \ check that pLevelsJSON is inferior or equal to the highest level of complikated
        /// </summary>
        /// <param name="pLevelsJSON"></param>
        public Level(int pLevelsJSON)
        {
            _time = Levels_JSON.GetLevelInfo(pLevelsJSON, Levels_JSON.Field.time).ToString().ToInt();
            _skin = Levels_JSON.GetLevelInfo(pLevelsJSON, Levels_JSON.Field.skin).ToString().ToInt();
            Godot.Collections.Dictionary lMap = Levels_JSON.GetLevelInfo(pLevelsJSON, Levels_JSON.Field.map) as Godot.Collections.Dictionary;
            Godot.Collections.Array lGround = lMap["ground"] as Godot.Collections.Array;
            _ground = new List<string>();
            foreach (var ground in lGround)
            {
                _ground.Add(ground.ToString());
            }
            Godot.Collections.Array lEnemys = lMap["enemys"] as Godot.Collections.Array;
            List<Godot.Collections.Array> lEnemysPosition = new List<Godot.Collections.Array>();
            _enemys = new List<List<int>>();
            int lenemyIndex = 0;
            foreach (var e in lEnemys)
            {
                lEnemysPosition.Add(e as Godot.Collections.Array);
            }
            foreach (var eValue in lEnemysPosition)
            {
                _enemys.Add(new List<int>());
                foreach (var val in eValue)
                {
                    _enemys[lenemyIndex].Add(val.ToString().ToInt());
                }
                lenemyIndex++;
            }
            Godot.Collections.Array lPlayer = lMap["player"] as Godot.Collections.Array;
            _player = new Vector2(lPlayer[1].ToString().ToInt(), lPlayer[0].ToString().ToInt());
        }

        private int 
            _time,
            _skin;
        private List<string>
            _ground;
        private List<List<int>>
            _enemys;
        private Vector2
            _player;

        /// <summary>
        /// return the value of Par in int
        /// </summary>
        public int Par { get {return _time; } }
        /// <summary>
        /// return the value of Skin in int
        /// </summary>
        public int Skin { get { return _skin; } }
        /// <summary>
        /// return the value of Ground in a list of string
        /// </summary>
        public List<string> Ground { get { return _ground; } }
        /// <summary>
        /// return the value of Enemys in a list of a list of int
        /// </summary>
        public List<List<int>> Enemies { get { return _enemys; } }
        /// <summary>
        /// return the value of Player in a Vector 2 
        /// </summary>
        public Vector2 Player { get { return _player; } }
    }
}
