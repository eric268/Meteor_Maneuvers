//--------------------------------------------------------------------------------
//------------------------------BackgroundSoundManager.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls access to background music and
//             adds ability to play said music.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Added Earth health------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    static AudioSource m_audioSource;

    public static AudioClip mainMenuBackground;
    public static AudioClip instructionsBackground;
    public static AudioClip level1Background;
    public static AudioClip gameOverBackground;


    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        //Loads background sounds from resource folder
        mainMenuBackground = Resources.Load("Sound/Background/MainMenuSound") as AudioClip;
        instructionsBackground = Resources.Load("Sound/Background/InstructionsSound") as AudioClip;
        level1Background = Resources.Load("Sound/Background/Level1Sound") as AudioClip;
        gameOverBackground = Resources.Load("Sound/Background/GameOverSound") as AudioClip;
    }

    //static function which checks the background sound is to be played
    public static void PlayBackgroundMusic(string name)
    {
        if (name == mainMenuBackground.name)
        {
            m_audioSource.PlayOneShot(mainMenuBackground);
        }
        else if (name == instructionsBackground.name)
        {
            m_audioSource.PlayOneShot(instructionsBackground);
        }
        else if (name == level1Background.name)
        {
            m_audioSource.PlayOneShot(level1Background);
        }
        else if (name == gameOverBackground.name)
        {
            m_audioSource.PlayOneShot(gameOverBackground);
        }

    }
}
