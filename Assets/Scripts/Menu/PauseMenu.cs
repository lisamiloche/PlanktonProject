using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause;

    public void Resume()
    {
        Time.timeScale = 1f;
        Pause.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
