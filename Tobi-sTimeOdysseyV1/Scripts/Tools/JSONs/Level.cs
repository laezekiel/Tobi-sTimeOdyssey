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
            Godot.Collections.Array lEnemies = lMap["enemys"] as Godot.Collections.Array;
            int lEnemiesIndex = 0;
            _enemyRotation = new List<List<float>>();
            _enemyPath = new List<List<Vector2>>();
            foreach (Godot.Collections.Dictionary enemy in lEnemies)
            {
                Godot.Collections.Array lEnemyRotation = enemy["rotation"] as Godot.Collections.Array;
                _enemyRotation.Add(new List<float>());
                foreach (var rotation in lEnemyRotation)
                {
                    _enemyRotation[lEnemiesIndex].Add(rotation.ToString().ToInt());
                }
                Godot.Collections.Array lEnemyPath = enemy["path"] as Godot.Collections.Array;
                _enemyPath.Add(new List<Vector2>());
                foreach (Godot.Collections.Array path in lEnemyPath)
                {
                    _enemyPath[lEnemiesIndex].Add(new Vector2(path[1].ToString().ToInt(), path[0].ToString().ToInt()));
                }
                lEnemiesIndex++;
            }
        }

        private int 
            _time,
            _skin;
        private List<string>
            _ground;
        private List<List<float>>
            _enemyRotation;
        private List<List<Vector2>>
            _enemyPath;

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
        /// return the value of Enemys Position in a list of list of int
        /// </summary>
        public List<List<float>> Enemies_Rotation { get { return _enemyRotation; } }
        /// <summary>
        /// return the value of Enemys Rotation in a list of list of Vector 2
        /// </summary>
        public List<List<Vector2>> Enemies_Path { get { return _enemyPath; } }
    }
}
