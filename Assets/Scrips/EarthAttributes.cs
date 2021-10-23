//--------------------------------------------------------------------------------
//------------------------------EarthAttributes.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls the attributes for the Earth  
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Added Earth health------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAttributes : MonoBehaviour
{
    public float m_iHealthRemaining;
    // Start is called before the first frame update
    void Start()
    {
        m_iHealthRemaining = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
