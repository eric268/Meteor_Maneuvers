using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject m_currentSelectedGameObject;

    private GameObject m_createdTower;

    public bool m_bannerTowerSelected;

    public Vector3 m_touchStartPos;
    public Vector3 m_touchEndPos;

    [SerializeField]
    public GameObject m_allMeteors;

    [SerializeField]
    public GameObject m_banner;

    private Component[] m_meteorsWithColliders;
    private Component[] m_towersOnBanner;

    [SerializeField]
    public GameObject m_redXButton;

    [SerializeField]
    public Canvas m_Level1UICanvas;

    Vector3 m_vTouchHeld;

    // Start is called before the first frame update
    void Start()
    {
        m_bannerTowerSelected = false;
        m_meteorsWithColliders = m_allMeteors.GetComponentsInChildren<Collider2D>();
        m_towersOnBanner = m_banner.GetComponentsInChildren<Collider2D>();

        //foreach(var tower in m_towersOnBanner)
        //{
         //   Debug.Log(tower.gameObject.name);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPosition = Camera.main.ScreenToWorldPoint(touch.position);

            CheckForTowerBannerSelected();

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    m_touchStartPos = touchedPosition;
                    m_touchEndPos = Vector3.zero;
                    break;
                case TouchPhase.Moved:

                    m_vTouchHeld = new Vector3(touchedPosition.x - m_touchStartPos.x, touchedPosition.y - m_touchStartPos.y, touchedPosition.z - m_touchStartPos.z);
                    break;
                case TouchPhase.Ended:
                    m_touchEndPos = touchedPosition;
                    m_touchStartPos = Vector3.zero;
                    m_vTouchHeld = Vector3.zero;
                    break;
            }
        }
        
        if (m_currentSelectedGameObject != null)
        {

        }
    }

    void CheckForTowerBannerSelected()
    {
        foreach (var tower in m_towersOnBanner)
        {
            //See if the touch position is within one of the tower banner colliders
            if (tower.GetComponent<Collider2D>().OverlapPoint(m_touchStartPos))
            {
                if (m_Level1UICanvas.GetComponent<Level1UI>().totalCash >= tower.GetComponent<TowerAttributes>().towerCost)
                {
                    Debug.Log(tower.gameObject.name);
                    m_redXButton.SetActive(true);
                    m_currentSelectedGameObject = tower.gameObject;
                }
                else
                {
                    m_currentSelectedGameObject = null;
                    m_Level1UICanvas.GetComponent<Level1UI>().m_bDisplayNotEnoughFundsText = true;
                }
                break;
            }
        }
    }
}
