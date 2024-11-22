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

    InteractiveObjects _hidenObject;
    Sequence sequence;
    Guid uid;

    Vector3 Scale;
    int _timeSinceStarted = 0;


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
        coco = StartCoroutine(WaitToGoInventory());
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
            _timeSinceStarted++;
        }

        if (!CanShrink)
        {
            KillScale();

        }

        LayerMask layerMask = LayerMask;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null && (hit.collider.gameObject.GetComponent("InteractiveObjects") as InteractiveObjects) !=null)
        {
            Debug.Log("Hillo");
            CanShrink = true;
            _hidenObject = hit.collider.gameObject.GetComponent("InteractiveObjects") as InteractiveObjects;
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
        if (_timeSinceStarted == 1)
            StartCoroutine(WaitToGoInventory());
    }

    void KillScale()
    {
        _timeSinceStarted = 0;
        DOTween.Kill(uid);
        sequence = null;
        Mask.transform.DOScale(Scale,0.5f);
        StopCoroutine(coco);
    }


    IEnumerator WaitToGoInventory()
    {
         
        yield return new WaitForSeconds(3f);

        if (_hidenObject != null)
        {
            foreach (var item in Inventory.Instance.InventoryBoxs)
            {
                if (!item.IsOccupied)
                {
                    Inventory.Instance.ItemInInventory++;
                    Debug.Log(item.type.ToString());
                    if (_hidenObject != null)
                    {
                        item.type = _hidenObject.type;
                        item.Image.sprite = _hidenObject.Image;
                    }
                    item.IsOccupied = true;
                    break;
                }
            }
            Destroy(_hidenObject.gameObject);
        }
    }

}
