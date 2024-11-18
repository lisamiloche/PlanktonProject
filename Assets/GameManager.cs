using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MGlass;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (MGlass.activeSelf) { MGlass.SetActive(false); }
            else { MGlass.SetActive(true); }
        }
    }
}
