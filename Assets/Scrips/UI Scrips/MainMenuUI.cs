using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartGamePressed()
    {
        SceneManager.LoadScene("Level1");
    }
    public void OnInstructionsPressed()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void OnQuitGamePressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
