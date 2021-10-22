using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    Vector3 m_vTouchHeld;

    private List<GameObject> m_listOfTowersPlaced;

    [SerializeField]
    public Sprite m_radiusImage;

    // Start is called before the first frame update
    void Start()
    {
        m_bannerTowerSelected = false;
        m_meteorsWithColliders = m_allMeteors.GetComponentsInChildren<Collider2D>();
        m_towersOnBanner = m_banner.GetComponentsInChildren<Collider2D>();
        towersPlaced = 0;
        m_bTowerCanBePlaced = false;
        m_listOfTowersPlaced = new List<GameObject>();

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
                    ResetTowersIfSelected();
                    if (!CheckForTowerBannerSelected(m_touchStartPos))
                    {
                        CheckIfPlacedTowerSelected(m_touchStartPos);
                    }
                    break;

                case TouchPhase.Moved:
                    if (CheckIfCanPurchaseTower(m_touchStartPos))
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
                m_redXButton.SetActive(false);
            }
        }
    }

    bool CheckForTowerBannerSelected(Vector3 startTouchPos)
    {
        foreach (var tower in m_towersOnBanner)
        {
            //See if the touch position is within one of the tower banner collider
            if (tower.GetComponent<Collider2D>().OverlapPoint(startTouchPos))
            {
                m_currentSelectedGameObject = tower.gameObject;
                tower.GetComponent<BannerAttributes>().m_highlightedBox.SetActive(true);
                return true;
            }
        }
        return false;
    }

    void CheckIfPlacedTowerSelected(Vector3 startTouchPos)
    {
        foreach(GameObject tower in m_listOfTowersPlaced)
        {
            if (tower.GetComponent<Collider2D>().OverlapPoint(startTouchPos))
            {
                m_currentSelectedGameObject = tower;
                Debug.Log(m_currentSelectedGameObject.transform.GetChild(1).name);
                m_currentSelectedGameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        return;
    }

    bool CheckIfCanPurchaseTower(Vector3 startTouchPos)
    {
        if (CheckForTowerBannerSelected(startTouchPos))
        {
          if (Level1UI.totalCash >= m_currentSelectedGameObject.GetComponent<BannerAttributes>().towerCost)
          {
              m_redXButton.SetActive(true);
              return true;
          }
          else
          {
               m_Level1UICanvas.GetComponent<Level1UI>().m_bDisplayNotEnoughFundsText = true;
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
        Level1UI.totalCash -= m_createdTower.GetComponent<TowerAttributes>().m_fTowerCost;
        m_createdTower.GetComponent<TowerAttributes>().m_bIsActive = true;
        CreateRadiusCircleAroundTower();
        CreateColliderAroundTower();
        m_listOfTowersPlaced.Add(m_createdTower.gameObject);
        m_createdTower = null;
        towersPlaced++;
        m_bTowerCanBePlaced = false;
        
    }

    void CreateColliderAroundTower()
    {
        GameObject temp = new GameObject();
        temp.name = "TowerColldider";
        temp.AddComponent<CircleCollider2D>();
        temp.GetComponent<CircleCollider2D>().radius = m_createdTower.GetComponent<TowerAttributes>().m_bulletRange / 100.0f;
        temp.AddComponent<TowerCollision>();
        temp.transform.SetParent(m_createdTower.transform);
        temp.transform.localPosition = new Vector3(0, 0, 1.0f);
    }

    void CreateRadiusCircleAroundTower()
    {
        GameObject temp = new GameObject();
        temp.name = "Radius";

        temp.AddComponent<SpriteRenderer>();
        temp.GetComponent<SpriteRenderer>().sprite = m_radiusImage;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
        temp.GetComponent<SpriteRenderer>().enabled = false;
        temp.transform.localScale = temp.transform.lossyScale * (m_createdTower.GetComponent<TowerAttributes>().m_bulletRange/100.0f);
        temp.transform.SetParent(m_createdTower.transform);
        temp.transform.localPosition = new Vector3(0, 0, 1.0f);
    }

    void ResetTowersIfSelected()
    {
        if (m_currentSelectedGameObject != null)
        {
            if (m_currentSelectedGameObject.GetComponent<TowerAttributes>())
            {
                Debug.Log(m_currentSelectedGameObject.transform.GetChild(1));
                m_currentSelectedGameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (m_currentSelectedGameObject.GetComponent<BannerAttributes>())
            {
                m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_highlightedBox.SetActive(false);
            }
        }
    }
}
