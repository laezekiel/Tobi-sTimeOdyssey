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
            _time = Levels_JSON.GetLevelInfo(pLevelsJSON, Levels_JSON.Field.era).ToString().ToInt();
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
            _enemyType = new List<Vector2>();
            foreach (Godot.Collections.Dictionary enemy in lEnemies)
            {
                Godot.Collections.Array lEnemyRotation = enemy["rotation"] as Godot.Collections.Array;
                _enemyRotation.Add(new List<float>());
                foreach (var rotation in lEnemyRotation)
                {
                    _enemyRotation[lEnemiesIndex].Add(rotation.ToString().ToFloat());
                }
                Godot.Collections.Array lEnemyPath = enemy["path"] as Godot.Collections.Array;
                _enemyPath.Add(new List<Vector2>());
                foreach (Godot.Collections.Array path in lEnemyPath)
                {
                    _enemyPath[lEnemiesIndex].Add(new Vector2(path[1].ToString().ToFloat(), path[0].ToString().ToFloat()));
                }
                Godot.Collections.Array lEnemyType = enemy["type"] as Godot.Collections.Array;
                _enemyType.Add(new Vector2(lEnemyType[0].ToString().ToFloat(), lEnemyType[1].ToString().ToFloat()));
                lEnemiesIndex++;
            }
            Godot.Collections.Dictionary lTraps = lMap["traps"] as Godot.Collections.Dictionary;
            Godot.Collections.Array lLaunchers = lTraps["launchers"] as Godot.Collections.Array;
            _launcherRotation = new List<float>();
            _launcherShotSpeed = new List<float>();
            foreach (Godot.Collections.Dictionary launcher in lLaunchers)
            {
                _launcherRotation.Add(launcher["rotation"].ToString().ToFloat());
                _launcherShotSpeed.Add(launcher["time"].ToString().ToFloat());
            }
            Godot.Collections.Array lBouncerRotation = lTraps["bouncer"] as Godot.Collections.Array;
            _bouncerRotation = new List<float>();
            foreach (var rot in lBouncerRotation)
            {
                _bouncerRotation.Add(rot.ToString().ToFloat());
            }
            Godot.Collections.Array lNailsRotation = lTraps["nails"] as Godot.Collections.Array;
            _nailsRotation = new List<float>();
            foreach (var rot in lNailsRotation)
            {
                _nailsRotation.Add(rot.ToString().ToFloat());
            }
            Godot.Collections.Array lHeat = lTraps["heat"] as Godot.Collections.Array;
            _heatRotation = new List<float>();
            _heatTime = new List<float>();
            foreach (Godot.Collections.Dictionary heat in lHeat)
            {
                _heatRotation.Add(heat["rotation"].ToString().ToFloat());
                _heatTime.Add(heat["time"].ToString().ToFloat());
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
        private List<Vector2>
            _enemyType;
        private List<float>
            _launcherRotation,
            _launcherShotSpeed,
            _bouncerRotation,
            _nailsRotation,
            _heatRotation,
            _heatTime;

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
        /// return the value of Enemys Rotation in a list of list of int
        /// </summary>
        public List<List<float>> Enemies_Rotation { get { return _enemyRotation; } }
        /// <summary>
        /// return the value of Enemys Path in a list of list of Vector 2
        /// </summary>
        public List<List<Vector2>> Enemies_Path { get { return _enemyPath; } }
        public List<Vector2> Enemies_Type { get { return _enemyType; } }

        /// <summary>
        /// return the value of Launchers Rotation in a list of float
        /// </summary>
        public List<float> Launchers_Rotation { get { return _launcherRotation; } }
        /// <summary>
        /// return the value of Laucher Shot Speed in a list of float
        /// </summary>
        public List<float> Laucher_Shot_Speed { get { return _launcherShotSpeed; } }
        /// <summary>
        /// return the value of Bouncer Rotation in a list of float
        /// </summary>
        public List<float> Bouncer_Rotation { get { return _bouncerRotation; } }
        /// <summary>
        /// return the value of Nails Rotation in a list of float
        /// </summary>
        public List<float> Nails_Rotation { get { return _nailsRotation; } }
        /// <summary>
        /// return the value of Heat Rotation in a list of float
        /// </summary>
        public List<float> Heat_Rotation { get { return _heatRotation; } }
        /// <summary>
        /// return the value of Heat Time in a list of float
        /// </summary>
        public List<float> Heat_Time { get { return _heatTime; } }

    }
}
