using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    private void Start()
    {
        BackgroundSoundManager.PlayBackgroundMusic("MainMenuSound");
    }

    public void OnStartGamePressed()
    {
        SoundEffectManager.PlaySoundEffect("GameStarted");
        SceneManager.LoadScene("Level1");
    }
    public void OnInstructionsPressed()
    {
        SoundEffectManager.PlaySoundEffect("ButtonPressed");
        SceneManager.LoadScene("Instructions");
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
