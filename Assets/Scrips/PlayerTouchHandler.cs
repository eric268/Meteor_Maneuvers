using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject m_currentSelectedGameObject;

    [SerializeField]
    public GameObject m_createdTower;

    public bool m_bannerTowerSelected;

    public Vector3 m_touchStartPos;
    public Vector3 m_touchEndPos;
    public Vector3 m_prevTouchPosition;

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

    private bool m_bTowerCanBePlaced;

    private int towersPlaced;

    private bool m_bBannerTowerTouched = false;
    Vector3 m_vTouchHeld;

    // Start is called before the first frame update
    void Start()
    {
        m_bannerTowerSelected = false;
        m_meteorsWithColliders = m_allMeteors.GetComponentsInChildren<Collider2D>();
        m_towersOnBanner = m_banner.GetComponentsInChildren<Collider2D>();
        towersPlaced = 0;
        m_bTowerCanBePlaced = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                        m_touchStartPos = touchedPosition;
                        m_touchEndPos = Vector3.zero;
                    break;

                case TouchPhase.Moved:
                    if (CheckForTowerBannerSelected())
                    {
                        CreateTower();
                    }
                    MoveTower(touchedPosition);
                    CheckIfTowerSuccessfullPlaced();
                    break;

                case TouchPhase.Ended:
                    m_touchEndPos = touchedPosition;
                    m_touchStartPos = Vector3.zero;
                    m_prevTouchPosition = Vector3.zero;
                    m_vTouchHeld = Vector3.zero;
                    if (m_createdTower != null && m_bTowerCanBePlaced)
                    {
                        PlaceTower();
                    }
                    break;
            }
        }
    }
    void MoveTower(Vector3 touchedPos)
    {
        m_vTouchHeld = new Vector3(touchedPos.x - m_touchStartPos.x, touchedPos.y - m_touchStartPos.y, touchedPos.z - m_touchStartPos.z);

        if (m_vTouchHeld.magnitude != 0.0f && m_createdTower != null)
        {
            m_createdTower.transform.position = new Vector3((m_vTouchHeld.x - m_prevTouchPosition.x) + m_createdTower.transform.position.x, (m_vTouchHeld.y - m_prevTouchPosition.y) + m_createdTower.transform.position.y, 0);
            m_prevTouchPosition = m_vTouchHeld;
        }
    }

    void CreateTower()
    {
        if (m_createdTower == null && m_vTouchHeld.magnitude != 0.0f)
        {
            if (m_currentSelectedGameObject != null && m_currentSelectedGameObject.GetComponent<BannerAttributes>())
            {
                Vector3 startPos = m_touchStartPos + m_vTouchHeld;
                m_createdTower = MonoBehaviour.Instantiate(m_currentSelectedGameObject.GetComponent<BannerAttributes>().towerPrefab);
                m_createdTower.transform.position = startPos;
                m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_highlightedBox.SetActive(false);
                m_currentSelectedGameObject = null;
                m_createdTower.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f,0.0f, 255.0f);
            }
        }
    }

    bool CheckForTowerBannerSelected()
    {
        foreach (var tower in m_towersOnBanner)
        {
            //See if the touch position is within one of the tower banner collider
            if (tower.GetComponent<Collider2D>().OverlapPoint(m_touchStartPos))
            {
                if (m_Level1UICanvas.GetComponent<Level1UI>().totalCash >= tower.GetComponent<BannerAttributes>().towerCost)
                {
                    if (m_currentSelectedGameObject != null && m_currentSelectedGameObject.GetComponent<BannerAttributes>())
                    {
                        m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_highlightedBox.SetActive(false);
                    }

                    Debug.Log(tower.gameObject.name);
                    m_redXButton.SetActive(true);
                    m_currentSelectedGameObject = tower.gameObject;
                    tower.GetComponent<BannerAttributes>().m_highlightedBox.SetActive(true);
                    return true;
                }
                else
                {
                    m_currentSelectedGameObject = null;
                    tower.GetComponent<BannerAttributes>().m_highlightedBox.SetActive(false);
                    m_Level1UICanvas.GetComponent<Level1UI>().m_bDisplayNotEnoughFundsText = true;
                }

            }
        }
        return false;
    }

    void CheckIfTowerSuccessfullPlaced()
    {
        if (m_createdTower != null)
        {
            int numBannerTowers = 5;
            int arraySize = m_meteorsWithColliders.Length + towersPlaced + numBannerTowers + 1;
            bool onMeteor = false;
            bool collidingWithOtherTower = false;
            bool collidingWithBannerTowers = false;
            Collider2D[] colliderArray = new Collider2D[arraySize];

            m_createdTower.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliderArray);

            for (int i = 0; i < arraySize; i++)
            {
                if (colliderArray[i] != null)
                {
                    if (colliderArray[i].gameObject.name == "Meteor")
                    {
                        onMeteor = true;
                    }
                    else if (colliderArray[i].GetComponent<TowerAttributes>())
                    {
                        Debug.Log(colliderArray[i].gameObject.name);
                        collidingWithOtherTower = true;
                    }
                    else if (colliderArray[i].GetComponent<BannerAttributes>())
                    {
                        collidingWithBannerTowers = true;
                    }
                }
            }

            if (onMeteor && !collidingWithOtherTower && !collidingWithBannerTowers)
            {
                m_createdTower.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
                m_bTowerCanBePlaced = true;
            }
            else
            {
                m_createdTower.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f, 0.0f, 255.0f);
                m_bTowerCanBePlaced = false;
            }
        }
    }

    void PlaceTower()
    {
        //Subtract the money from the game currency
        m_createdTower.GetComponent<TowerAttributes>().m_bIsActive = true;
        m_createdTower = null;
        towersPlaced++;
        m_bTowerCanBePlaced = false;
    }
}
