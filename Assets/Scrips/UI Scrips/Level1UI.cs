using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Level1UI : MonoBehaviour
{
    public int totalCash = 450;
    public int totalHealh = 100;

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
    private float m_fTimeToDisplayFundWarning;

    public bool m_bDisplayNotEnoughFundsText;
    // Start is called before the first frame update
    private void Start()
    {
        OnResumePlayPressed();
        m_redXButton.SetActive(false);
        m_fFundsTextCounter = 0.0f;
        m_fTimeToDisplayFundWarning = 3.5f;
        m_notEnoughFundsText.enabled = false;
    }
    private void Update()
    {
        if(m_bDisplayNotEnoughFundsText)
        {
            DisplayInsufficientFundsText();
        }
    }

    public void OnStartPressed()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void PauseMenuPressed()
    {
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
    }
    public void OnResumePlayPressed()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
    }
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnQuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
    #endregion<PauseMenu>

    public void OnXButtonPressed()
    {
        m_playerTouchHandler.m_currentSelectedGameObject = null;
        m_redXButton.SetActive(false);
    }
    private void DisplayInsufficientFundsText()
    {
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
}
