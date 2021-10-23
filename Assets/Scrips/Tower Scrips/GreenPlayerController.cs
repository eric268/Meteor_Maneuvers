//--------------------------------------------------------------------------------
//------------------------------GreenPlayerController.cs--------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script handles the selection and control of the green 
//             player character. It rotates moves and selects this character
//------------------------------Revision History----------------------------------
//------------------------------Version 1.5 - Added arrival value for rotate and move functions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerController : MonoBehaviour
{
    public bool m_bSelected;
    private float m_bSpeed;
    private float m_fRotationSpeed;
    public Vector3 m_vTargetPosition;
    GameObject m_parent;
    // Start is called before the first frame update
    void Start()
    {
        m_bSelected = false;
        m_fRotationSpeed = 0.7f;
        m_bSpeed = 3.0f;
        transform.localPosition = new Vector3(0, 0, 1);
        GetComponent<SpriteRenderer>().enabled = false;
        m_vTargetPosition = transform.parent.gameObject.transform.position;
        m_parent = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //Checks if touch has been inputted
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                //Checks if the green player was selected
                case TouchPhase.Began:
                    if (transform.parent.gameObject.GetComponent<Collider2D>().OverlapPoint(touchedPosition))
                    {
                        //If they are already selected de select them
                        if (m_bSelected)
                        {
                            SoundEffectManager.PlaySoundEffect("ButtonPressed");
                            m_bSelected = false;
                            GetComponent<SpriteRenderer>().enabled = false;
                        }
                        //Otherwise select them
                        else
                        {
                            SoundEffectManager.PlaySoundEffect("ButtonPressed");
                            GetComponent<SpriteRenderer>().enabled = true;
                            m_bSelected = true;
                        }
                    }
                    else
                    {
                        //If the player is selected and the screen is touched somewhere update target location with touch location
                        if (m_bSelected)
                        {
                            m_vTargetPosition = new Vector3(touchedPosition.x, touchedPosition.y, 0.0f);
                            GetComponent<SpriteRenderer>().enabled = false;
                            m_bSelected = false;
                        }
                    }
                    break;
            }
                
        }
        //If the player is close to their target location do not update rotate or move
        //This is so its own enemy detection is not affecting these functions
        if (Vector3.Distance(m_vTargetPosition, m_parent.transform.position) > 0.1f)
        {
            RotateShip(m_vTargetPosition);
            MoveShip(m_vTargetPosition);
        }
        //Checks if player character collides with enemies and destroys the player character if occurs
        CheckCollisions();
    }

    void RotateShip(Vector3 touchedPos)
    {
            //Rotates the ship to face its target
            Vector2 m_vDesiredDirection = new Vector2(touchedPos.x - m_parent.transform.position.x, touchedPos.y - m_parent.transform.position.y);
            m_vDesiredDirection = m_vDesiredDirection.normalized;
            float desiredRotation = MathHelper.CalculateAngle(m_parent.GetComponent<TowerAttributes>().m_vDirection, m_vDesiredDirection) + m_parent.transform.eulerAngles.z;
            m_parent.transform.rotation = Quaternion.Slerp(m_parent.transform.rotation, Quaternion.Euler(0, 0, desiredRotation), m_fRotationSpeed);
    }

    void MoveShip(Vector3 touchedPos)
    {
        //Moves ship towards its target location
        m_parent.transform.position = new Vector2(Mathf.Lerp(m_parent.transform.position.x, touchedPos.x, m_bSpeed * Time.deltaTime),
            Mathf.Lerp(m_parent.transform.position.y, touchedPos.y, m_bSpeed * Time.deltaTime));
    }

    void CheckCollisions()
    {
        //Checks if ship collides with enemies
        List<Collider2D> collList = new List<Collider2D>();
        m_parent.transform.gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), collList);

        foreach(Collider2D coll in collList)
        {
            if (coll.gameObject.GetComponent<EnemyAttributes>())
            {
                //Player can destroy green and orange ship upon collision
                PlayerTouchHandler.m_bGreenPlayerPlaced = false;
                SoundEffectManager.PlaySoundEffect("Explosion");

                //Destroys green and orange enemies on collision
                if (coll.gameObject.GetComponent<EnemyAttributes>().m_enemyType != EnemyType.PURPLE_ENEMY)
                {
                    EnemyManager.Instance().ReturnEnemy(coll.gameObject, coll.gameObject.GetComponent<EnemyAttributes>().m_enemyType);

                    //Get points and cash for destroying them
                    Level1UI.m_fTotalCash += coll.gameObject.GetComponent<EnemyAttributes>().m_fCashWhenDestroyed;
                    Level1UI.m_fTotalScore += coll.gameObject.GetComponent<EnemyAttributes>().m_fCashWhenDestroyed;
                }
                //Destroy the green player character
                Destroy(transform.parent.gameObject);
                break;
                
            }
        }
    }
}
