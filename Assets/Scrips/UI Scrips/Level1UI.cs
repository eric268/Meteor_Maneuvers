using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Level1UI : MonoBehaviour
{
    [SerializeField]
    private Canvas pauseCanvas;
    // Start is called before the first frame update
    private void Start()
    {
        OnResumePlayPressed();
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
}
