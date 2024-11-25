using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    private int _lenght;
    void Start()
    {
        _lenght = (int)_videoPlayer.clip.length;
        StartCoroutine(WaitToChangScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitToChangScene()
    {
        yield return new WaitForSeconds(_lenght);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
