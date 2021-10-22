using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BannerCostUI : MonoBehaviour
{
    public TextMeshProUGUI m_costText;
    public GameObject m_bannerShip;
    // Start is called before the first frame update
    void Start()
    {
        m_costText.text = "$" + m_bannerShip.GetComponent<BannerAttributes>().towerCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
