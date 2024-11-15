
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance;

    public List<MergeObjects> InventoryBoxs;
    public List<MergeObjects> ItemToMerge;

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
        if (NumberToMerge >= 2)
        {
            MergeButton.gameObject.SetActive(true);
        }
        if (NumberToMerge < 2)
        {
            MergeButton.gameObject.SetActive(false);
        }
    }


    public void OnClickTryMerge()
    {
        foreach (var item in ItemToMerge)
        {
            item.Image.sprite = null;
            item.Outline.enabled = false;
            item.IsOccupied = false;
            ItemInInventory--;
        }
        NumberToMerge = 0;
        ItemToMerge.Clear();
    }
}
