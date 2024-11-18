using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject SettingsCanvas;
    public GameObject Pause;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            Pause.SetActive(true);
        }
    }


    public void Resume()
    { 
        Time.timeScale = 1f; 
        Pause.SetActive(false);
    }

    public void Settings()
    {
        SettingsCanvas.SetActive(true);
    }

    public void Back()
    {
        SettingsCanvas.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

}
