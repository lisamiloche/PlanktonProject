using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeObjects : TypesManager, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Image Image;
    [HideInInspector] public Outline Outline;
    [HideInInspector] public bool IsOccupied;
    private bool _isOn = false;
    [HideInInspector] public bool _drag = false; //Ajout Lisa
    

    void Start()
    {
        Image = GetComponent<Image>();
        Outline = GetComponent<Outline>();
        Outline.enabled = false;
        IsOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOn)
        {
            if (Input.GetMouseButtonDown(1) && Image.sprite != null)
            {
                if (Outline.enabled)
                {
                    Outline.enabled = false;
                    Inventory.Instance.ItemToMerge.Remove(this); 
                    Inventory.Instance.NumberToMerge--;
                }
                else if (Inventory.Instance.NumberToMerge < 3)
                {
                    Inventory.Instance.ItemToMerge.Add(this);
                    Inventory.Instance.NumberToMerge++;
                    Outline.enabled = true;
                }
                else if (Inventory.Instance.NumberToMerge >= 3)
                {
                    Inventory.Instance.ItemToMerge[0].Outline.enabled = false;
                    Inventory.Instance.ItemToMerge.RemoveAt(0);
                    Inventory.Instance.ItemToMerge.Add(this);
                    Outline.enabled = true;
                }

            }

            if (Input.GetMouseButton(0) && Image.sprite != null) // AJOUT LISA
            {
                _drag = true;
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        _isOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        _isOn = false;
    }

   
}
