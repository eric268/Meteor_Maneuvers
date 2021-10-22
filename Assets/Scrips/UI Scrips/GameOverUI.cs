using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void Start()
    {
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
