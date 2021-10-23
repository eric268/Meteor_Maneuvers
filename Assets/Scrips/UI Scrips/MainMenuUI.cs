//--------------------------------------------------------------------------------
//------------------------------MainMenuUI.cs---------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             Contains variables, text, and buttons that are used in
//             display of the main menu scene.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.2 - Added sound effects-----------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

//Manages all functions called when main menu buttons are pressed
public class MainMenuUI : MonoBehaviour
{

    private void Start()
    {
        //Plays background music
        BackgroundSoundManager.PlayBackgroundMusic("MainMenuSound");
    }
    //Function called when start button is pressed
    public void OnStartGamePressed()
    {
        SoundEffectManager.PlaySoundEffect("GameStarted");
        SceneManager.LoadScene("Level1");
    }
    //Function called when instructions button is pressed
    public void OnInstructionsPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("Instructions");
    }
    //Function called when quit game button is pressed
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
