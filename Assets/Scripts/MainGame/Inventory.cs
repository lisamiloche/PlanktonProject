
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance;

    public List<Image> InventoryBoxs;
    public List<MergeObjects> ItemToMerge;

    public int NumberToMerge;

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
            item.Image = null;
            item.Outline.enabled = false;
        }
        NumberToMerge = 0;
    }
}
