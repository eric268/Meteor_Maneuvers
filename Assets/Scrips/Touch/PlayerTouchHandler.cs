//--------------------------------------------------------------------------------
//------------------------------PlayerTouchHandler.cs-----------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script handles the player input for selecting most game
//             objects. It allows for selection of banner and placed towers
//             and allows the player to drag and drop towers into the scene
//             assuming they have the cash. Also allows for one generation
//             of the green player controlled ship.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.9 - Removed game object from drill tower creation

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Class that handles touch input for tower selection and purchase
public class PlayerTouchHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject m_currentSelectedGameObject;

    [SerializeField]
    public GameObject m_createdTower;

    [SerializeField]
    public GameObject m_greenPlayerRef;

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

    public Sprite m_highlightImage;

    static public bool m_bGreenPlayerPlaced;

    // Start is called before the first frame update
    void Start()
    {
        m_bannerTowerSelected = false;
        m_meteorsWithColliders = m_allMeteors.GetComponentsInChildren<Collider2D>();
        m_towersOnBanner = m_banner.GetComponentsInChildren<Collider2D>();
        towersPlaced = 0;
        m_bTowerCanBePlaced = false;
        m_listOfTowersPlaced = new List<GameObject>();
        m_bGreenPlayerPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if touch has occurred
        if (Input.touchCount > 0)
        {
            //If touch occurs get touch in screen to world position
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                //Records where touch began
                case TouchPhase.Began:
                    m_touchStartPos = touchedPosition;
                    m_touchEndPos = Vector3.zero;
                    //De selects all towers
                    ResetTowersIfSelected();

                    //Checks if the current touch game object is a tower banner
                    if (!CheckForTowerBannerSelected(m_touchStartPos))
                    {
                        //If not checks if current a placed touch is on a tower to display radius
                        CheckIfPlacedTowerSelected(m_touchStartPos);
                    }
                    break;
                    //Records if touch is being held and its position
                case TouchPhase.Moved:
                    //If player is trying to drag and drop tower see if it they can afford it
                    if (CheckIfCanPurchaseTower(m_touchStartPos))
                    {
                        //If so create that tower
                        CreateTower();
                    }
                    //Moves created tower to touched position and checks if it can be created there
                    MoveTower(touchedPosition);
                    //Checks if the tower was successfully placed
                    CheckIfTowerSuccessfullPlaced();
                    break;
                    //Records position where touch ended
                case TouchPhase.Ended:
                    //Reset touch variables
                    m_touchEndPos = touchedPosition;
                    m_touchStartPos = Vector3.zero;
                    m_prevTouchPosition = Vector3.zero;
                    m_vTouchHeld = Vector3.zero;
                    //If tower can be placed where finger stopped touched then place the tower
                    if (m_createdTower != null && m_bTowerCanBePlaced)
                    {
                        PlaceTower();
                    }
                    break;
            }
        }
    }
    //Moves the tower to the touched position
    void MoveTower(Vector3 touchedPos)
    {
        m_vTouchHeld = new Vector3(touchedPos.x - m_touchStartPos.x, touchedPos.y - m_touchStartPos.y, touchedPos.z - m_touchStartPos.z);

        if (m_vTouchHeld.magnitude != 0.0f && m_createdTower != null)
        {
            m_createdTower.transform.position = new Vector3((m_vTouchHeld.x - m_prevTouchPosition.x) + m_createdTower.transform.position.x, (m_vTouchHeld.y - m_prevTouchPosition.y) + m_createdTower.transform.position.y, 0);
            m_prevTouchPosition = m_vTouchHeld;
        }
    }

    //If a tower has been selected and the touch is being held on the screen then create one of the select towers
    void CreateTower()
    {
        if (m_createdTower == null && m_vTouchHeld.magnitude != 0.0f)
        {
            if (m_currentSelectedGameObject != null && m_currentSelectedGameObject.GetComponent<BannerAttributes>())
            {
                Vector3 startPos = m_touchStartPos + m_vTouchHeld;
                //Creates a tower from a tower prefab based upon which tower is selected
                m_createdTower = MonoBehaviour.Instantiate(m_currentSelectedGameObject.GetComponent<BannerAttributes>().towerPrefab);
                m_createdTower.transform.position = startPos;
                //No longer display tower price on banner
                m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_bTowerCost.SetActive(false);
                m_currentSelectedGameObject = null;

                //Initially shows tower as dark red until it has found a place where it can be successfully spawned (on meteor with no other tower close by)
                m_createdTower.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f,0.0f, 255.0f);
            }
        }
    }
    //Checks to see if the touched position is one of the towers on the bottom banner
    bool CheckForTowerBannerSelected(Vector3 startTouchPos)
    {
        foreach (var tower in m_towersOnBanner)
        {
            //See if the touch position is within one of the tower banner collider
            if (tower.GetComponent<Collider2D>().OverlapPoint(startTouchPos))
            {
                m_currentSelectedGameObject = tower.gameObject;

                //If selected banner tower if the green player tower checks if one has already been spawned
                //(Only one green tower can exist at a time)
                if (m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_bannerTowerType == TowerType.GREEN_TOWER && m_bGreenPlayerPlaced == true)
                {
                    m_Level1UICanvas.GetComponent<Level1UI>().m_bDisplayOnlyOneGreenTower = true;
                    m_currentSelectedGameObject = null;
                    return true;
                }
                //Green tower already exists and displays message to the player
                else if (m_currentSelectedGameObject == m_greenPlayerRef)
                {
                    m_currentSelectedGameObject = null;
                    return false;
                }
                //Shows tower cost UI
                tower.GetComponent<BannerAttributes>().m_bTowerCost.SetActive(true);
                return true;
            }
        }
        return false;
    }
    //Checks if the touch input is selecting one of the placed towers
    //If so will display the towers radius
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
    //Checks to see if the selected banner tower can be afforded by the player
    bool CheckIfCanPurchaseTower(Vector3 startTouchPos)
    {
        if (CheckForTowerBannerSelected(startTouchPos))
        {
          if (m_currentSelectedGameObject != null && Level1UI.m_fTotalCash >= m_currentSelectedGameObject.GetComponent<BannerAttributes>().towerCost)
          {
              m_redXButton.SetActive(true);
              return true;
          }
          //If player cannot afford tower displays this to the player via UI text
          else
          {
               m_Level1UICanvas.GetComponent<Level1UI>().m_bDisplayNotEnoughFundsText = true;
          }
            
        }
        return false;
    }
    //Checks if the tower that is currently trying to be created is in a spot where it can be spawned.
    //Spawning spots usually have to be on meteors away from other towers
    void CheckIfTowerSuccessfullPlaced()
    {
        int numBannerTowers = 5;
        bool onMeteor = false;
        bool collidingWithOtherTower = false;
        bool collidingWithBannerTowers = false;
        int arraySize = m_meteorsWithColliders.Length + towersPlaced + numBannerTowers + 1;

        if (m_createdTower != null)
        {
            //The green ship is the only tower that does not need to be spawned on meteor
            if (m_createdTower.GetComponent<TowerAttributes>().m_towerType == TowerType.GREEN_TOWER)
            {
                Collider2D[] colliderArray = new Collider2D[arraySize];

                //Check that the green tower is not being spawned on the banner
                m_createdTower.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliderArray);

                for (int i = 0; i < arraySize; i++)
                {
                    if (colliderArray[i] != null)
                    {
                        if (colliderArray[i].GetComponent<BannerAttributes>())
                        {
                            collidingWithBannerTowers = true;
                        }
                    }
                }
                //Shows the tower as its correct color if it can be spawned in that location
                if (!collidingWithBannerTowers)
                {
                    m_createdTower.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
                    m_bTowerCanBePlaced = true;
                }
                //Shows tower in dark red if it cannot be spawned in that location
                else
                {
                    m_createdTower.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f, 0.0f, 255.0f);
                    m_bTowerCanBePlaced = false;
                }
                return;
            }
            //See if enemy is on a meteor, away from another ship and is not too close to the banner
            else
            {
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
                //Again changes tower color based upon if its spawning location is valid
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
    }
    //If the tower is released above a valid location this function will place it in the scene
    void PlaceTower()
    {
        //Subtract the money from the game currency
        Level1UI.m_fTotalCash -= m_createdTower.GetComponent<TowerAttributes>().m_fTowerCost;
        m_createdTower.GetComponent<TowerAttributes>().m_bIsActive = true;

        //If drill tower is created it leaves early as it does not need to detect enemeies
        if (m_createdTower.GetComponent<TowerAttributes>().m_towerType == TowerType.DRILL_TOWER)
        {
            m_listOfTowersPlaced.Add(m_createdTower.gameObject);
            m_createdTower = null;
            towersPlaced++;
            m_bTowerCanBePlaced = false;
            m_redXButton.SetActive(false);
            return;
        }
        //Creates the circle which represents the tower based on its radius attribute
        CreateRadiusCircleAroundTower();
        //Creates a circle collider that matches its radius for enemy detection
        CreateColliderAroundTower();

        //If green tower is created add highlight box game object to show when it is selected
        if (m_createdTower.GetComponent<TowerAttributes>().m_towerType == TowerType.GREEN_TOWER)
        {
            CreateGreenPlayerTower();
        }

        m_listOfTowersPlaced.Add(m_createdTower.gameObject);
        m_createdTower = null;
        towersPlaced++;
        m_bTowerCanBePlaced = false;

        //Removes UI button that is used to return tower that is no longer needed to be placed
        m_redXButton.SetActive(false);
    }
    //Creates collider around tower for enemy detection
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
    //Creates radius visual around tower
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
    //Removes all UI visuals from banner towers and placed towers
    void ResetTowersIfSelected()
    {
        if (m_currentSelectedGameObject != null)
        {
            if (m_currentSelectedGameObject.GetComponent<TowerAttributes>())
            {
                m_currentSelectedGameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (m_currentSelectedGameObject.GetComponent<BannerAttributes>())
            {
                m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_bTowerCost.SetActive(false);
            }
        }
    }
    //Created boarder highlight around green player controller
    void CreateGreenPlayerTower()
    {
        m_bGreenPlayerPlaced = true;
        m_greenPlayerRef = m_createdTower;
        GameObject temp = new GameObject();
        temp.name = "boarderHighlight";
        temp.AddComponent<SpriteRenderer>();
        temp.GetComponent<SpriteRenderer>().sprite = m_highlightImage;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 8;
        temp.GetComponent<SpriteRenderer>().enabled = false;
        temp.AddComponent<GreenPlayerController>();
        temp.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        
        temp.transform.SetParent(m_createdTower.transform);
        temp.transform.localPosition = new Vector3(0, 0, 1);
    }
}
