//--------------------------------------------------------------------------------
//------------------------------BannerAttributes.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 20/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script manages the attributes for towers on
//             the game banner 
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Attributes added--------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that encapsulates all banner attributes
public class BannerAttributes : MonoBehaviour
{
    [SerializeField]
    public int towerCost;

    [SerializeField]
    public TowerType m_bannerTowerType;

    [SerializeField]
    public GameObject towerPrefab;

    [SerializeField]
    public GameObject m_bTowerCost;

    // Start is called before the first frame update
    void Start()
    {
        m_bTowerCost.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
