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
    private bool _initialized = false;
 
    public GameObject SettingsCanvas;
    public GameObject Pause;

        public List<InteractiveObjects> HidenObjects;

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
    void Start()
    {
        
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
