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
        mainMenuBackground = Resources.Load("Sound/Background/MainMenuSound") as AudioClip;
        instructionsBackground = Resources.Load("Sound/Background/InstructionsSound") as AudioClip;
        level1Background = Resources.Load("Sound/Background/Level1Sound") as AudioClip;
        gameOverBackground = Resources.Load("Sound/Background/GameOverSound") as AudioClip;

        Debug.Log(mainMenuBackground.name);


    }

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

    public static void PlaySoundEffect(string name)
    {

    }
}
