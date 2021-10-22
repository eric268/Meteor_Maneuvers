using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private Canvas secondCanvas;
    [SerializeField]
    private Canvas thirdCanvas;

    [SerializeField]
    private TextMeshProUGUI m_PlayerTowerText;
    [SerializeField]
    private TextMeshProUGUI m_PurpleTowerText;
    [SerializeField]
    private TextMeshProUGUI m_OrangeTowerText;
    [SerializeField]
    private TextMeshProUGUI m_BlueTowerText;
    [SerializeField]
    private TextMeshProUGUI m_DrillTowerText;
    [SerializeField]
    private Image m_goldImage;

    [SerializeField]
    private TextMeshProUGUI m_GreenEnemyText;
    [SerializeField]
    private TextMeshProUGUI m_OrangeEnemyText;
    [SerializeField]
    private TextMeshProUGUI m_PurpleEnemyText;



    void Start()
    {
        mainCanvas.enabled = true;
        secondCanvas.enabled = false;
        thirdCanvas.enabled = false;

        DisableAllTowerText();
        DisableAllEnemyText();
    }
    #region<GameObjective>
    public void OnPage1NextPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        mainCanvas.enabled = false;
        secondCanvas.enabled = true;
    }
    public void OnPage2NextPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        DisableAllTowerText();
        secondCanvas.enabled = false;
        thirdCanvas.enabled = true;
    }

    public void OnPage2PreviousPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        DisableAllTowerText();
        secondCanvas.enabled = false;
        mainCanvas.enabled = true;
    }

    public void OnPage3PreviousPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        DisableAllEnemyText();
        thirdCanvas.enabled = false;
        secondCanvas.enabled = true;
    }

    public void OnReturnToMainMenuPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("MainMenu");
    }
    #endregion<GameObjective>

    #region<Towers>
    void DisableAllTowerText()
    {
        m_PlayerTowerText.enabled = false;
        m_PurpleTowerText.enabled = false;
        m_OrangeTowerText.enabled = false;
        m_BlueTowerText.enabled = false;
        m_DrillTowerText.enabled = false;
        m_goldImage.enabled = false;
    }

    public void OnPlayerTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_PlayerTowerText.enabled = !m_PlayerTowerText.enabled;
    }

    public void OnPurpleTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_PurpleTowerText.enabled = !m_PurpleTowerText.enabled;
    }

    public void OnOrangeTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_OrangeTowerText.enabled = !m_OrangeTowerText.enabled;
    }

    public void OnBlueTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_BlueTowerText.enabled = !m_BlueTowerText.enabled;
    }

    public void OnDrillPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_DrillTowerText.enabled = !m_DrillTowerText.enabled;
        m_goldImage.enabled = m_DrillTowerText.enabled;
    }
    #endregion<Towers>

    #region<Enemies>
    void DisableAllEnemyText()
    {
        m_GreenEnemyText.enabled = false;
        m_OrangeEnemyText.enabled = false;
        m_PurpleEnemyText.enabled = false;
    }
    public void OnGreenEnemyPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_GreenEnemyText.enabled = !m_GreenEnemyText.enabled;
    }
    public void OnOrangeEnemyPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_OrangeEnemyText.enabled = !m_OrangeEnemyText.enabled;
    }
    public void OnPurpleEnemyPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_PurpleEnemyText.enabled = !m_PurpleEnemyText.enabled;
    }
    #endregion<Enemies>
}
