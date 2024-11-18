using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class MagnifyingGlass : MonoBehaviour
{
    public static MagnifyingGlass Instance;

    public SpriteMask Mask;

    public LayerMask LayerMask;

    [HideInInspector] public bool CanShrink;

    GameObject _hidenObject;
    Sequence sequence;
    Guid uid;

    Vector3 Scale;


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

    Coroutine coco;


    void Start()
    {
        Scale = Mask.transform.localScale;
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
            PlayScale();
        }

        if (!CanShrink)
        {
            KillScale();

        }
        //StopCoroutine(coco);

        LayerMask layerMask = LayerMask;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("Hillo");
            CanShrink = true;
            _hidenObject = hit.collider.gameObject;
        }

        if (hit.collider == null)
        {
            CanShrink = false;
            _hidenObject = null;
            
        }

    }


    void PlayScale()
    {
        if (sequence == null)
        {
            sequence = DOTween.Sequence();
            sequence.Append(Mask.transform.DOScale(0.5f, 3));
            uid = System.Guid.NewGuid();
            sequence.id = uid;
        }
        sequence.Play();
    }

    void KillScale()
    {
        DOTween.Kill(uid);
        sequence = null;
        Mask.transform.DOScale(Scale,0.5f);
    }

}
