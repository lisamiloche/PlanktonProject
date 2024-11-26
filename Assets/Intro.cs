using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    public Image Fade;
    [SerializeField] private VideoPlayer _videoPlayer;
    private int _lenght;

    void Start()
    {
        Fade.DOFade(0, 1f);
        _lenght = (int)_videoPlayer.clip.length;
        StartCoroutine(WaitToChangScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(WaitToSkip());
    }

    IEnumerator WaitToChangScene()
    {
        yield return new WaitForSeconds(_lenght);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    IEnumerator WaitToSkip()
    {
        Fade.DOFade(1, 2f);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
