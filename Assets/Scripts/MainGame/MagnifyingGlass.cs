using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlass : MonoBehaviour
{
    public static MagnifyingGlass Instance;
    public bool IsOnObject;
    [HideInInspector] public bool CanShrink;


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
    }

    void Start()
    {
        CanShrink = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;

    }




}
