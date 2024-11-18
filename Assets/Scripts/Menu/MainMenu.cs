using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Image Fade;

    public GameObject Settings;
    public GameObject Credits;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadingStart());
    }

    public void ButtonPlay()
    {
        StartCoroutine(FadingPlay());
    }

    public void ButtonSettings()
    {
        Settings.SetActive(true);
    }

    public void ButtonCredits()
    {
        Credits.SetActive(true);
    }

    public void ButtonBack()
    {
        Settings.SetActive(false);
        Credits.SetActive(false);
    }

    public void ButtonQuit()
    {
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        StartCoroutine(FadingQuit());
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator FadingStart()
    {
        Fade.DOFade(0, 3);
        yield return new WaitForSeconds(3f);
        Fade.enabled = false;
    }


    IEnumerator FadingPlay()
    {
        Fade.enabled = true;
        Fade.DOFade(1, 2);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    IEnumerator FadingQuit()
    {
        Fade.enabled = true;
        Fade.DOFade(1, 2);
        yield return new WaitForSeconds(2);
    }

}
