//--------------------------------------------------------------------------------
//------------------------------Level1UI.cs---------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             Contains variables, text, and buttons that are used in
//             display of the Level 1 game scene.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.3 - Added display for green player already placed text

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

//Class that encapsulates all level 1 buttons, text, variables and general UI elements
public class Level1UI : MonoBehaviour
{
    public EnemySpawner m_enemySpawnerRef;

    public static float m_fTotalCash;
    public float m_fTotalHealth;
    public static float m_fTotalScore;

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

        m_fTotalCash = 850;
        m_fTotalHealth = 100;
        m_fTotalScore = 0;

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
                PlayerPrefs.SetFloat("HealthRemaining", 0);
                PlayerPrefs.SetFloat("ScoreRemaining", m_fTotalScore);
                PlayerPrefs.SetInt("Saved", 1);
                PlayerPrefs.Save();
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

    //This button deselects the current tower that is trying to be placed in the scene
    public void OnXButtonPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        if (m_playerTouchHandler.m_currentSelectedGameObject != null)
        {
            m_playerTouchHandler.m_currentSelectedGameObject.GetComponent<BannerAttributes>().m_bTowerCost.SetActive(false);
            m_playerTouchHandler.m_currentSelectedGameObject = null;
        }
        Destroy(m_playerTouchHandler.m_createdTower);
        m_redXButton.SetActive(false);
    }
    //Displays text that player does not have sufficient funds to purchase tower
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
    //Displays text that the green player ship already exists in the scene (only one can be made at a time)
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
