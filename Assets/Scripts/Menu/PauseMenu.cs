using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Image Fade;
    public GameObject Pause;

    IEnumerator FadingQuit()
    {
        Fade.enabled = true;
        Fade.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Pause.SetActive(false);
    }

    public void Quit()
    {
        StartCoroutine(FadingQuit());
    }
}
