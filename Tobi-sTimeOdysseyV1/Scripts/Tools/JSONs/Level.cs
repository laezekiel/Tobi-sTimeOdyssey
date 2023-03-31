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
    /// A Level structure contain every information needed for a Level of Tobi's Time Odyssey <br/>
    /// </summary>
    public struct Level
    {
        public enum FieldLevel
        {
            era,
            skin,
            map,
            ground,
            enemys,
            traps,
            type,
            rotation,
            path,
            launchers,
            bouncer,
            nails,
            heat,
            time
        }
        /// <summary>
        /// Create a new Level with a an integer pLevelsJSON <br/>
        /// / ! \ check that pLevelsJSON is inferior or equal to the highest level of complikated
        /// </summary>
        /// <param name="pLevelsJSON"></param>
        public Level(List<string> pLevel)
        {
            _name = pLevel[0] + " " + pLevel[1] + "." + pLevel[2];
            Godot.Collections.Dictionary lJson = Tobi_Data_JSON.LoadFileLV(pLevel);
            Godot.Collections.Dictionary lMap = lJson[FieldLevel.map.ToString()] as Godot.Collections.Dictionary;
            Godot.Collections.Array lGround = lMap[FieldLevel.ground.ToString()] as Godot.Collections.Array;
            Godot.Collections.Array lEnemies = lMap[FieldLevel.enemys.ToString()] as Godot.Collections.Array;
            Godot.Collections.Dictionary lTraps = lMap[FieldLevel.traps.ToString()] as Godot.Collections.Dictionary;
            Godot.Collections.Array lLaunchers = lTraps[FieldLevel.launchers.ToString()] as Godot.Collections.Array;
            Godot.Collections.Array lBouncerRotation = lTraps[FieldLevel.bouncer.ToString()] as Godot.Collections.Array;
            Godot.Collections.Array lNailsRotation = lTraps[FieldLevel.nails.ToString()] as Godot.Collections.Array;
            Godot.Collections.Array lHeat = lTraps[FieldLevel.heat.ToString()] as Godot.Collections.Array;

            _time = lJson[FieldLevel.era.ToString()].ToString().ToInt();
            _skin = lJson[FieldLevel.skin.ToString()].ToString().ToInt();
            _ground = new List<string>(); 
            _enemyRotation = new List<List<float>>();
            _enemyPath = new List<List<Vector2>>();
            _enemyType = new List<Vector2>();
            _launcherRotation = new List<float>();
            _launcherShotSpeed = new List<float>();
            _bouncerRotation = new List<float>();
            _nailsRotation = new List<float>();
            _heatRotation = new List<float>();
            _heatTime = new List<float>();
            _levelCutscenes = new CutscenesText(pLevel);

            int lEnemiesIndex = 0;

            foreach (var ground in lGround) _ground.Add(ground.ToString());

            foreach (Godot.Collections.Dictionary enemy in lEnemies)
            {
                Godot.Collections.Array lEnemyRotation = enemy[FieldLevel.rotation.ToString()] as Godot.Collections.Array;
                Godot.Collections.Array lEnemyPath = enemy[FieldLevel.path.ToString()] as Godot.Collections.Array;
                Godot.Collections.Array lEnemyType = enemy[FieldLevel.type.ToString()] as Godot.Collections.Array;

                _enemyRotation.Add(new List<float>());
                _enemyPath.Add(new List<Vector2>());
                _enemyType.Add(new Vector2(lEnemyType[0].ToString().ToFloat(), lEnemyType[1].ToString().ToFloat()));

                foreach (var rotation in lEnemyRotation) _enemyRotation[lEnemiesIndex].Add(rotation.ToString().ToFloat());

                foreach (Godot.Collections.Array path in lEnemyPath) _enemyPath[lEnemiesIndex].Add(new Vector2(path[1].ToString().ToFloat(), path[0].ToString().ToFloat()));
                
                lEnemiesIndex++;
            }
            foreach (Godot.Collections.Dictionary launcher in lLaunchers)
            {
                _launcherRotation.Add(launcher[FieldLevel.rotation.ToString()].ToString().ToFloat());
                _launcherShotSpeed.Add(launcher[FieldLevel.time.ToString()].ToString().ToFloat());
            }
            foreach (var rot in lBouncerRotation) _bouncerRotation.Add(rot.ToString().ToFloat());
            
            foreach (var rot in lNailsRotation) _nailsRotation.Add(rot.ToString().ToFloat());
            
            foreach (Godot.Collections.Dictionary heat in lHeat)
            {
                _heatRotation.Add(heat[FieldLevel.rotation.ToString()].ToString().ToFloat());
                _heatTime.Add(heat[FieldLevel.time.ToString()].ToString().ToFloat());
            }
        }

        private string
            _name;

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

        private CutscenesText
            _levelCutscenes;

        /// <summary>
        /// return the value of Par in int
        /// </summary>
        public int Par { get { return _time; } }

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

        public CutscenesText Cutscenes { get { return _levelCutscenes; } }

        public string ToStringLV()
        {
            string lText = _name;

            return lText;
        }

    }
}
