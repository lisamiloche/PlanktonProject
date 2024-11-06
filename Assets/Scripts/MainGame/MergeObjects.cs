using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeObjects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image Image;
    private bool _isOn = false;
    void Start()
    {
        Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOn)
        {
            if (Input.GetMouseButtonDown(1) && Image.sprite != null)
            {
                if (Inventory.Instance.NumberToMerge <= 3)
                {
                    Inventory.Instance.ItemToMerge.Add(Image);
                    Inventory.Instance.NumberToMerge++;
                }
                else
                {
                    Inventory.Instance.ItemToMerge.RemoveAt(0);
                    Inventory.Instance.ItemToMerge.Add(Image);
                }

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
