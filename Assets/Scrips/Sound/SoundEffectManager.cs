//--------------------------------------------------------------------------------
//------------------------------SoundEffectManager.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls access to all sound effects  
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Added play sound effects function---

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    static AudioSource m_audioSource;

    public static AudioClip gameStarted;
    public static AudioClip bulletHit;
    public static AudioClip goldNuggetCollected;
    public static AudioClip towerFireBullet;
    public static AudioClip characterExplosion;
    public static AudioClip buttonPressed;
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        gameStarted = Resources.Load("Sound/Effects/GameStarted") as AudioClip;
        bulletHit = Resources.Load("Sound/Effects/BulletHit") as AudioClip;
        goldNuggetCollected = Resources.Load("Sound/Effects/NuggetCollected") as AudioClip;
        towerFireBullet = Resources.Load("Sound/Effects/FireBullet") as AudioClip;
        characterExplosion = Resources.Load("Sound/Effects/Explosion") as AudioClip;
        buttonPressed = Resources.Load("Sound/Effects/ButtonPressed") as AudioClip;
    }

    // Static function which plays a sound effect given a sound effect name
    public static void PlaySoundEffect(string name)
    {
        if (name == gameStarted.name)
        {
            m_audioSource.PlayOneShot(gameStarted, 0.1f);
        }
        else if (name == bulletHit.name)
        {
            m_audioSource.PlayOneShot(bulletHit, 0.25f);
        }
        else if (name == goldNuggetCollected.name)
        {
            m_audioSource.PlayOneShot(goldNuggetCollected, 0.07f);
        }
        else if (name == towerFireBullet.name)
        {
            m_audioSource.PlayOneShot(towerFireBullet, 0.1f);
        }
        else if (name == characterExplosion.name)
        {
            m_audioSource.PlayOneShot(characterExplosion, 0.05f);
        }
        else if (name == buttonPressed.name)
        {
            m_audioSource.PlayOneShot(buttonPressed, 0.25f);
        }

    }
}
