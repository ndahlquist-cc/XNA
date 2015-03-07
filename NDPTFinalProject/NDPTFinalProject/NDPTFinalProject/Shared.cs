/*  Shared.cs
 *  
 *  This class contains public static variables for easy sharing of information between classes
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + Added stage
 *  Revision History:
 *  2014-11-11
 *      + added isKittyDead
 *      + added worldSpeed
 *  2014-11-17
 *      + added score
 *  2014-11-23
 *      + added level
 *  2014-11-30
 *      + added minMenuIndex
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace NDPTFinalProject
{
    /// <summary>
    /// Class containing public static variables for easy sharing of information between classes 
    /// </summary>
    class Shared
    {
        //Shared class variables
        public static Vector2 stage;
        public static bool isKittyDead;
        public static bool isKittyZombified;
        public static bool isKittyFried;
        public static bool isStartScene;
        public static Vector2 worldSpeed;
        public static int score;
        public static int level;
        public static int minMenuIndex;
        
        /// <summary>
        /// Method to initialize shared class variables 
        /// </summary>
        public static void initializeShared()
        {
            isKittyDead = false;
            isKittyFried = false;
            isKittyZombified = false;
            isStartScene = true;
            score = 0;
            level = 1;
            worldSpeed = new Vector2(3,2);
            minMenuIndex = 1;
        }
        
    }
}
