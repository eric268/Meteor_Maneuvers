//--------------------------------------------------------------------------------
//------------------------------MathHelper.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script acts as a helper class with some math functions
//             used to assist with tower and enemy movements.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.1 - Added Calculate Direction-----------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a helper class containing some physics functions
public class MathHelper : MonoBehaviour
{
    //Calculates the angle between two vectors
    public static float CalculateAngle(Vector2 vec1, Vector2 vec2)
    {
        float det = vec1.x * vec2.y - vec1.y * vec2.x;

        return Mathf.Atan2(det, Vector2.Dot(vec1, vec2))*Mathf.Rad2Deg;
    }
    //Calculates the direction of a game object given its rotation (z value)
    public static Vector2 CalculateDirection(float rotation)
    {
        return new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad));
    }
}
