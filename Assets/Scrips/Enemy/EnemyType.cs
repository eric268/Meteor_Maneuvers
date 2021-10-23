//--------------------------------------------------------------------------------
//------------------------------EnemyType.cs--------------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script contains an enumeration for identifying enemies.  
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Added enemy type ENUM---------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy type enum
[System.Serializable]
public enum EnemyType
{
    GREEN_ENEMY,
    PURPLE_ENEMY,
    ORANGE_ENEMY,
    NUMBER_OF_ENEMY_TYPES
}
