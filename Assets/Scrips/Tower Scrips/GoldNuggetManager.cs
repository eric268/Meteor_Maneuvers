//--------------------------------------------------------------------------------
//------------------------------GoldNuggetManager.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls the bounds checking and touch
//             collision for all generated golden nuggets.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.1 - Added bounds checking---------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that manages the touch collision detection found golden nuggets
public class GoldNuggetManager : MonoBehaviour
{
    [SerializeField]
    public float m_fLifeSpan;
    [SerializeField]
    public float m_vValueUponCollection;

    private float m_fLifeSpanCounter;

    private void Start()
    {
        m_fLifeSpanCounter = 0.0f;
    }

    private void Update()
    {
        CheckBounds();
        CheckTouchCollsion();
        m_fLifeSpanCounter += Time.deltaTime;
        
    }
    //Checks if the player has touched or dragged their finger over a golden nugget
    void CheckTouchCollsion()
    {
        //Destroys nugget if its lifespan is up
        if (m_fLifeSpanCounter >= m_fLifeSpan)
        {
            Destroy(gameObject);
        }
        //Checks if touch input was received
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>().OverlapPoint(touchedPosition))
                    {
                        //If touch was received increment score and cash and destory nugget
                        Level1UI.m_fTotalScore += m_vValueUponCollection;
                        Level1UI.m_fTotalCash += m_vValueUponCollection;
                        SoundEffectManager.PlaySoundEffect("NuggetCollected");
                        Destroy(gameObject);
                    }
                    break;
                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>().OverlapPoint(touchedPosition))
                    {
                        SoundEffectManager.PlaySoundEffect("NuggetCollected");
                        Level1UI.m_fTotalScore += m_vValueUponCollection;
                        Level1UI.m_fTotalCash += m_vValueUponCollection;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
    //Checks if nugget is outside of scene bounds and returns it if so
    void CheckBounds()
    {
        if (transform.position.x > TowerAttributes.xBounds)
        {
            transform.position = new Vector3(TowerAttributes.xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -TowerAttributes.xBounds)
        {
            transform.position = new Vector3(-TowerAttributes.xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > TowerAttributes.yBoundsMax)
        {
            transform.position = new Vector3(transform.position.x, TowerAttributes.yBoundsMax, transform.position.z);
        }
        if (transform.position.y < TowerAttributes.yBoundsMin)
        {
            transform.position = new Vector3(transform.position.x, TowerAttributes.yBoundsMin, transform.position.z);
        }
    }
}
