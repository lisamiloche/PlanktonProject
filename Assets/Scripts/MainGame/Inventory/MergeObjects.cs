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

    private Inventory Inventory;

    private void Awake()
    {
        Image = GetComponent<Image>();
        Outline = GetComponent<Outline>();
    }
    void Start()
    {
        Inventory = GetComponent<Inventory>();
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
                    Inventory.ItemToMerge.Remove(this);
                    Inventory.NumberToMerge--;
                }
                else if (Inventory.NumberToMerge < 3)
                {
                    Inventory.ItemToMerge.Add(this);
                    Inventory.NumberToMerge++;
                    Outline.enabled = true;
                }
                else if (Inventory.NumberToMerge >= 3)
                {
                    Inventory.ItemToMerge[0].Outline.enabled = false;
                    Inventory.ItemToMerge.RemoveAt(0);
                    Inventory.ItemToMerge.Add(this);
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
        Debug.Log("enter" + type.ToString());
        _isOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        _isOn = false;
    }

   
}
