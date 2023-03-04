using Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Author: Louis Bouré
namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools
{
    /// <summary>
    /// Contain All Complikated Level
    /// </summary>
    public static class AllLevels
    {
        private static List<Level> allLevels;

        /// <summary>
        /// if allLevels undefined generate all Level
        /// </summary>
        private static void Init()
        {
            if (allLevels == null)
            {
                allLevels = new List<Level>();
                int lLimit = Levels_JSON.GetNumberLevel();
                for (int i = 0; i < lLimit; i++)
                {
                    allLevels.Add(new Level(i));
                }
            }
        }

        /// <summary>
        /// reaturn all Complikated level
        /// </summary>
        /// <returns></returns>
        public static List<Level> GetAllLevel()
        {
            Init();
            return allLevels;
        }

        /// <summary>
        /// return the pLevel demanded
        /// </summary>
        /// <param name="pLevel"></param>
        /// <returns></returns>
        public static Level GetLevel(int pLevel = 0)
        {
            Init();
            return allLevels[pLevel];
        }
    }
}
