//--------------------------------------------------------------------------------
//------------------------------InstructionsUI.cs---------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             Contains variables, text, and buttons that are used in
//             display of the instructions menu scene.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.4 - Added sound effects-----------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Class that manages access and houses information to all instructions menus
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
    //Next button pressed
    public void OnPage1NextPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        mainCanvas.enabled = false;
        secondCanvas.enabled = true;
    }
    //Next button pressed on page two
    public void OnPage2NextPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        DisableAllTowerText();
        secondCanvas.enabled = false;
        thirdCanvas.enabled = true;
    }
    //Previous button pressed on page 2
    public void OnPage2PreviousPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        DisableAllTowerText();
        secondCanvas.enabled = false;
        mainCanvas.enabled = true;
    }
    //Previous button pressed on page 3
    public void OnPage3PreviousPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        DisableAllEnemyText();
        thirdCanvas.enabled = false;
        secondCanvas.enabled = true;
    }
    //Return to main menu button pressed
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
    // green tower button pressed
    public void OnPlayerTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_PlayerTowerText.enabled = !m_PlayerTowerText.enabled;
    }
    // purple tower button pressed
    public void OnPurpleTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_PurpleTowerText.enabled = !m_PurpleTowerText.enabled;
    }
    // orange tower button pressed
    public void OnOrangeTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_OrangeTowerText.enabled = !m_OrangeTowerText.enabled;
    }
    // blue tower button pressed
    public void OnBlueTowerPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_BlueTowerText.enabled = !m_BlueTowerText.enabled;
    }
    // drill tower button pressed
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
    //Green enemy button pressed
    public void OnGreenEnemyPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_GreenEnemyText.enabled = !m_GreenEnemyText.enabled;
    }
    // Orange enemy button pressed
    public void OnOrangeEnemyPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_OrangeEnemyText.enabled = !m_OrangeEnemyText.enabled;
    }
    //Purple enemy button pressed
    public void OnPurpleEnemyPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        m_PurpleEnemyText.enabled = !m_PurpleEnemyText.enabled;
    }
    #endregion<Enemies>
}
