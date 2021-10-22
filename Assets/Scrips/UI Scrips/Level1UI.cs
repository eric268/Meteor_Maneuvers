using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Level1UI : MonoBehaviour
{
    public EnemySpawner m_enemySpawnerRef;

    public static float m_fTotalCash = 850;
    public float m_fTotalHealth = 100;
    public static float m_fTotalScore = 0;

    [SerializeField]
    public TextMeshProUGUI m_scoreValueText;

    [SerializeField]
    public TextMeshProUGUI m_GreenTowerAlredyPlaced;

    [SerializeField]
    public TextMeshProUGUI m_iHealthDisplayText;

    [SerializeField]
    public TextMeshProUGUI m_cashRemainingText;

    [SerializeField]
    public GameObject m_earthRef;

    #region<PauseMenu>
    [SerializeField]
    private Canvas pauseCanvas;

    [SerializeField]
    private PlayerTouchHandler m_playerTouchHandler;

    [SerializeField]
    private GameObject m_redXButton;

    [SerializeField]
    public TextMeshProUGUI m_notEnoughFundsText;

    private float m_fFundsTextCounter;
    private float m_fGreenTextCounter;
    private float m_fTimeToDisplayFundWarning;

    public bool m_bDisplayNotEnoughFundsText;

    [SerializeField]
    public bool m_bDisplayOnlyOneGreenTower;
    // Start is called before the first frame update
    private void Start()
    {
        BackgroundSoundManager.PlayBackgroundMusic("Level1Sound");
        OnResumePlayPressed();
        m_redXButton.SetActive(false);
        m_fFundsTextCounter = 0.0f;
        m_fTimeToDisplayFundWarning = 3.5f;
        m_notEnoughFundsText.enabled = false;
        m_GreenTowerAlredyPlaced.enabled = false;

    }
    private void Update()
    {
        m_cashRemainingText.text = m_fTotalCash.ToString();
        m_scoreValueText.text = m_fTotalScore.ToString();

        if (m_bDisplayOnlyOneGreenTower)
        {
            DisplayGreenTowerAlreadyPlacedText();
        }
        else if(m_bDisplayNotEnoughFundsText)
        {
            DisplayInsufficientFundsText();
        }
        

        if (m_fTotalHealth != m_earthRef.GetComponent<EarthAttributes>().m_iHealthRemaining)
        {
            m_fTotalHealth = m_earthRef.GetComponent<EarthAttributes>().m_iHealthRemaining;
            m_iHealthDisplayText.text = m_fTotalHealth.ToString();
            if (m_fTotalHealth <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void OnStartPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        EnemySpawner.m_fStartLevel = true;
    }
    public void PauseMenuPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
    }
    public void OnResumePlayPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
    }
    public void OnMainMenuPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("MainMenu");
    }
    public 
        void OnQuitPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
    #endregion<PauseMenu>

    public void OnXButtonPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        if (m_playerTouchHandler.m_currentSelectedGameObject != null)
        {
            m_playerTouchHandler.m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_highlightedBox.SetActive(false);
            m_playerTouchHandler.m_currentSelectedGameObject = null;
        }
        Destroy(m_playerTouchHandler.m_createdTower);
        m_redXButton.SetActive(false);
    }
    private void DisplayInsufficientFundsText()
    {
        m_GreenTowerAlredyPlaced.enabled = false;
        m_fGreenTextCounter = 0;
        m_bDisplayOnlyOneGreenTower = false;

        m_notEnoughFundsText.enabled = true;
        Debug.Log(m_fFundsTextCounter);
        m_fFundsTextCounter += Time.deltaTime;

        if (m_fFundsTextCounter >= m_fTimeToDisplayFundWarning)
        {
            m_bDisplayNotEnoughFundsText = false;
            m_notEnoughFundsText.enabled = false;
            m_fFundsTextCounter = 0.0f;
        }
    }
    private void DisplayGreenTowerAlreadyPlacedText()
    {
        m_bDisplayNotEnoughFundsText = false;
        m_notEnoughFundsText.enabled = false;
        m_fFundsTextCounter = 0;

        m_GreenTowerAlredyPlaced.enabled = true;
        m_fGreenTextCounter += Time.deltaTime;

        if (m_fGreenTextCounter >= m_fTimeToDisplayFundWarning)
        {
            m_bDisplayOnlyOneGreenTower = false;
            m_GreenTowerAlredyPlaced.enabled = false;
            m_fGreenTextCounter = 0.0f;
        }
    }
}
