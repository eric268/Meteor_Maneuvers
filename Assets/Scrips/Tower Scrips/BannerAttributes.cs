using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAttributes : MonoBehaviour
{
    [SerializeField]
    public int towerCost;

    [SerializeField]
    public TowerType m_bannerTowerType;

    [SerializeField]
    public GameObject towerPrefab;

    [SerializeField]
    public GameObject m_highlightedBox;

    // Start is called before the first frame update
    void Start()
    {
        m_highlightedBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
