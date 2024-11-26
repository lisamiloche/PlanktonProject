using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject MGlass;
    public SpriteMask MaskingGlass;
    [SerializeField] private GameObject _grain;
    public Camera MainCam;
    private int index;
    public GameObject Pause;
    public GameObject Player;
    public Image Fade;
    public bool CanDialog;  

    public List<InteractiveObjects> HidenObjects;

    [SerializeField] private int _nbOfObjToDrop;
    [HideInInspector] public int _nbOfObjDropped;
    [SerializeField] private bool _needObjToChangeScene;
    [SerializeField] private bool _needDialogToChangeScene;
    [SerializeField] private Dialog _dialog;
    [SerializeField] private string _sceneSuivante;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("STOOOOOOOOOOOOOOOOOOOOP");
            return;
        }
        else
        {
            Instance = this;
        }
        _grain.SetActive(false);
    }

    private void Start()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(FadingStart());
        index = SceneManager.GetActiveScene().buildIndex;
        ChooseGoodSound();
    }

    private void ChooseGoodSound()
    {
        if (index == 2 || index == 5)
        {
            AudioManager.Instance.PlayMusic(1, true);
        }
        else if (index == 4 || index == 9)
        {
            AudioManager.Instance.PlayMusic(2, true);
        }
        else if (index == 7)
        {
            AudioManager.Instance.PlayMusic(3, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("index : " + index);

        if (Input.GetKeyDown(KeyCode.V))
        {
            AudioManager.Instance.PlaySFX(7);
            if (MGlass.activeSelf) { MGlass.SetActive(false); _grain.SetActive(false); }
            else { MGlass.SetActive(true); _grain.SetActive(true); }
        }


        if (Input.GetKeyDown(KeyCode.Escape) && Fade.enabled == false)
        {
            Pause.transform.position = new Vector3(MainCam.transform.position.x, 0, 0);
            Time.timeScale = 0f;
            Pause.SetActive(true);
        }

        Debug.Log(_nbOfObjDropped);
        if (_needDialogToChangeScene && _needObjToChangeScene)
        {
            CanDialog = false;
            if (_nbOfObjDropped == _nbOfObjToDrop)
            {
                CanDialog = true;
                if (index == 6)
                {
                    AudioManager.Instance.PlaySFX(0);
                }
                if (!_dialog.InProgress)
                {
                    if (index == 4)
                        AudioManager.Instance.PlaySFX(1);
                    StartCoroutine(FadingBetweenScene());
                }
            }
        }
        else if (_needObjToChangeScene)
        {
            CanDialog = true;
            if (_nbOfObjDropped == _nbOfObjToDrop)
            {
                if (_needDialogToChangeScene)
                {
                    if (!_dialog.InProgress)
                    {
                        if (index == 4)
                            AudioManager.Instance.PlaySFX(1);
                        StartCoroutine(FadingBetweenScene());
                    }
                    else
                        Debug.Log("Besoin de dialog fini");
                }
                else
                {
                    if (index == 4)
                        AudioManager.Instance.PlaySFX(1);
                    StartCoroutine(FadingBetweenScene());
                }
                //Ajouter un fade
            }
        }
        else if (_needDialogToChangeScene)
        {
            CanDialog = true;
            if (!_dialog.InProgress)
            {
                if (index == 4)
                    AudioManager.Instance.PlaySFX(1);
                StartCoroutine(FadingBetweenScene());
            }
            else
                Debug.Log("Besoin de dialog fini");
        }
        else
        {
            Debug.Log("Management de la scène anormal");
        }

    }
    IEnumerator FadingBetweenScene()
    {
        Fade.enabled = true;
        Fade.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_sceneSuivante);
    }
    IEnumerator FadingStart()
    {
        Fade.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        Fade.enabled = false;
    }
   


}
