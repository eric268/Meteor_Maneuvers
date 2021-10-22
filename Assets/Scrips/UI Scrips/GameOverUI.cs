using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

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

    private void Start()
    {
        m_healthScoreMultiplier = 10.0f;
        BackgroundSoundManager.PlayBackgroundMusic("MainMenuSound");

        if (PlayerPrefs.GetInt("Saved") == 1)
        {
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
        m_healthScore = (m_healthScoreMultiplier * m_fRemainingHealh);
        m_finalScore = m_score + m_healthScore;
        m_healthScoreMultiplierText.text = m_healthScoreMultiplier.ToString();
        m_healthText.text = m_fRemainingHealh.ToString();
        m_scoreText.text = m_score.ToString();
        m_healthScoreText.text = m_healthScore.ToString();
        m_finalScoreText.text = m_finalScore.ToString();

        BackgroundSoundManager.PlayBackgroundMusic("GameOverSound");
    }
    public void OnRestartPressed()
    {

        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("Level1");
    }

    public void OnMainMenuPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("MainMenu");
    }

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
