//--------------------------------------------------------------------------------
//------------------------------MainMenuMovement.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 28/09/2021-------------------------
//------------------------------Description---------------------------------------
//             This script moves enemies in the main menu 
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Added reposition of enemy based on scene bounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages movement of ships on main menu scene
public class MainMenuMovement : MonoBehaviour
{

    private Vector2 m_vStartingPosition;

    void Start()
    {
        //Moves ships
        m_vStartingPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        //Resets ship if they move far enough on the negative x axis
        if (transform.position.x <= -22)
        {
            transform.position = m_vStartingPosition;
        }
    }
}
