
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance;

    public List<MergeObjects> InventoryBoxs;
    public List<MergeObjects> ItemToMerge;
    public ListImageMerge[] MergeSprites;

    public int NumberToMerge;
    public int ItemInInventory;

    public Button MergeButton;


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

    }

    // Update is called once per frame
    void Update()
    {
        if (NumberToMerge >= 2 && GameManager.Instance.index != 5 && GameManager.Instance.index != 6 && GameManager.Instance.index != 8 )
        {
            MergeButton.gameObject.SetActive(true);
            MergeButton.gameObject.transform.position = new Vector3(GameManager.Instance.MainCam.transform.position.x, 0, 0);
        }
        if (NumberToMerge < 2)
        {
            MergeButton.gameObject.SetActive(false);
        }
    }


    public void OnClickTryMerge()
    {
        string toCheck = ItemToMerge[0].type.ToString();
        for (int i = 1; i < ItemToMerge.Count; i++)
        {
            if (toCheck != ItemToMerge[i].type.ToString())
            {
                Debug.Log("You can't merge, sorry.");
                AudioManager.Instance.PlaySFX(3);
                break;
            }
            if (i == ItemToMerge.Count-1)
            {
                foreach (var item in ItemToMerge)
                {
                    AudioManager.Instance.PlaySFX(5);
                    item.Image.sprite = null;
                    item.Outline.enabled = false;
                    item.IsOccupied = false;
                    ItemInInventory--;
                }
                NumberToMerge = 0;
                foreach (var item in MergeSprites)
                {
                    if (item.type.ToString() == ItemToMerge[0].type.ToString())
                    {
                        ItemToMerge[0].Image.sprite = item.Sprite;
                    }
                }

               
                ItemToMerge.Clear();
            }
        }
    }
}
