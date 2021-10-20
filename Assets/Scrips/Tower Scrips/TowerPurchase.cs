using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPurchase : MonoBehaviour
{
    [SerializeField]
    public PlayerTouchHandler m_playerTouchInfo;

    [SerializeField]
    public Collider2D m_collider2D;

    [SerializeField]
    public GameObject m_redXButton;

    [SerializeField]
    public Canvas m_Level1UICanvas;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_collider2D.OverlapPoint(m_playerTouchInfo.m_touchStartPos))
        {
            if (m_Level1UICanvas.GetComponent<Level1UI>().totalCash >= GetComponent<BannerAttributes>().towerCost)
            {
                Debug.Log("Green Ship selected");
                m_redXButton.SetActive(true);
                m_playerTouchInfo.m_currentSelectedGameObject = gameObject;
            }
            else
            {
                m_playerTouchInfo.m_currentSelectedGameObject = null;
                m_Level1UICanvas.GetComponent<Level1UI>().m_bDisplayNotEnoughFundsText = true;
            }
            m_playerTouchInfo.m_touchStartPos = new Vector2(0.0f, 0.0f);
        }
        else
        {
            m_playerTouchInfo.m_currentSelectedGameObject = null;
        }

       
    }
    private void OnGUI()
    {
    }
}
