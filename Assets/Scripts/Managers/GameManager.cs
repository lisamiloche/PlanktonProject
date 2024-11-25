using DG.Tweening;
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
    public GameObject SettingsCanvas;
    public GameObject Pause;
    public GameObject Player;
    public Image Fade;

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
        Time.timeScale = 1f;
        Fade.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (MGlass.activeSelf) { MGlass.SetActive(false); _grain.SetActive(false); }
            else { MGlass.SetActive(true); _grain.SetActive(true); }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            Pause.SetActive(true);
        }

        Debug.Log(_nbOfObjDropped);
        if(_needObjToChangeScene)
        {
            if (_nbOfObjDropped == _nbOfObjToDrop)
            {
                if(_needDialogToChangeScene)
                {
                    if(!_dialog.InProgress)
                    {
                        StartCoroutine(FadingBetweenScene());
                    }
                    else
                    Debug.Log("Besoin de dialog fini");
                }
                else
                {
                    StartCoroutine(FadingBetweenScene());
                }                
                //Ajouter un fade
            }
        }
        else if (_needDialogToChangeScene)
        {
            if (!_dialog.InProgress)
            {
                StartCoroutine(FadingBetweenScene());
            }
            else
                Debug.Log("Besoin de dialog fini");
        }
        else{
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
