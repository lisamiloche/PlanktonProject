using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MagnifyingGlass : MonoBehaviour
{
    public static MagnifyingGlass Instance;

    public Mask Mask;

    public bool IsOnObject;
    [HideInInspector] public bool CanShrink;

    InteractiveObjects _hidenObject;


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

        if (CanShrink)
        {
            StartCoroutine(Shrinking());

        }

    }


    IEnumerator Shrinking()
    {
        Mask.transform.DOScale(0.5f, 3);
        yield return new WaitForSeconds(3f);
        _hidenObject.OrderRenderer.sortingOrder = 1;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (InteractiveObjects obj in GameManager.Instance.HidenObjects)
        {
            if (collision.gameObject == obj)
            {

                CanShrink = true;
                _hidenObject = obj;
            }

        }
    }
}
