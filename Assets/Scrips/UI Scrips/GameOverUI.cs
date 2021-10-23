//--------------------------------------------------------------------------------
//------------------------------GameOverUI.cs-------------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             Contains variables, text, and buttons that are used in
//             display of the game game over scene.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.3 - Added game over scoring system variables

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

//Class that manages the game over scene functions to display text variables and buttons
public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    public float m_fRemainingHealh;
    public float m_score;
    public float m_finalScore;
    public float m_healthScore;

    [SerializeField]
    public float m_healthScoreMultiplier;

    [SerializeField]
    public TextMeshProUGUI m_healthScoreMultiplierText;
    [SerializeField]

    public TextMeshProUGUI m_healthText;
    [SerializeField]

    public TextMeshProUGUI m_healthScoreText;
    [SerializeField]
    public TextMeshProUGUI m_scoreText;
    [SerializeField]
    public TextMeshProUGUI m_finalScoreText;

    [SerializeField]
    public TextMeshProUGUI m_gameResultsText;

    private void Start()
    {
        //Plays background music
        BackgroundSoundManager.PlayBackgroundMusic("GameOverSound");

        //Value used to score remaining health
        m_healthScoreMultiplier = 10.0f;

        //Checks if data has been saved regarding Level1 scoring values
        if (PlayerPrefs.GetInt("Saved") == 1)
        {
            //Gets saved health and score
            m_fRemainingHealh = PlayerPrefs.GetFloat("HealthRemaining");
            m_score = PlayerPrefs.GetFloat("ScoreRemaining");
        }
        else
        {
            m_fRemainingHealh = 0;
            m_score = 0;
            m_finalScore = 0;
            m_healthScore = 0;
        }
        //Sets UI text to score values
        m_healthScore = (m_healthScoreMultiplier * m_fRemainingHealh);
        m_finalScore = m_score + m_healthScore;
        m_healthScoreMultiplierText.text = m_healthScoreMultiplier.ToString();
        m_healthText.text = m_fRemainingHealh.ToString();
        m_scoreText.text = m_score.ToString();
        m_healthScoreText.text = m_healthScore.ToString();
        m_finalScoreText.text = m_finalScore.ToString();

        if (m_fRemainingHealh > 0)
        {
            m_gameResultsText.text = "GAME WON!";
        }
        else
        {
            m_gameResultsText.text = "GAME LOST";
        }
    }
    //Button called when pause menu restart button is pressed
    public void OnRestartPressed()
    {

        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("Level1");
    }
    //Button called when main menu pause menu button is pressed
    public void OnMainMenuPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("MainMenu");
    }
    //Button called when the game over pause menu button is pressed
    public void OnQuitGamePressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
