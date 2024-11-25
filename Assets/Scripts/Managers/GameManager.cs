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

    public List<InteractiveObjects> HidenObjects;

    [SerializeField] private int _nbOfObjToDrop;
    [HideInInspector] public int _nbOfObjDropped;
    [SerializeField] private bool _needObjToChangeScene;


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
                Debug.Log("Changement de scène");
                //fade + changement de scène
            }
        }
        else
        {
            Debug.Log("Pas besoin d'objet spécifique");
            // voir comment on change de scène
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
